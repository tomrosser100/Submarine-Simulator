    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public bool CanMove { get; private set; } = true;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 6.0f;
    [SerializeField] private float gravity = 30.0f;
    private Vector3 moveDirection;
    private CharacterController characterController;

    [Header("Look")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;  
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;  
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;  
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f; 
    private float lookRotation = 0;
    private Camera playerCamera;

    [Header("Interact")]
    [SerializeField] private bool canInteract = true;
    [SerializeField] private KeyCode interactKey = KeyCode.Mouse0;
    [SerializeField] private Interactable currentInteractable;
    [SerializeField] private Vector3 interactionRayPoint = default;
    [SerializeField] private float interactionDistance = default;
    [SerializeField] private LayerMask interactionLayer = default;

    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();
            HandleGravity();
            HandleMouseLook();

            if (canInteract)
                HandleInteraction();

            ApplyFinalMovements();
        }
    }

    private void HandleMovementInput()
    {
        float moveDirectionY = moveDirection.y;
        moveDirection = TransformDirectionFromLocalToWorldSpace(transform, walkSpeed);
        moveDirection.y = moveDirectionY;

        static Vector3 TransformDirectionFromLocalToWorldSpace(Transform transform, float walkSpeed) => 
            ConvertForwardVector2XToVector3X(transform, walkSpeed) + ConvertRightVector2XToVector3X(transform, walkSpeed);
      
        static Vector3 ConvertForwardVector2XToVector3X(Transform transform, float walkSpeed) => 
            transform.TransformDirection(Vector3.forward) * GetCurrentForwardVelocity(walkSpeed);

        static Vector3 ConvertRightVector2XToVector3X(Transform transform, float walkSpeed) =>
            transform.TransformDirection(Vector3.right) * GetCurrentRightVelocity(walkSpeed);

        static float GetCurrentRightVelocity(float walkSpeed) => Input.GetAxis("Horizontal") * walkSpeed;

        static float GetCurrentForwardVelocity(float walkSpeed) => Input.GetAxis("Vertical") * walkSpeed;
    }

    private void HandleGravity()
    {
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;
    }

    private void HandleMouseLook()
    { 
        lookRotation += GetMouseLookUpwardVelocity(lookSpeedY);
        lookRotation = ClampUpperAndLowerLookLimits(lookRotation, upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = UpwardVelocityToEulerQuarternion(lookRotation);
        transform.rotation *= RightVelocityToEulerQuarternion(lookSpeedX);

        static float GetMouseLookUpwardVelocity(float lookSpeedY) => -Input.GetAxis("Mouse Y") * lookSpeedY;

        static float GetMouseLookRightVelocity(float lookSpeedX) => Input.GetAxis("Mouse X") * lookSpeedX;

        static Quaternion UpwardVelocityToEulerQuarternion(float lookRotation) => Quaternion.Euler(lookRotation, 0, 0);

        static float ClampUpperAndLowerLookLimits(float lookRotation, float upperLookLimit, float lowerLookLimit) =>
            Mathf.Clamp(lookRotation, -upperLookLimit, lowerLookLimit);

        static Quaternion RightVelocityToEulerQuarternion(float lookSpeedX) => 
            Quaternion.Euler(0, GetMouseLookRightVelocity(lookSpeedX), 0);

    }

    private void HandleInteraction()
    {
        if (Physics.Raycast(GetVector3Origin(playerCamera, interactionRayPoint), out RaycastHit hitDirection, interactionDistance))
        {
            if (ColliderIsInCollisionLayer(hitDirection) && (currentInteractable == null || CurrentInteractableExistsButIsDifferent(hitDirection, currentInteractable)))
            {
                hitDirection.collider.TryGetComponent(out currentInteractable);

                if (currentInteractable)
                    currentInteractable.OnFocus();
            }
        } else if (currentInteractable)
        {
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }

        if (currentInteractable != null && Physics.Raycast(GetVector3Origin(playerCamera, interactionRayPoint), out RaycastHit _, interactionDistance, interactionLayer))
            currentInteractable.OnInteract();

        static bool CurrentInteractableExistsButIsDifferent(RaycastHit hitDirection, Interactable currentInteractable) =>
            hitDirection.collider.gameObject.GetInstanceID() != currentInteractable.GetInstanceID();

        static bool ColliderIsInCollisionLayer(RaycastHit hitDirection) => hitDirection.collider.gameObject.layer == 9;

        static Ray GetVector3Origin(Camera playerCamera, Vector3 interactionRayPoint) =>
            playerCamera.ViewportPointToRay(interactionRayPoint);
    }

    private void ApplyFinalMovements() =>
        characterController.Move(moveDirection * Time.deltaTime);

}

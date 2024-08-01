using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBase : Interactable
{
    private Animator steeringWheel;
    private bool isSpinning = false;
    private int stage = 3;
    private float leftLimit = 0;
    private float rightLimit = 0;
    public float leftTurn = 0;
    public float rightTurn = 0;
    void Start()
    {
        steeringWheel = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (stage)
        {
            case 0:
                leftLimit = 1;
                break;
            case 1:
                leftLimit = 0.6f;
                break;
            case 2:
                leftLimit = 0.3f;
                break;
            case 3:
                leftLimit = 0;
                rightLimit = 0;
                break;
            case 4:
                rightLimit = 0.3f;
                break;
            case 5:
                rightLimit = 0.6f;
                break;
            case 6:
                rightLimit = 1;
                break;
        }

        if (leftLimit < leftTurn) {
            leftTurn -= 0.1f * Time.deltaTime;
        } else {
            leftTurn += 0.1f * Time.deltaTime;
        }

        if (rightLimit < rightTurn) {
            rightTurn -= 0.1f * Time.deltaTime;
        } else {
            rightTurn += 0.1f * Time.deltaTime;
        }

        
    }
    public override void OnInteract()
    {
        if (!isSpinning)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && stage > 0)
            {
                stage--;
                StartCoroutine(SpinLeft(true));
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && stage < 6)
            {
                stage++;
                StartCoroutine(SpinLeft(false));
            }
        }
    }

    IEnumerator SpinLeft(bool left)
    {
        isSpinning = true;
        if (left)
        {
            steeringWheel.Play("SteeringLeft");
        } else {
            steeringWheel.Play("SteeringRight");
        }
        yield return new WaitForSeconds(1);
        isSpinning = false;

        yield return null;
    }

    public override void OnLoseFocus(){}
    public override void OnFocus(){
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnitionBase : Interactable
{
    private EngineControl engineControl;
    private Animator buttonAnimator;
    void Start()
    {
        engineControl = GameObject.FindObjectOfType<EngineControl>();
        buttonAnimator = GetComponentInChildren<Animator>();
    }

    public override void OnInteract()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            buttonAnimator.SetTrigger("PressButton");
            engineControl.Initiate();
        }
    }
    public override void OnLoseFocus(){}
    public override void OnFocus(){}

}

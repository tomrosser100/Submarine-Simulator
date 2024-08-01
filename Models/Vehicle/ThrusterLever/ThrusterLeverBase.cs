using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterLeverBase : Interactable
{
    private EngineControl engine;
    private Animator leverAnimator;
    public int step = 1;
    public bool isReversing = false;
    private float engineLimit = 0;
    void Start()
    {
        engine = GameObject.FindObjectOfType<EngineControl>();
        leverAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (engine.engineReady)
        {
            switch(step)
            {
                case 0:
                    isReversing = true;
                    engineLimit = 0.5f;
                    break;
                case 1:
                    isReversing = false;
                    engineLimit = 0;
                    break;
                case 2:
                    engineLimit = 0.5f;
                    break;
                case 3:
                    engineLimit = 1f;
                    break;
            }
        } else {
            engineLimit = 0;
        }

        if (engine.enginePower < engineLimit)
        {
            engine.enginePower += 0.1f * Time.deltaTime;
        }

        if (engine.enginePower > engineLimit)
        {
            engine.enginePower -= 0.1f * Time.deltaTime;
        }

    }

    public override void OnInteract()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            AnimationBackward();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            AnimationForward();
        }
    }
    void AnimationForward()
    {
        switch(step)
        {
            case 0:
                step++;
                leverAnimator.Play("LeverReverse 0");
                break;

            case 1:
                step++;
                leverAnimator.Play("LeverAheadSlow");
                break;

            case 2:
                step++;
                leverAnimator.Play("LeverAheadFast");
                break;
        }
    }

    void AnimationBackward()
    {
        switch(step)
        {
            case 1:
                step--;
                leverAnimator.Play("LeverReverse");
                break;

            case 2:
                step--;
                leverAnimator.Play("LeverAheadSlow 0");
                break;

            case 3:
                step--;
                leverAnimator.Play("LeverAheadFast 0");
                break;

        }
    }
    public override void OnLoseFocus(){}
    public override void OnFocus(){}
}

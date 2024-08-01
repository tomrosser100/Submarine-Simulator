using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthControlWheel : Interactable
{
    private Animator wheel;
    private bool isTurning = false;
    private int stage = 3;
    private float upLimit = 0;
    private float downLimit = 0;
    public float upTurn = 0;
    public float downTurn = 0;

    void Start()
    {
        wheel = GetComponent<Animator>();
    }
    void Update()
    {
        switch (stage)
        {
            case 0:
                upLimit = 1;
                break;
            case 1:
                upLimit = 0.6f;
                break;
            case 2:
                upLimit = 0.3f;
                break;
            case 3:
                upLimit = 0;
                downLimit = 0;
                break;
            case 4:
                downLimit = 0.3f;
                break;
            case 5:
                downLimit = 0.6f;
                break;
            case 6:
                downLimit = 1;
                break;
        }

        if (upLimit < upTurn) {
            upTurn -= 0.05f * Time.deltaTime;
        } else {
            upTurn += 0.05f * Time.deltaTime;
        }

        if (downLimit < downTurn) {
            downTurn -= 0.05f * Time.deltaTime;
        } else {
            downTurn += 0.05f * Time.deltaTime;
        }
    }
    public override void OnInteract()
    {
        print("Test");
        if (!isTurning)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && stage > 0)
            {
                stage--;
                StartCoroutine(WheelTurn(true));
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && stage < 6)
            {
                stage++;
                StartCoroutine(WheelTurn(false));
            }
        }
    }

    IEnumerator WheelTurn(bool up)
    {
        isTurning = true;
        if (up) {
            wheel.Play("WheelTurn");
        } else {
            wheel.Play("WheelTurn 0");
        }
        yield return new WaitForSeconds(1);
        isTurning = false; 
    }
    public override void OnLoseFocus(){}
    public override void OnFocus(){}
}

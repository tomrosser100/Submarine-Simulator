using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthControlSlide : MonoBehaviour
{
    private float x;
    private float y;
    private Vector3 upLimit;
    private Vector3 downLimit;

    DepthControlWheel depth;

    private float t;

    private float neutral = 0.5f;
    private float up;
    private float down;

    void Start()
    {
        depth = GameObject.FindObjectOfType<DepthControlWheel>();
        //transform.localPosition = new Vector3 (0.200000003f,1.90734909e-06f,-2.20000005f);
        x = transform.localPosition.x;
        y = transform.localPosition.y;
    }

    void Update()
    {
        upLimit = new Vector3 (x, y, -0.07f);
        downLimit = new Vector3 (x, y, -4.23f);

        up = depth.upTurn / 2;
        down = depth.downTurn / 2;
        t = neutral + up - down;
        
        transform.localPosition = Vector3.Lerp(downLimit, upLimit, t);
    }
}

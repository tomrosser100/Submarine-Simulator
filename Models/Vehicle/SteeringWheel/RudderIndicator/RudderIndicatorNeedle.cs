using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RudderIndicatorNeedle : MonoBehaviour
{

    //private Quaternion neutral = new Quaternion(-0.7f, 0, 0, 0.7f);
    private Quaternion left = new Quaternion(-0.65f, -0.25f, 0.25f, 0.65f);
    private Quaternion right = new Quaternion(-0.65f, 0.25f, -0.25f, 0.65f);

    SteeringBase steering;
    private float t;

    private float neutral = 0.5f;
    private float steeringLeft;
    private float steeringRight;



    void Start()
    {
        steering = GameObject.FindAnyObjectByType<SteeringBase>();
    }

    void Update()
    {
        steeringLeft = steering.leftTurn / 2;
        steeringRight = steering.rightTurn / 2;
        t = neutral + steeringRight - steeringLeft;
        transform.localRotation = Quaternion.Slerp(left, right, t);
    }
}

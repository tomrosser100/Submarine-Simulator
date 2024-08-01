using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    EngineControl engine;
    SteeringBase steering;
    DepthControlWheel depth;

    public float bouyancy = 0;

    void Awake()
    {
        engine = GameObject.FindObjectOfType<EngineControl>();
        steering = GameObject.FindObjectOfType<SteeringBase>();
        depth = GameObject.FindObjectOfType<DepthControlWheel>();
    }

    void FixedUpdate()
    {

        transform.position -= transform.forward * engine.enginePower * Time.deltaTime;

        if (engine.enginePower > 0)
            transform.Rotate(new Vector3 (0, steering.leftTurn + -steering.rightTurn, 0) * Time.deltaTime);
            
        transform.position += transform.up *  (depth.upTurn + -depth.downTurn)/2 * Time.deltaTime;
    }
} 

/*


*/
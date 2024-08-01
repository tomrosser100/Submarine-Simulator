using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheel : MonoBehaviour
{
    private AudioSource wheel;
    public AudioClip wheelTurn;
    void Start()
    {
        wheel = GetComponent<AudioSource>();
    }
    void PlaySound() =>
        wheel.PlayOneShot(wheelTurn);

}
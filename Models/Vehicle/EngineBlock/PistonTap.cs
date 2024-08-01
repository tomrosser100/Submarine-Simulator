using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonTap : MonoBehaviour
{
    private AudioSource piston;
    public AudioClip pistonTap;
    void Start()
    {
        piston = GetComponent<AudioSource>();
    }
    void PlaySound() =>
        piston.PlayOneShot(pistonTap);

}
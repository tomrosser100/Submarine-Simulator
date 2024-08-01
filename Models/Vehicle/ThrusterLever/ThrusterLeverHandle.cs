using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterLeverHandle : MonoBehaviour
{
    private AudioSource lever;
    public AudioClip leverPull;
    void Start()
    {
        lever = GetComponent<AudioSource>();
    }
    void PlaySound() =>
        lever.PlayOneShot(leverPull);

}

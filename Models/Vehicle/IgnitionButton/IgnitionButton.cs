using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnitionButton : MonoBehaviour
{
    private AudioSource button;
    public AudioClip buttonPress;

    void PlaySound() =>
        button.PlayOneShot(buttonPress);

    void Start()
    {
        button = GetComponent<AudioSource>();
    }

}

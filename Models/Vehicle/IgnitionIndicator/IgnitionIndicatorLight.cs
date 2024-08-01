using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnitionIndicatorLight : MonoBehaviour
{
    Renderer bulb;
    [SerializeField, Range(0, 3)] public int off_blinking_ready_stop = 0;
    void Start()
    {
        bulb = GetComponent<Renderer>();
        StartCoroutine(Light());
    }
    IEnumerator Light()
    {
        //print($"Light beginning: {off_blinking_ready_stop}");

        while (off_blinking_ready_stop == 0)
        {
            //print($"Off: {off_blinking_ready_stop}");
            bulb.material.SetColor("_EmissionColor", new Color (0, 0, 0, 0));
            yield return new WaitForSeconds(.25f);
            yield return null;
        }

        while (off_blinking_ready_stop == 1)
        {
            //print($"Blinking: {off_blinking_ready_stop}");
            bulb.material.SetColor("_EmissionColor", new Color(1, 1, 0, 1));
            yield return new WaitForSeconds(.25f);
            bulb.material.SetColor("_EmissionColor", new Color (0, 0, 0, 0));
            yield return new WaitForSeconds(.25f);
            yield return null;
        }

        while (off_blinking_ready_stop == 2)
        {
            //print($"Ready: {off_blinking_ready_stop}");
            bulb.material.SetColor("_EmissionColor", new Color(1, 1, 0, 1));
            yield return new WaitForSeconds(.25f);
            yield return null;
        }

        while (off_blinking_ready_stop == 3)
        {
            //print($"Stop: {off_blinking_ready_stop}");
            bulb.material.SetColor("_EmissionColor", new Color(1, 0, 0, 1));
            yield return new WaitForSeconds(.25f);
            yield return null;
        }

    StartCoroutine(Light());
    
    }
}

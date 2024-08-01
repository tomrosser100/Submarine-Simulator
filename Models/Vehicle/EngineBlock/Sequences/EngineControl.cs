using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineControl : MonoBehaviour
{
    private Animator pistonAnimator;
    public AudioSource engineAudio;
    public AudioClip engineStop;
    public AudioClip stutter1;
    public AudioClip stutter2;
    public AudioClip engineStart;
    IgnitionIndicatorLight indicator;
    ThrusterLeverBase lever;
    Coroutine coroutine1;
    Coroutine coroutine2;
    Coroutine coroutine3;
    Coroutine coroutine4;
    private bool stage1Complete;
    private bool stage2Complete;
    private bool stage3Complete;
    private bool engineOn = false;
    public bool engineReady = false;

    public float enginePower = 0;

    void Start()
    {
        indicator = GameObject.FindObjectOfType<IgnitionIndicatorLight>();
        lever = GameObject.FindObjectOfType<ThrusterLeverBase>();
        pistonAnimator = GetComponentInChildren<Animator>();
        pistonAnimator.enabled = false;
    }
    void Update()
    {
        pistonAnimator.speed = enginePower + 1;
        engineAudio.pitch = enginePower + 1;
    }

    public void Initiate()
    {
        if (indicator.off_blinking_ready_stop != 3)
        {
            stage1Complete = false;
            stage2Complete = false;
            stage3Complete = false;
            
            if (!engineOn)
            {
                engineOn = true;
                coroutine1 = StartCoroutine(Stage1());
                coroutine2 = StartCoroutine(Stage2());
                coroutine3 = StartCoroutine(Stage3());
                coroutine4 = StartCoroutine(Stage4());

            } 
            else 
            {
                engineOn = false;
                engineReady = false;
                StartCoroutine(Kill());
                StopCoroutine(coroutine1);
                StopCoroutine(coroutine2);
                StopCoroutine(coroutine3);
                StopCoroutine(coroutine4);

            }
        }
    }

    IEnumerator Kill()
    {
        indicator.off_blinking_ready_stop = 3;
        
        yield return new WaitUntil(() => enginePower < 0.01f);
        if (engineAudio.isPlaying)
        {
            engineAudio.Stop();
            engineAudio.PlayOneShot(engineStop);
        }
        pistonAnimator.enabled = false;

        yield return new WaitForSeconds(2);
        indicator.off_blinking_ready_stop = 0;

        yield return null;
    }

    IEnumerator Stage1()
    {
        indicator.off_blinking_ready_stop = 1;
        yield return new WaitForSeconds(2);

        stage1Complete = true;
        yield return null;
    }

    IEnumerator Stage2()
    {
        yield return new WaitUntil(() => stage1Complete);

        int randomNum = 0;
        while (randomNum != 2)
        {
            randomNum = Random.Range(0, 3);

            if (randomNum == 0)
            {
                engineAudio.PlayOneShot(stutter1);
                yield return new WaitForSeconds(2);
            }

            if (randomNum == 1)
            {
                engineAudio.PlayOneShot(stutter2);
                yield return new WaitForSeconds(2);
            }
        }

        stage2Complete = true;
        yield return null;
    }

    IEnumerator Stage3()
    {
        yield return new WaitUntil(() => stage2Complete);
        indicator.off_blinking_ready_stop = 2;
        engineAudio.PlayOneShot(engineStart);
        yield return new WaitForSeconds(0.3f);
        pistonAnimator.enabled = true;
        engineAudio.Play();

        stage3Complete = true;
        engineReady = true;
        yield return null;
    }

    IEnumerator Stage4()
    {
        yield return new WaitUntil(() => stage3Complete);

        print("stage 4 commence");

        


        // this needs one number between 0 and 1 that indicates animation speed and engine noise increase
        // where 0 equals minimum and 1 equals maximum possible

        // this number will correlate to an engine power variable
        // the engine power variable will be manipulated by the position of the thruster lever

        // this value must update continually

        yield return null;
    }
}


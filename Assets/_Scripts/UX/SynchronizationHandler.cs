using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchronizationHandler : MonoBehaviour
{
    [SerializeField] AudioSource romeo;
    [SerializeField]private AudioSource julia;

    private void OnEnable()
    {
        TextSnippetController.OnActorResponse += StartJuliaSynchro;
        TextSnippetController.OnRomeoAnimation += StartRomeoSynchro;
        ActorManager.OnActorChanged += SetJuliaSource;
    }

    private void OnDisable()
    {
        TextSnippetController.OnActorResponse -= StartJuliaSynchro;
        TextSnippetController.OnRomeoAnimation -= StartRomeoSynchro;
        ActorManager.OnActorChanged -= SetJuliaSource;  
    }

    private void StartJuliaSynchro(EmotionAnswerWrapper wrapper)
    {
        //if(wrapper.myClip != null)
        //{
            julia.clip = wrapper.myClip;
            julia.Play();
        //}
    }

    private void StartRomeoSynchro(AnimationAnswerWrapper romeoanswer)
    {
        //if (romeoanswer.myClip != null)
        //{
            romeo.clip = romeoanswer.myClip;
            romeo.Play();
        //}
    }

    private void SetJuliaSource(AudioSource source)
    {
        julia = source;
    }

    // Start is called before the first frame update6
}

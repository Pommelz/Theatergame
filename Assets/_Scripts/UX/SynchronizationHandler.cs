using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchronizationHandler : MonoBehaviour
{
    [SerializeField] AudioSource romeo;
    [SerializeField] private AudioSource julia;
    bool isReplay = false;

    private void OnEnable()
    {
        TextSnippetController.OnActorResponse += StartJuliaSynchro;
        TextSnippetController.OnRomeoAnimation += StartRomeoSynchro;
        TextSnippetController.OnReplayStarts += Replaystarts;
        TextSnippetController.OnRomeoSkips += SkipRomeo;
        ActorManager.OnActorChanged += SetJuliaSource;
    }


    private void OnDisable()
    {
        TextSnippetController.OnActorResponse -= StartJuliaSynchro;
        TextSnippetController.OnRomeoAnimation -= StartRomeoSynchro;
        TextSnippetController.OnReplayStarts -= Replaystarts;
        TextSnippetController.OnRomeoSkips -= SkipRomeo;
        ActorManager.OnActorChanged -= SetJuliaSource;
    }

    private void Replaystarts()
    {
        isReplay = true;
    }

    private void StartJuliaSynchro(EmotionAnswerWrapper wrapper)
    {
        //if(wrapper.myClip != null)
        //{
        if (isReplay)
        {
            julia.clip = wrapper.myClip;
            julia.Play();
        }
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

    private void SkipRomeo()
    {
        romeo.Stop();
    }
    // Start is called before the first frame update6
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheerHandler : MonoBehaviour
{
    // Start is called before the first frame update

   

    [SerializeField] private AudioClip[] CheerVariants;
    private AudioSource[] audios;

    private void Start()
    {
        SetupAudioStructure();
    }

    public void PlayRandomCheer()
    {
        int random = Random.Range(0, CheerVariants.Length);
        PlayAudioOnFirstFreeAvailable(CheerVariants[random].name, CheerVariants[random]);
    }

    private void SetupAudioStructure()
    {
        audios = new AudioSource[2];
        for (int i = 0; i < audios.Length; i++)
        {
            audios[i] = this.gameObject.AddComponent<AudioSource>();
        }
    }

    private void PlayAudioOnFirstFreeAvailable(string _name, AudioClip myClip)
    {
        if (!audios[0].isPlaying)
        {
            if (audios[0].clip != audios[0].clip) audios[0].clip = myClip;
            audios[0].Play();
        }
        else if (audios[0].isPlaying && !audios[1].isPlaying)
        {
            if (audios[1].clip != audios[1].clip) audios[1].clip = myClip;
            audios[1].Play();
        }
        else
        {
            Debug.Log(_name + " could not been played on this Object: " + this.gameObject.name);
        }
    }

}

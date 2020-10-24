using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheerHandler : MonoBehaviour
{
    // Start is called before the first frame update

    #region Singleton
    static CheerHandler instance;
    public static CheerHandler Instance
    {
        get
        {
            if (!instance)
            {
                Debug.LogError("[SCENECONTROLLER]: i'm the instance, i do not exist.");
            }

            return instance;
        }
    }
    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

    }
    #endregion

    [SerializeField] private AudioClip[] CheerVariants;
    private AudioSource[] audios;

    private void Start()
    {
        SetupAudioStructure();
    }

    public void PauseAll()
    {
        for (int i = 0; i < audios.Length; i++)
        {
            StartCoroutine(FadeAudio(audios[i]));
        }
    }

    private IEnumerator FadeAudio(AudioSource myPlayer)
    {
        float temp = myPlayer.volume;
        while (myPlayer.volume >= 0.01)
        {
            myPlayer.volume -= 0.05f;
            yield return null;
        }
        myPlayer.Stop();
        myPlayer.volume = temp;
        yield return null;
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

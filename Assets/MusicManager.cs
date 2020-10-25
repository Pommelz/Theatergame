using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip[] musicClips = new AudioClip[4];
    AudioSource myPlayer;
    int songCounter;
    [SerializeField] float increaseRate = 0.05f;

    #region Singleton
    static MusicManager instance;
    public static MusicManager Instance
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

    private void Start()
    {
        myPlayer = GetComponent<AudioSource>();
        songCounter = 0;
        PlayNextSong();
    }

    public IEnumerator RepeatSongList()
    {
        while (myPlayer.isPlaying)
        {
            yield return null;
        }
        PlayNextSong();
        yield return null;
    }

    public void PlayNextSong()
    {
        StartCoroutine(FadeInMusic(musicClips[songCounter]));
        StartCoroutine(RepeatSongList());
    }

    private IEnumerator FadeInMusic(AudioClip _newClip)
    {
        float temp = myPlayer.volume;
        myPlayer.clip = _newClip;
        myPlayer.volume = 0.01f;
        myPlayer.Play();
        while (myPlayer.volume <= temp)
        {
            myPlayer.volume += increaseRate;
            yield return null;
        }
        songCounter++;
        yield return null;
    }

    public void PlayFinalTheme()
    {
        myPlayer.clip = musicClips[musicClips.Length - 1];
        myPlayer.Play();
    }

    public IEnumerator FadeOutMusic()
    {
        float temp = myPlayer.volume;
        while (myPlayer.volume >= 0.01f)
        {
            myPlayer.volume -= increaseRate;
            yield return null;
        }
        myPlayer.Stop();
        myPlayer.volume = temp;
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip[] musicClips = new AudioClip[4];
    AudioSource myPlayer;

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

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion


    public void PlayFinalTheme()
    {
        myPlayer.clip = musicClips[musicClips.Length - 1];
        myPlayer.Play();
    }

    public IEnumerator FadeMusic()
    {
        while(myPlayer.volume >= 0.01)
        {
            myPlayer.volume -= 0.05f;
            yield return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    #region Singleton
    static SFXManager instance;
    public static SFXManager Instance
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

    [SerializeField] AudioSource btnSound;
    [SerializeField] AudioSource promptSound;
    [SerializeField] AudioSource bellSound;
    [SerializeField] AudioSource footstepSound;

    // Start is called before the first frame update

    public void PlayButtonClick()
    {
        btnSound.Play();
    }
    public void PlayPromptClick()
    {
        promptSound.Play();
    }
    public void PlayBellClick()
    {
        bellSound.Play();
    }
    public void ToggleFootstepClick()
    {
        if (footstepSound.isPlaying) footstepSound.Stop();
        else if (!footstepSound.isPlaying) footstepSound.Play();
    }

}

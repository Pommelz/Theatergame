using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightController : MonoBehaviour
{

    #region Singleton
    static SpotlightController instance;
    public static SpotlightController Instance
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

    public Light[] spotlights;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;
        FollowObject(target);
    }

    public void SetSpotlightsActive(bool _SetActive, int _amount)
    {
        for (int i = 0; i < spotlights.Length; i++)
        {
            if (i <= _amount)
                spotlights[i].enabled = _SetActive;
        }
    }

    public void FollowObject(Transform _cameraTarget)
    {
        for (int i = 0; i < spotlights.Length; i++)
        {
            spotlights[i].transform.LookAt(_cameraTarget);
        }
    }
}

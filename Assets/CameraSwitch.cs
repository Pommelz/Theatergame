using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    #region Singleton
    static CameraSwitch instance;
    public static CameraSwitch Instance
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

    private Camera activeCamera;
    [SerializeField] private Camera[] availableCameras;
    private CameraFade[] fadeTargets;
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        fadeTargets = new CameraFade[availableCameras.Length];
        for (int i = 0; i < fadeTargets.Length; i++)
        {
            fadeTargets[i] = availableCameras[i].GetComponent<CameraFade>();
            Debug.Log(fadeTargets[i]);
        }
        activeCamera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) ToggleCamera();
    }

    public void ToggleCamera()
    {
        if(activeCamera == availableCameras[0])
        {
            fadeTargets[0].ToggleFade();
            fadeTargets[1].ToggleFade();
            StartCoroutine(StartCameraSwitch());
        }else if(activeCamera == availableCameras[1])
        {
            fadeTargets[0].ToggleFade();
            fadeTargets[1].ToggleFade();
            StartCoroutine(StartCameraSwitch());
        }
    }

    private IEnumerator StartCameraSwitch()
    {
        yield return new WaitForSeconds(delay);
        if (activeCamera == availableCameras[0])
        {
            availableCameras[0].enabled = false;
            availableCameras[1].enabled = true;
            activeCamera = availableCameras[1];
            yield return null;
        }
        else if (activeCamera == availableCameras[1])
        {
            availableCameras[1].enabled = false;
            availableCameras[0].enabled = true;
            activeCamera = availableCameras[0];
            yield return null;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLightHandler : MonoBehaviour
{
    #region Singleton
    static HealthLightHandler instance;
    public static HealthLightHandler Instance
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

    int damage = 0;
    public float multiplier = 1f;
    public float targetIntensity = 2f;
    Light[] healthLights;
    Color startColor;

    // Start is called before the first frame update
    void Start()
    {
        healthLights = new Light[GetComponentsInChildren<Light>().Length];
        for (int i = 0; i < healthLights.Length; i++)
        {
            Debug.Log("HealthLight" + (i + 1));
            healthLights[i] = transform.Find("HealthLight" + (i + 1)).GetComponent<Light>();

        }
        startColor = healthLights[0].color;
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space)) TakeDamage();
    //}

    private IEnumerator Glow(Light _targetLight)
    {
        float temp = _targetLight.intensity;
        Debug.Log(temp);
        while (_targetLight.intensity < targetIntensity)
        {
            _targetLight.intensity += (Time.deltaTime * multiplier);
            yield return null;
        }
        while (_targetLight.intensity > temp)
        {
            _targetLight.intensity -= (Time.deltaTime * multiplier);
            yield return null;
        }
        _targetLight.intensity = temp;
        yield return null;
    }

    public void ResetDamage()
    {
        damage = 0;
        for (int i = 0; i < healthLights.Length; i++)
        {
            healthLights[i].color = startColor;
        }
    }

    public void TakeDamage()
    {
        if (damage < 3)
        {

            healthLights[damage].color = Color.red;
            SFXManager.Instance.PlayWrong();
            StartCoroutine(Glow(healthLights[damage]));
            damage++;

            if (damage == 3) ResetDamage();
        }
    }
}

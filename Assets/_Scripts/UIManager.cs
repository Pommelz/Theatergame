using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public Transform romeoPnl;
    private TextMeshProUGUI romeoText;
    public Transform Moodbar;

    private void OnEnable()
    {
        //TextSnippetController.OnActorText += SetJulietText;
        TextSnippetController.OnMoodIncrease += FillMoodbar;
    }

    private void OnDisable()
    {
        //TextSnippetController.OnActorText -= SetJulietText;
        TextSnippetController.OnMoodIncrease -= FillMoodbar;
    }


    void Start()
    {
        //romeoText = romeoPnl.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetRomeoText(string _text)
    {
        romeoPnl.gameObject.SetActive(true);
        romeoText.SetText(_text);
    }
    private void SetJulietText(string _text)
    {
        romeoPnl.gameObject.SetActive(false);
    }
    private void FillMoodbar(float _fill)
    {
        Moodbar.GetComponent<Image>().fillAmount += _fill;
    }
}

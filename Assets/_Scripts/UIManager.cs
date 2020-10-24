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

    private void OnEnable()
    {
        TextSnippetController.OnRomeoText += SetRomeoText;
        TextSnippetController.OnActorText += SetJulietText;
    }


    void Start()
    {
        romeoText = romeoPnl.GetChild(0).GetComponent<TextMeshProUGUI>();
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
}

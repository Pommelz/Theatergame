using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Collections.Enums;

public class UIManager : MonoBehaviour
{
    public Transform romeoPnl;
    public Transform actorPnl;

    private TextMeshProUGUI romeoText;
    private TextMeshProUGUI actorText;
    
    public Transform Moodbar;
    
    public Camera cam1;
    public Camera cam2;
    private bool isReplay = false;
    
    private void OnEnable()
    {
        TextSnippetController.OnRomeoAnimation += SetRomeoText;
        TextSnippetController.OnReplayStarts += StartReplay;
        TextSnippetController.OnActorResponse += SetJulietText;
        TextSnippetController.OnMoodIncrease += FillMoodbar;
        TextSnippetController.OnSplittedTextOccured += ActorTextSplit;
    }

    private void OnDisable()
    {
        //TextSnippetController.OnActorText -= SetJulietText;
        TextSnippetController.OnRomeoAnimation -= SetRomeoText;
        TextSnippetController.OnReplayStarts -= StartReplay;
        TextSnippetController.OnActorResponse -= SetJulietText;
        TextSnippetController.OnMoodIncrease -= FillMoodbar;
        TextSnippetController.OnSplittedTextOccured -= ActorTextSplit;
    }
    private void ActorTextSplit(string _text)
    {
        SetJulietText(_text);
    }

    private void StartReplay()
    {
        isReplay = true;
        SwitchCamera();
    }

    void Start()
    {
        romeoText = romeoPnl.GetChild(1).GetComponent<TextMeshProUGUI>();
        actorText = actorPnl.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchCamera();
        }
    }

    private void SetRomeoText(AnimationAnswerWrapper _wrapper)
    {
        if (isReplay)
        {
            actorPnl.gameObject.SetActive(false);
            romeoPnl.gameObject.SetActive(true);
            romeoText.SetText(_wrapper.responsePart);
        }
    }
    private void SetJulietText(EmotionAnswerWrapper _wrapper)
    {
        if (isReplay)
        {
            romeoPnl.gameObject.SetActive(false);
            actorPnl.gameObject.SetActive(true);
            actorText.SetText(_wrapper.responsePart);
        }
    }
    private void SetJulietText(string _text)
    {
        if (isReplay)
        {
            romeoPnl.gameObject.SetActive(false);
            actorPnl.gameObject.SetActive(true);
            actorText.SetText(_text);
        }
    }
    private void FillMoodbar(float _fill)
    {
        Moodbar.GetComponent<Image>().fillAmount += _fill;
    }

    private void SwitchCamera()
    {
        cam1.enabled = !cam1.enabled;
        cam2.enabled = !cam2.enabled;
    }

}

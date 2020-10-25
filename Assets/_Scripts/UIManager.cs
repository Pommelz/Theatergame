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
    public Transform bckstgRomeoPnl;

    public Transform creditsPnl;

    private TextMeshProUGUI romeoText;
    private TextMeshProUGUI actorText;

    public Transform Moodbar;
    public Transform CurtainAnim;

    public Camera cam1;
    public Camera cam2;
    private bool isReplay = false;

    private void OnEnable()
    {
        TextSnippetController.OnRomeoAnimation += SetRomeoText;
        TextSnippetController.OnReplayStarts += StartReplay;
        TextSnippetController.OnActorResponse += SetJulietText;
        TextSnippetController.OnMoodIncrease += FillMoodbar;
        TextSnippetController.OnSplittedTextOccured += JulietTextSplit;
        TextSnippetController.OnSplittedTextOccuredRomeo += RomeoTextSplit;
        TextSnippetController.OnRomeoSkips += RomeoSkips;
        TextSnippetController.OnPlayBackEnd += Credits;
    }

    private void OnDisable()
    {
        //TextSnippetController.OnActorText -= SetJulietText;
        TextSnippetController.OnRomeoAnimation -= SetRomeoText;
        TextSnippetController.OnReplayStarts -= StartReplay;
        TextSnippetController.OnActorResponse -= SetJulietText;
        TextSnippetController.OnMoodIncrease -= FillMoodbar;
        TextSnippetController.OnSplittedTextOccured -= JulietTextSplit;
        TextSnippetController.OnSplittedTextOccuredRomeo -= RomeoTextSplit;
        TextSnippetController.OnRomeoSkips -= RomeoSkips;
        TextSnippetController.OnPlayBackEnd -= Credits;
    }

    private void RomeoSkips()
    {
        bckstgRomeoPnl.gameObject.SetActive(false);
    }
    private void JulietTextSplit(string _text)
    {
        SetJulietText(_text);
    }
    private void RomeoTextSplit(string _text)
    {
        SetRomeoText(_text);
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

    private void SetRomeoText(AnimationAnswerWrapper _wrapper)
    {
        if (isReplay)
        {
            actorPnl.gameObject.SetActive(false);
            romeoPnl.gameObject.SetActive(true);
            romeoText.SetText(_wrapper.responsePart);
        }
        else
        {
            return;
            //Debug.Log("backstage");
            //bckstgRomeoPnl.gameObject.SetActive(true);
            //bckstgRomeoPnl.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(_wrapper.responsePart);
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
        else
        {
            bckstgRomeoPnl.gameObject.SetActive(false);
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
        else
        {
            bckstgRomeoPnl.gameObject.SetActive(false);
        }
    }
    private void SetRomeoText(string _text)
    {
        if (isReplay)
        {
            actorPnl.gameObject.SetActive(false);
            romeoPnl.gameObject.SetActive(true);
            romeoText.SetText(_text);
        }
        else
        {
            Debug.Log("Backstage romeo");
            bckstgRomeoPnl.gameObject.SetActive(true);
            bckstgRomeoPnl.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(_text);
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

    private void Credits()
    {
        actorPnl.gameObject.SetActive(false);
        romeoPnl.gameObject.SetActive(false);
        creditsPnl.gameObject.SetActive(true);
        //CurtainAnim.GetComponent<Animation>().Play();
    }

    public void BackToMainMenu()
    {
        SceneController.Instance.LoadScene(StringCollection.MAINMENU_SCENE);
    }

}

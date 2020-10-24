using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextSnippet : MonoBehaviour
{
    private TextSnippetController snippetController;
    [HideInInspector]
    public bool isRhyme;
    [HideInInspector]
    public int wrongAnsweriD;
    public EmotionAnswerWrapper myWrapper;
    public TextMeshProUGUI text;

    private void Awake()
    {
        snippetController = GameObject.Find("Canvas").GetComponent<TextSnippetController>();
        // text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnSnippetPressed()
    {
        snippetController.RoundEvaluation(isRhyme, myWrapper);
    }

    public void SetBttnText()
    {
       // Debug.Log(snippetController.gameObject.name /*+ text.gameObject.name */+ myWrapper.responseSmiley);

        text.SetText(snippetController.ReturnSmileyStringByEmotion(myWrapper.responseSmiley));
    }
}

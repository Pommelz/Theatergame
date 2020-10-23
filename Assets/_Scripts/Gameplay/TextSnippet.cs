using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSnippet : MonoBehaviour
{
    private TextSnippetController snippetController;
    [HideInInspector]
    public bool isRhyme;

    private void Start()
    {
        snippetController = GameObject.Find("Canvas").GetComponent<TextSnippetController>();
    }

    public void OnSnippetPressed()
    {
        snippetController.Evaluation(isRhyme);
    }
}

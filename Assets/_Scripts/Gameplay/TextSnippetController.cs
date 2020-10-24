﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collections.ListWrapping;
using UnityEngine.UI;
using TMPro;
using Collections.Enums;

public class TextSnippetController : MonoBehaviour
{
    public List<AnswerListWrapper> rhyme = new List<AnswerListWrapper>();
    public List<EmotionAnswerWrapper> wrongAnswers = new List<EmotionAnswerWrapper>();

    public Transform SnippetBttn;
    ObjectPool bttnPool;

    private List<EmotionAnswerWrapper> chosenText = new List<EmotionAnswerWrapper>();
    private List<Transform> currBttns = new List<Transform>();
    private int roundCount = 0;

    void Start()
    {
        bttnPool = new ObjectPool(SnippetBttn.gameObject, 3, true);
        StartRound();
    }

    private void StartRound()
    {
        int randomRhyme = Random.Range(0, rhyme.Count);
        RomeoText();
    }

    private void SpawnResponseBttns(int round)
    {
        int r = Random.Range(0, 3);

        //TODO: struct
        EmotionAnswerWrapper wrong1 = null;
        EmotionAnswerWrapper wrong2 = null;

        EmotionAnswerWrapper julietResponse = rhyme[roundCount].julietText;
        List<EmotionAnswerWrapper> answers = new List<EmotionAnswerWrapper>();

        switch (julietResponse.responseSmiley)
        {
            case Smiley.HAPPY:
                wrong1 = SpawnWrongBttnByEmotion(Smiley.SAD);
                wrong2 = SpawnWrongBttnByEmotion(Smiley.BANANA);
                break;
            case Smiley.SAD:
                wrong1 = SpawnWrongBttnByEmotion(Smiley.HAPPY);
                wrong1 = SpawnWrongBttnByEmotion(Smiley.BANANA);
                break;
            case Smiley.BANANA:
                wrong1 = SpawnWrongBttnByEmotion(Smiley.HAPPY);
                wrong2 = SpawnWrongBttnByEmotion(Smiley.SAD);
                break;
        }

        answers.Add(wrong1);
        answers.Add(wrong2);
        int randomJuliet = Random.Range(0, 3);

        for (int i = 0; i < 3; i++)
        {

            GameObject bttn = bttnPool.NextFree();
            TextSnippet ts = bttn.GetComponent<TextSnippet>();
            currBttns.Add(bttn.transform);

            if (i == randomJuliet)
            {
                ts.isRhyme = true;
                ts.myWrapper = julietResponse;
                bttn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(ReturnSmileyStringByEmotion(julietResponse.responseSmiley));
            }
            else
            {
                ts.isRhyme = false;
                Debug.Log(answers[0].responseSmiley);
                Debug.Log(answers[0].responsePart);
                bttn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(ReturnSmileyStringByEmotion(answers[0].responseSmiley));

                ts.myWrapper = answers[0];
                answers.Remove(answers[0]);

            }
        }
        roundCount++;
    }

    private void RomeoText()
    {
        string text = rhyme[roundCount].RomeoText;
        Debug.Log(text);

        //TODO: spawn snippets on romeo animation event
        SpawnResponseBttns(roundCount);
    }

    public string ReturnSmileyStringByEmotion(Smiley _emotion)
    {
        string smileystring = "";
        switch (_emotion)
        {
            case Smiley.HAPPY:
                smileystring = ":)";
                break;
            case Smiley.SAD:
                smileystring = ":(";
                break;
            case Smiley.BANANA:
                smileystring = ":P";
                break;
        }
        return smileystring;
    }

    private EmotionAnswerWrapper SpawnWrongBttnByEmotion(Smiley _wantedEmotion)
    {
        EmotionAnswerWrapper chosen = null;
        foreach (EmotionAnswerWrapper saw in wrongAnswers)
        {
            if (saw.responseSmiley == _wantedEmotion)
            {
                chosen = saw;
                break;
            }
        }
        if (chosen == null)
        {
            Debug.Log("chosen null");
        }
        //wrongAnswers.Remove(chosen);
        return chosen;
    }

    public void RoundEvaluation(bool _correctAnswer, EmotionAnswerWrapper _smileyanswerwrapper)
    {
        chosenText.Add(_smileyanswerwrapper);
        Debug.Log(_correctAnswer ? "correct" + _smileyanswerwrapper.responsePart : "wrong");
        foreach (Transform t in currBttns)
        {
            t.gameObject.SetActive(false);
        }

        currBttns.Clear();
        //TODO: Call this function after the actor is done
        if (roundCount < rhyme.Count -1)
            RomeoText();
        else
            foreach (EmotionAnswerWrapper s in chosenText)
            {
                Debug.Log(s.responsePart);
            }
        //RomeoText();
    }
}
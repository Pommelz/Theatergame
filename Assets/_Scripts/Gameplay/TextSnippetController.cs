using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collections.ListWrapping;
using UnityEngine.UI;
using TMPro;

public class TextSnippetController : MonoBehaviour
{
    public List<ListWrapper> rhyme = new List<ListWrapper>();
    public List<string> wrongAnswers = new List<string>();

    public Transform SnippetBttn;
    ObjectPool bttnPool;

    private ListWrapper activeRhyme;
    private List<string> chosenText = new List<string>();
    private List<Transform> currBttns = new List<Transform>();
    private int roundCount = 0;

    void Start()
    {
        bttnPool = new ObjectPool(SnippetBttn.gameObject, 3, true);
        StartRhymeRound();
    }

    private void StartRhymeRound()
    {
        int randomRhyme = Random.Range(0, rhyme.Count);
        activeRhyme = rhyme[randomRhyme];
        SpawnSnippetBttns(roundCount);
    }

    private void SpawnSnippetBttns(int round)
    {
        int r = Random.Range(0, 3);

        for (int i = 0; i < 3; i++)
        {
            GameObject bttn = bttnPool.NextFree();
            TextSnippet ts = bttn.GetComponent<TextSnippet>();

            currBttns.Add(bttn.transform);

            if (i == r)
            {
                ts.isRhyme = true;
                bttn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(activeRhyme.myList[round]);
            }
            else
            {
                ts.isRhyme = false;
                int randomWrongAnswer = Random.Range(0, wrongAnswers.Count);
                bttn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(wrongAnswers[randomWrongAnswer]);
                ts.wrongAnsweriD = randomWrongAnswer;

            }
        }
        roundCount++;
    }

    public void RoundEvaluation(bool _correctAnswer, int wrongAnswerID)
    {
        chosenText.Add(_correctAnswer ? activeRhyme.myList[roundCount - 1] : wrongAnswers[wrongAnswerID]);

        foreach (Transform t in currBttns)
        {
            t.gameObject.SetActive(false);
        }

        //TODO: Call this function after the actor is done
        if (roundCount < 3)
            SpawnSnippetBttns(roundCount);
        else
            foreach (string s in chosenText)
            {
                Debug.Log(s);
            }
    }
}

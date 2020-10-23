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

    private List<string> chosenTest = new List<string>();
    ObjectPool bttnPool;
    private ListWrapper activeRhyme;
    private int roundCount = 0;
    private List<Transform> currBttns = new List<Transform>();

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
        int r = Random.Range(0, 2);
        Debug.Log(r.ToString());

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

            }
        }

        roundCount++;

    }

    private void DefineBttnText(bool _correctAnswer)
    {

    }

    public void Evaluation(bool _correctAnswer)
    {
        Debug.Log(_correctAnswer ? "correct answer!" : "wrong answer");
        
        foreach(Transform t in currBttns)
        {
            t.gameObject.SetActive(false);
        }

        SpawnSnippetBttns(roundCount);
    }
}

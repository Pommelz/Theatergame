using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collections.ListWrapping;
using UnityEngine.UI;
using TMPro;
using Collections.Enums;
using System.Linq;

public class TextSnippetController : MonoBehaviour
{
    public List<AnswerListWrapper> rhyme = new List<AnswerListWrapper>();
    public List<EmotionAnswerWrapper> wrongAnswers = new List<EmotionAnswerWrapper>();

    public Transform SnippetBttn;
    ObjectPool bttnPool;

    private List<EmotionAnswerWrapper> chosenText = new List<EmotionAnswerWrapper>();
    private List<Transform> currBttns = new List<Transform>();
    private List<int> wrongAnswerIDs = new List<int>();

    private int roundCount = 0;
    private int wrongInputs = 0;

    private int maxChars = 186;
    string[] separatingStrings = { "/", "..." };
    private bool isPlayback = false;

    public delegate void SendMessage_EventType();
    public delegate void PassingFloat_EventType(float _float);
    public delegate void PassingRomeoAnswer_EventType(AnimationAnswerWrapper romeoanswer);
    public delegate void PassingAnimationTag_EventType(AnimationTag emotion);
    public delegate void PassingJulietResponse_EventType(EmotionAnswerWrapper wrapper);
    public delegate void PassingString_EventType(string _text);

    public static event PassingRomeoAnswer_EventType OnRomeoAnimation;
    public static event PassingFloat_EventType OnMoodIncrease;
    public static event SendMessage_EventType OnActorSwap;
    public static event PassingAnimationTag_EventType OnResponseEmotion;
    public static event SendMessage_EventType OnReplayStarts;
    public static event SendMessage_EventType OnRomeoSkips;
    public static event SendMessage_EventType OnPlayBackEnd;

    public static event PassingJulietResponse_EventType OnActorResponse;
    public static event PassingString_EventType OnSplittedTextOccured;
    public static event PassingString_EventType OnSplittedTextOccuredRomeo;

    private Coroutine RomeoSplits;


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
            case Smiley.LOVE:
                wrong1 = SpawnWrongResponseByEmotion(Smiley.SAD);
                wrong2 = SpawnWrongResponseByEmotion(Smiley.DRAMA);
                break;
            case Smiley.SAD:
                wrong1 = SpawnWrongResponseByEmotion(Smiley.LOVE);
                wrong2 = SpawnWrongResponseByEmotion(Smiley.DRAMA);
                break;
            case Smiley.DRAMA:
                wrong1 = SpawnWrongResponseByEmotion(Smiley.LOVE);
                wrong2 = SpawnWrongResponseByEmotion(Smiley.SAD);
                break;
        }

        answers.Add(wrong1);
        answers.Add(wrong2);

        int randomJuliet = Random.Range(0, 3);

        //spawn three bttns
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
                bttn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(ReturnSmileyStringByEmotion(answers[0].responseSmiley));

                ts.myWrapper = answers[0];
                answers.Remove(answers[0]);

            }
        }
        roundCount++;
    }

    private void RomeoText()
    {
        string text = rhyme[roundCount].RomeoText.responsePart;
        Debug.Log(text);
        if (isPlayback)
        {
            if (SplitText(text).Count > 1)
            {
                OnRomeoAnimation?.Invoke(rhyme[roundCount].RomeoText);
                RomeoSplits = StartCoroutine(RomeoSplittedText(SplitText(text)));
            }
            else
            {
                OnRomeoAnimation?.Invoke(rhyme[roundCount].RomeoText);
            }
        }
        else
        {
            OnRomeoAnimation?.Invoke(rhyme[roundCount].RomeoText);
            RomeoSplits = StartCoroutine(RomeoSplittedText(SplitText(text)));
        }

        //TODO: spawn snippets on romeo animation event
        StartCoroutine(TalkingDelay(false));
    }

    IEnumerator RomeoSplittedText(List<string> _text)
    {
        for (int i = 0; i < _text.Count; i++)
        {
            OnSplittedTextOccuredRomeo(_text[i]);
            yield return new WaitForSeconds(rhyme[roundCount].RomeoText.myClip ? rhyme[roundCount].RomeoText.myClip.length / _text.Count : 4f / _text.Count);
            yield return null;
        }
    }

    public string ReturnSmileyStringByEmotion(Smiley _emotion)
    {
        string smileystring = "";
        switch (_emotion)
        {
            case Smiley.LOVE:
                smileystring = ":)";
                break;
            case Smiley.SAD:
                smileystring = ":(";
                break;
            case Smiley.DRAMA:
                smileystring = ":P";
                break;
        }
        return smileystring;
    }

    private EmotionAnswerWrapper SpawnWrongResponseByEmotion(Smiley _wantedEmotion)
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

        //wrongAnswers.Remove(chosen);
        return chosen;
    }

    public void RoundEvaluation(bool _correctAnswer, EmotionAnswerWrapper _smileyanswerwrapper)
    {
        OnResponseEmotion?.Invoke(_smileyanswerwrapper.animation);

        //if (isPlayback)
        OnActorResponse?.Invoke(_smileyanswerwrapper);

        if (_correctAnswer)
        {
            float increase = 1f / (float)rhyme.Count;
            OnMoodIncrease?.Invoke(increase);
            CheerHandler.Instance.PlayRandomCheer();
        }
        else
        {
            HealthLightHandler.Instance.TakeDamage();
            wrongInputs++;
            wrongAnswerIDs.Add(roundCount);
            if (wrongInputs == 3)
            {
                OnActorSwap?.Invoke();
                wrongInputs = 0;
            }
        }

        chosenText.Add(_smileyanswerwrapper);
        foreach (Transform t in currBttns)
        {
            t.gameObject.SetActive(false);
        }

        currBttns.Clear();
        //TODO: Call this function after the actor is done
        if (roundCount < rhyme.Count)
            StartCoroutine(TalkingDelay(true));
        else
        {
            Debug.Log("end");
            isPlayback = true;
            OnActorSwap?.Invoke();
            TheaterPlayback();
        }
    }
    private IEnumerator TalkingDelay(bool isRomeoDelay)
    {
        bool skipped = false;
        if (isRomeoDelay)
        {
            yield return new WaitForSeconds(1.5f);
            //float delay = 1.5f;
            ////float delay = 0;
            ////if (chosenText.Count > 0)
            ////{
            ////    if (!isPlayback)
            ////        delay = chosenText[chosenText.Count - 1].myClip ? chosenText[chosenText.Count - 1].myClip.length : 4f;
            ////    else
            ////        delay = chosenText[roundCount].myClip ? chosenText[chosenText.Count - 1].myClip.length : 4f;
            ////}
            //while (delay > 0 && !skipped)
            //{
            //    delay -= Time.deltaTime;

            //    //if (Input.GetKeyDown(KeyCode.Return))
            //    //    skipped = true;
            //    yield return null;
            //}

            RomeoText();

        }
        else
        {
            float delay = 0;
            delay = rhyme[roundCount].RomeoText.myClip ? rhyme[roundCount].RomeoText.myClip.length : 4f;
            while (delay > 0 && !skipped)
            {
                delay -= Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    OnRomeoSkips?.Invoke();
                    if (RomeoSplits != null)
                        StopCoroutine(RomeoSplits);
                    skipped = true;
                }

                yield return null;
            }
            if (RomeoSplits != null)
                StopCoroutine(RomeoSplits);

            OnRomeoSkips?.Invoke();

            if (!isPlayback)
                SpawnResponseBttns(roundCount);

        }
        yield return null;
    }

    private void TheaterPlayback()
    {
        OnReplayStarts?.Invoke();
        StartCoroutine(PlayBack());
    }

    IEnumerator PlayBack()
    {
        roundCount = 0;
        RomeoText();
        roundCount++;

        for (int i = 0; i < chosenText.Count; i++)
        {
            if (wrongAnswerIDs.Contains(i))
                OnActorSwap?.Invoke();

            yield return new WaitForSeconds(rhyme[i].RomeoText.myClip ? rhyme[i].RomeoText.myClip.length : 4f);
            Debug.Log(chosenText[i].responsePart);
            List<string> splittedText = SplitText(chosenText[i].responsePart);
            if (splittedText.Count == 1)
            {
                OnResponseEmotion(chosenText[i].animation);
                OnActorResponse?.Invoke(chosenText[i]);
                yield return new WaitForSeconds(chosenText[i].myClip ? chosenText[i].myClip.length : 4f);
            }
            else
            {
                for (int j = 0; j < splittedText.Count; j++)
                {
                    OnResponseEmotion(chosenText[i].animation);
                    OnSplittedTextOccured?.Invoke(splittedText[j]);
                    yield return new WaitForSeconds(chosenText[i].myClip ? (chosenText[i].myClip.length) / splittedText.Count : 4f / splittedText.Count);
                    //List<string> sentences = splittedText.OfType<string>().ToList();
                }

            }
            //yield return new WaitForSeconds(chosenText[i].myClip ? chosenText[i].myClip.length : 4f);
            RomeoText();
            if (roundCount < rhyme.Count)
                roundCount++;
            else
            {
                OnPlayBackEnd?.Invoke();
            }
            yield return null;
        }
        yield return null;
    }
    private List<string> SplitText(string _text)
    {
        List<string> splitted = new List<string>();
        splitted = _text.Split('/').ToList();
        //if (_text.Length > maxChars)
        //{
        //    splitted = (_text.Split(separatingStrings, System.StringSplitOptions.None));
        //}
        return splitted;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collections.Enums;

public class ActorManager : MonoBehaviour
{
    private List<Actor> actors = new List<Actor>();
    int actorID = 0;
    public delegate void PassingAudioSource_EventType(AudioSource source);
    public static event PassingAudioSource_EventType OnActorChanged;

    void OnEnable()
    {
        TextSnippetController.OnActorSwap += SetNextActorActive;
        TextSnippetController.OnResponseEmotion += SetActorAnimation;
    }
    void OnDisable()
    {
        TextSnippetController.OnActorSwap -= SetNextActorActive;
        TextSnippetController.OnResponseEmotion -= SetActorAnimation;
    }

    void Start()
    {
        foreach (Transform t in this.transform)
        {
            if (t.GetComponent<Actor>())
                actors.Add(t.GetComponent<Actor>());
        }
        SetNextActorActive();
    }

    void SetNextActorActive()
    {
        if (actorID > 0)
            actors[(actorID - 1)].MoveBack();
        if (actorID < actors.Count - 1)
        {
            actors[actorID].MoveToStage();

        }
        else
        {
            actorID = 0;
            actors[actorID].MoveToStage();
        }
        OnActorChanged?.Invoke(actors[actorID].transform.GetChild(0).GetComponent<AudioSource>()); ;
        actorID++;
        //if (actorID == actors.Count)
        //{
        //    actorID = 0;
        //}
        Debug.Log(actorID.ToString());
    }

    void SetActorAnimation(AnimationTag _tag)
    {
        if (actorID - 1 >= 0)
            actors[actorID - 1].StartActorAnimation(_tag);
        else
            actors[actors.Count - 1].StartActorAnimation(_tag);

    }


}

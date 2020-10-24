using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collections.Enums;

public class Actor : MonoBehaviour
{
    Animator anim;
    Animator movementAnim;

    public bool isRomeo;

    public Transform stagePos;

    private void Awake()
    {
        anim = isRomeo ? GetComponent<Animator>() : transform.GetChild(0).GetComponent<Animator>();
        movementAnim = !isRomeo ? GetComponent<Animator>() : null;
    }

    private void OnEnable()
    {
        if (isRomeo)
            TextSnippetController.OnRomeoAnimation += StartActorAnimation;
    }

    private void OnDisable()
    {
        if (isRomeo)
            TextSnippetController.OnRomeoAnimation -= StartActorAnimation;
    }

    private void StartActorAnimation(AnimationAnswerWrapper romeoanswer)
    {
        int animationID = 0;

        switch (romeoanswer.animation)
        {
            case Collections.Enums.AnimationTag.ACCUSING:
                animationID = 1;
                break;
            case Collections.Enums.AnimationTag.DISPAIR:
                animationID = 2;
                break;
            case Collections.Enums.AnimationTag.DRAMA:
                animationID = 3;
                break;
            case Collections.Enums.AnimationTag.LOOKINGFORLINES:
                animationID = 4;
                break;
            case Collections.Enums.AnimationTag.TALKINGNORMAL:
                animationID = 5;
                break;
        }

        anim.SetInteger("animID", animationID);
        anim.SetTrigger("playAnim");
    }
    
    public void StartActorAnimation(AnimationTag animation)
    {
        int animationID = 0;

        switch (animation)
        {
            case Collections.Enums.AnimationTag.ACCUSING:
                animationID = 1;
                break;
            case Collections.Enums.AnimationTag.DISPAIR:
                animationID = 2;
                break;
            case Collections.Enums.AnimationTag.DRAMA:
                animationID = 3;
                break;
            case Collections.Enums.AnimationTag.LOOKINGFORLINES:
                animationID = 4;
                break;
            case Collections.Enums.AnimationTag.TALKINGNORMAL:
                animationID = 5;
                break;
        }

        anim.SetInteger("animID", animationID);
        anim.SetTrigger("playAnim");
    }

    public void MoveToStage()
    {
        movementAnim.enabled = true;
        movementAnim.SetTrigger("move");
        Debug.Log("moveToStage");
        //TODO play walking animation here
    }

    public void AnimSetPosition()
    {
        movementAnim.enabled = false;
        this.transform.position = stagePos.position;

        //Play text animation
    }
    public void MoveBack()
    {
        movementAnim.enabled = true;
        Debug.Log("moveBack");
        movementAnim.SetTrigger("moveback");
    }
}

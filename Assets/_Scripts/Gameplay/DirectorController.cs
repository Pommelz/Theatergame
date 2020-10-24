using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DirectorController : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        TextSnippetController.OnRomeoAnimation += StartRomeoAnimation;
    }

    private void OnDisable()
    {
        TextSnippetController.OnRomeoAnimation -= StartRomeoAnimation;
    }

    private void StartRomeoAnimation(AnimationAnswerWrapper romeoanswer)
    {
        int animationID = 0;

        switch (romeoanswer.animation)
        {
            case Collections.Enums.AnimationTag.ACCUSING:
                animationID = 1;
                break;
            case Collections.Enums.AnimationTag.BASE:
                animationID = 2;
                break;
            case Collections.Enums.AnimationTag.DISPAIR:
                animationID = 3;
                break;
            case Collections.Enums.AnimationTag.DRAMA:
                animationID = 4;
                break;
            case Collections.Enums.AnimationTag.LOOKINGFORLINES:
                animationID = 5;
                break;
            case Collections.Enums.AnimationTag.TALKINGNORMAL:
                animationID = 6;
                break;
        }

        anim.SetInteger("animID", animationID);
    }

    void Start()
    {

    }

    void Update()
    {

    }
}

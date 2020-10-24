using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Collections.Enums;

[System.Serializable]
public class EmotionAnswerWrapper
{
    public string responsePart;
    public Smiley responseSmiley;
    public AnimationTag animation;
}

[System.Serializable]
public class AnimationAnswerWrapper
{
    public string responsePart;
    public AnimationTag animation;
}

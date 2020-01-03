using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "DialogData", menuName = "ScriptableObjects/Dialog")]
public class DialogData : ScriptableObject
{
    [Header("Dialog Item:")]
    public string title;
    public Sprite teksture;

    [Header("Begin start dialog:")]
    public string firstName;
    [TextArea(3, 5)] public string shortDescription;
    [TextArea(5,20)] public string longDescritorin;
    [TextArea(5, 20)] public string longDescritorinInSpanish;

    [Header("Main dialog:")]
    public VideoClip idleVid;
    public Conversation conversation;
}
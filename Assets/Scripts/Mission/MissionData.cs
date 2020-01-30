using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "MissionData", menuName = "ScriptableObjects/Mission")]
public class MissionData : ScriptableObject
{
    public Sprite icon;
    public string title;

    public List<PersonDialog> personDialogs;
}

[Serializable]
public struct PersonDialog
{
    public string personName;
    public Conversation conversation;
}


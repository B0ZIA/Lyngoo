using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionData", menuName = "ScriptableObjects/Mission")]
public class MissionData : ScriptableObject
{
    public Sprite icon;
    public string title;
}

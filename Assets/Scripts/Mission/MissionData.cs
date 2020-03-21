using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "MissionData", menuName = "ScriptableObjects/Mission")]
public class MissionData : ScriptableObject
{
    public Sprite icon;
    public string title;

    public GameObject mapPrefab;

    public List<PersonDialog> personDialogs;

    public PersonDialog FindPersonDialog(string personName)
    {
        foreach (var person in personDialogs)
        {
            if (person.personName == personName)
            {
                return person;
            }
        }

        Debug.LogError("I can not find person with this name: " + personName);
        return null;
    }
}

[Serializable]
public class PersonDialog
{
    public string personName;
    public Conversation conversation;
    public bool isComplete;
}


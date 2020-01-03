using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dictionary", menuName = "ScriptableObjects/Dictionary")]
public class DictionaryData : ScriptableObject
{
    public string dictionaryName;
    public bool isComplite = false;

    public List<Word> knowWords;
}

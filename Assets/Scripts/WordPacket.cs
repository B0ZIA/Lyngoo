using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WordPacked", menuName = "ScriptableObjects/WordPacked")]
public class WordPacked : ScriptableObject
{
    public List<Word> words;
}

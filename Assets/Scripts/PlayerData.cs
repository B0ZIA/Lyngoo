using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    public static List<Word> knowWords;

    [SerializeField]
    private DictionaryData dictionary;

    public List<Word> GetKnowWords()
    {
        return dictionary.knowWords;
    }

    public void Awake()
    {
        if (Instance == null)
            Instance = this;

        knowWords = dictionary.knowWords;
    }

    public void SaveWod(Word word)
    {
        if (dictionary.knowWords.Contains(word) == false)
            dictionary.knowWords.Add(word);
    }
}

[Serializable]
public struct Word
{
    public string inSpanish;
    public string inPoland;

    public Word(string inSpanish, string inPoland)
    {
        this.inSpanish = inSpanish;
        this.inPoland = inPoland;
    }
}


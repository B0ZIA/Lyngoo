using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TranslateSystem
{
    const string SPACE = " ";
    const string DOT = ".";
    const string COMMA = ",";
    const string EXTRA_START = "<color=green>";
    const string EXTRA_END = "</color>";

    public static string TranslateToPolishByWords(string source, List<Word> words)
    {
        string toTranslate = " " + source;


        for (int i = 0; i < words.Count; i++)
        {
            toTranslate = ChangeWords(toTranslate, words[i].inSpanish, words[i].inPoland);

            toTranslate = ChangeWordsWithDotAtEnd(toTranslate, words[i].inSpanish, words[i].inPoland);

            toTranslate = ChangeWordsWithCommaAtEnd(toTranslate, words[i].inSpanish, words[i].inPoland);
        }

        return toTranslate;
    }

    private static string ChangeWords(string source, string oldWord, string newWord)
    {
        string oldValue = SPACE + oldWord + SPACE;
        string newValue = EXTRA_START + SPACE + newWord + EXTRA_END + SPACE;

        string result = source.Replace(oldValue, newValue);

        return result;
    }

    private static string ChangeWordsWithDotAtEnd(string source, string oldWord, string newWord)
    {
        string oldValue = SPACE + oldWord + DOT;
        string newValue = EXTRA_START + SPACE + newWord + EXTRA_END + DOT;

        string result = source.Replace(oldValue, newValue);

        return result;
    }

    private static string ChangeWordsWithCommaAtEnd(string source, string oldWord, string newWord)
    {
        string oldValue = SPACE + oldWord + COMMA;
        string newValue = EXTRA_START + SPACE + newWord + EXTRA_END + COMMA;

        string result = source.Replace(oldValue, newValue);

        return result;
    }
}

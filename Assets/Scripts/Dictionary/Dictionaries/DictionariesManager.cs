using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionariesManager : MonoBehaviour
{
    public static DictionariesManager Instance;

    private static Vocabulary lastClickedDictionary;
    private DictionariesController controller;



    private void Start()
    {
        if (Instance == null)
            Instance = this;
        controller = GetComponent<DictionariesController>();
    }

    public static Vocabulary GetLastClicked()
    {
        return lastClickedDictionary;
    }

    public static void SetLastClicked(Vocabulary dictionary)
    {
        lastClickedDictionary = dictionary;
    }

    public static void RemoveLastClicked()
    {
        lastClickedDictionary = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryController : MonoBehaviour
{
    public static DictionaryController Instance;

    [SerializeField]
    private GameObject dictionariesListField;
    [SerializeField]
    private GameObject ficheField;



    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ShowFiche()
    {
        dictionariesListField.SetActive(false);
        ficheField.SetActive(true);
    }

    public void ShowList()
    {
        dictionariesListField.SetActive(true);
        ficheField.SetActive(false);
    }
}

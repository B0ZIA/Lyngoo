using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField]
    private GameObject DictionaryArea;
    [SerializeField]
    private GameObject DialogsArea;
    [SerializeField]
    private GameObject MissionsArea;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ShowDictionary()
    {
        DictionaryArea.SetActive(true);
        DialogsArea.SetActive(false);
        MissionsArea.SetActive(false);
    }

    public void ShowDialogs()
    {
        DictionaryArea.SetActive(false);
        DialogsArea.SetActive(true);
        MissionsArea.SetActive(false);
    }

    public void ShowMissions()
    {
        DictionaryArea.SetActive(false);
        DialogsArea.SetActive(false);
        MissionsArea.SetActive(true);
    }
}

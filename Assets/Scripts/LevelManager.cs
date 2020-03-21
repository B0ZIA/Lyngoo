using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public FollowToTarget follow;

    [SerializeField]
    private GameObject dicotionaryTarget;
    [SerializeField]
    private GameObject dialogsTarget;
    [SerializeField]
    private GameObject missionsTarget;

    [SerializeField]
    private GameObject DictionaryArea;
    [SerializeField]
    private GameObject DialogsArea;
    [SerializeField]
    private GameObject MissionsArea;

    [SerializeField]
    private GameObject missions;
    [SerializeField]
    private GameObject mainMission;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ShowDictionary()
    {
        follow.target = dicotionaryTarget;
    }

    public void ShowDialogs()
    {
        follow.target = dialogsTarget;
    }

    public void ShowMissions()
    {
        follow.target = missionsTarget;

        missions.SetActive(true);
        mainMission.SetActive(false);
    }
}

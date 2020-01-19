using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionController : MonoBehaviour
{
    public static MissionController Instance;

    private static MissionData currentMisionData;
    private static Mission lastClicked;

    [SerializeField]
    private GameObject missionListField;
    [SerializeField]
    private GameObject mapField;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public static void LoadMisionData(MissionData data)
    {
        currentMisionData = data;
    }

    public static Mission GetLatClickedMision()
    {
        return lastClicked;
    }

    public static void SetLastClickedMission(Mission mission)
    {
        lastClicked = mission;
    }

    public static void RemoveLastClickedMission()
    {
        lastClicked = null;
    }

    public void LoadMap(MissionData data)
    {
        currentMisionData = data;
        missionListField.SetActive(false);
        mapField.SetActive(true);
        //MapController.Instance.Setup();
    }
}

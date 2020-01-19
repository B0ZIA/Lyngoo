using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{
    [SerializeField]
    private MissionData missionData;

    [SerializeField]
    private Image image;
    [SerializeField]
    private Text title;

    void Start()
    {
        if (missionData != null)
        {
            image.sprite = missionData.icon;
            image.color = Color.white;
            title.text = missionData.title;
        }
    }

    public void OnClick()
    {
        if (MissionController.GetLatClickedMision() == this)
        {
            MissionController.RemoveLastClickedMission();
            MissionController.Instance.LoadMap(missionData);
            return;
        }
        MissionController.SetLastClickedMission(this);
    }
}

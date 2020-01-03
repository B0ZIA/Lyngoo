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
}

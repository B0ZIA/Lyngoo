using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public static MapController Instance;

    [SerializeField]
    private MapNavigator navigator;

    private void Awake()
    {
        if (Instance = null)
            Instance = this;
    }

    public void Setup()
    {
        //TODO: Wszytać mapę z mision data jak będzie już ich kilka
    }
}

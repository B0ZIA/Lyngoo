using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BackgroudSlider : MonoBehaviour
{
    [SerializeField]
    private GameObject holders;
    [SerializeField]
    private List<MovingItem> movingItems = new List<MovingItem>();

    private bool zoom = false;



    public void SwipeRight()
    {
        if (holders.transform.localPosition.x < 1200 && !zoom)
        {
            foreach (var movingItem in movingItems)
            {
                movingItem.canvas.transform.Translate(Vector2.right * Time.deltaTime * (3 - movingItem.speed) / 5);
            }
        }
    }

    public void SwipeLeft()
    {
        if (holders.transform.localPosition.x > -1740 && !zoom)
        {
            foreach (var movingItem in movingItems)
            {
                movingItem.canvas.transform.Translate(-Vector2.right * Time.deltaTime * (3 - movingItem.speed) / 5);
            }
        }
    }

    public void Zoom(float x)
    {
        if (zoom == false)
        {
            zoom = true;

            for (int i = 0; i < movingItems.Count; i++)
            {
                movingItems[i].Zoom(x);
            }
        }
    }

    public void BreakZoom()
    {
        zoom = false;

        for (int i = 0; i < movingItems.Count; i++)
        {
            movingItems[i].ResetTransform();
        }
    }
}

[Serializable]
public class MovingItem
{
    public float speed;
    public GameObject canvas;

    private Vector2 _sizeDelta;
    private Vector3 _localPosition;
    private Vector2 _anchoredPosition;



    public void ResetTransform()
    {
        canvas.GetComponent<RectTransform>().sizeDelta = _sizeDelta;
        canvas.GetComponent<RectTransform>().localPosition = _localPosition;
        canvas.GetComponent<RectTransform>().anchoredPosition = _anchoredPosition;
    }

    private void SaveData()
    {
        _sizeDelta = canvas.GetComponent<RectTransform>().sizeDelta;
        _localPosition = canvas.GetComponent<RectTransform>().localPosition;
        _anchoredPosition = canvas.GetComponent<RectTransform>().anchoredPosition;
    }

    public void Zoom(float x)
    {
        SaveData();

        canvas.GetComponent<RectTransform>().sizeDelta = new Vector2(2400,530);
        canvas.GetComponent<RectTransform>().localPosition = new Vector3(x, 260,0);
        canvas.GetComponent<RectTransform>().anchoredPosition = new Vector3(x, 260, 0);
    }
}


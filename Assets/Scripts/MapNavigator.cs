using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapNavigator : MonoBehaviour
{
    [SerializeField]
    private BackgroudSlider slider;

    private Vector2 startPos = Vector2.zero;
    private bool scroll = false;
    private float distance;



    private void Update()
    {
        if (scroll)
        {
            distance = Input.mousePosition.x - startPos.x;

            if (distance > 50)
                slider.SwipeRight();

            if (distance < -50)
                slider.SwipeLeft();
        }
    }

    public void StartScrolling()
    {
        scroll = true;
        startPos = Input.mousePosition;
    }

    public void BreakScrolling()
    {
        scroll = false;
        startPos = Vector2.zero;
    }
}

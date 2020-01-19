using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapNavigator : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float zoomTargetDistance;

    [SerializeField]
    private GameObject dialogField;

    private bool moveRight = false;
    private bool moveLeft = false;
    private float speed = 1f;

    private bool zoom = false;



    private void Update()
    {
        if (zoom)
        {
            Debug.Log("Pozycja środka: "+ target.transform.localPosition.x);
            Debug.Log("Pozycja targetu: "+ zoomTargetDistance);

            if (target.transform.localPosition.x + zoomTargetDistance > 10)
            {
                moveRight = true;
                moveLeft = false;
            }
            else if (target.transform.localPosition.x + zoomTargetDistance < -10)
            {
                moveRight = false;
                moveLeft = true;
            }
            else
            {
                moveRight = false;
                moveLeft = false;
                dialogField.SetActive(true);
            }
        }

        if (moveRight)
            SwipeRight();
        else if (moveLeft)
            SwipeLeft();
    }

    private void SwipeRight()
    {
        if (target.transform.localPosition.x > -1450)
            target.transform.Translate(-Vector2.right * Time.deltaTime * speed);
    }

    private void SwipeLeft()
    {
        if (target.transform.localPosition.x < 1400)
            target.transform.Translate(Vector2.right * Time.deltaTime * speed);
    }

    public void LeftBtn()
    {
        moveRight = false;
        moveLeft = true;
    }

    public void RightBtn()
    {
        moveRight = true;
        moveLeft = false;
    }

    public void RestMoving()
    {
        moveRight = false;
        moveLeft = false;
    }

    public void ZoomOnGameObject(float distanceOfCenterMap)
    {
        zoomTargetDistance = distanceOfCenterMap;
        zoom = true;
    }
}

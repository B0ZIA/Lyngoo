using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPerson : Person
{
    public float distanceFromCenterMap;

    [SerializeField] private Text exhibitText;
    [SerializeField] private Image personImage;
    [SerializeField] private BackgroudSlider slider;



    public void OnClick()
    {
        RespondOnPlayer();
    }

    private void Update()
    {
        if (available)
        {
            personImage.color = Color.white;
        }
        else
        {
            personImage.color = Color.gray;
        }
    }

    public override void StartTalking()
    {
        slider.Zoom(distanceFromCenterMap);
        StartCoroutine(Saying("Kolego, zagrać ci coś?"));
        MissionController.Instance.OpenDialog(gameObject.name, exhibitText);
    }
}

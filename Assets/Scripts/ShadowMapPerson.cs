using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEditor;

public class ShadowMapPerson : MapPerson, IPerson
{
    public List<MapPerson> personsWhoMustBeComplete;
    [SerializeField]
    private Image personImage;



    protected override bool WantTalking()
    {
        if (personsWhoMustBeComplete != null)
        {
            foreach (var peson in personsWhoMustBeComplete)
            {
                if (!peson.IsComplete())
                {
                    return false;
                }
            }
        }
        return true;
    }

    public virtual void Update()
    {
        if (WantTalking())
        {
            personImage.color = Color.white;
        }
        else
        {
            personImage.color = Color.gray;
        }
    }
}
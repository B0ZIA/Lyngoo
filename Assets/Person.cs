using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Person : MonoBehaviour
{
    [SerializeField]
    private string givingUpPlayerText = "Nie mam teraz czasu";
    [SerializeField]
    private string shortDialogText = "Cześć, co tam?";
    [SerializeField]
    protected Text exhibitText;
    [SerializeField]
    protected bool available = false;
    [SerializeField]
    protected bool active = false;

    private bool isTalking = false;
    protected bool complete = false;


    protected void RespondOnPlayer()
    {
        if (complete)
        {
            StartCoroutine(Saying(givingUpPlayerText));
        }
        else
        {
            if (available)
            {
                if (!active)
                {
                    StartCoroutine(Saying(shortDialogText));
                }
                else
                {
                    StartTalking();
                }
            }
        }
    }

    protected IEnumerator Saying(string text)
    {
        isTalking = true;
        exhibitText.text = text;

        yield return new WaitForSecondsRealtime(3);

        exhibitText.text = "";
        isTalking = false;
    }

    public abstract void StartTalking();
}

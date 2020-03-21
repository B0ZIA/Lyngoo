using UnityEngine;

public class InterestedMapPerson : ShadowMapPerson, IPerson
{
    [SerializeField]
    private string shortDialogText = "Cześć, co tam?";

    public override void RespondOnPlayer()
    {
        if (!IsComplete())
        {
            if (WantTalking())
                StartTalking();
            else
            {
                StartCoroutine(Saying(shortDialogText));
            }
        }
        else
        {
            StartCoroutine(Saying(givingUpPlayerText));
        }

    }

    public override void Update()
    {
        ;
    }
}

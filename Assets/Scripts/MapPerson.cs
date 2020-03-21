using UnityEngine;

public class MapPerson : Person, IPerson
{
    public float distanceFromCenterMap;

    [SerializeField]
    protected string givingUpPlayerText = "Nie mam teraz czasu";
    [SerializeField]
    private BackgroudSlider slider;
    [SerializeField]
    private Animator anim;



    public void StartTalking()
    {
        slider.Zoom(distanceFromCenterMap);
        MissionController.Instance.OpenDialog(this);
    }

    public virtual void RespondOnPlayer()
    {
        if (!IsComplete())
        {
            if (WantTalking())
            {
                StartTalking();
            }
        }
        else
        {
            StartCoroutine(Saying(givingUpPlayerText));
        }
    }

    public void SetOutline(bool value)
    {
        if (anim != null)
        {
            anim.SetBool("outline", value);
        }
        else
        {
            Debug.LogError(gameObject.name + " do not have animator!");
        }
    }

    protected virtual bool WantTalking()
    {
        return true;
    }

    public bool IsComplete()
    {
        return MissionController.currentMisionData.FindPersonDialog(gameObject.name).isComplete;
    }

    void OnMouseDown()
    {
        RespondOnPlayer();
    }
}
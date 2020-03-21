using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Person : MonoBehaviour
{
    public ExhibitText.Position textPos;
    [SerializeField]
    private bool isTalking = false;



    public IEnumerator Saying(string text, int time = 3)
    {
        isTalking = true;
        ExhibitText.Instance.GetBackground().color = new Color32(0,0,0,100);
        ExhibitText.Instance.GetText(textPos).text = text;

        yield return new WaitForSecondsRealtime(time);

        ExhibitText.Instance.GetBackground().color = new Color32(0, 0, 0, 0);
        ExhibitText.Instance.GetText(textPos).text = "";
        isTalking = false;
    }

    
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    private DialogData data;

    [SerializeField]
    private Image image;
    [SerializeField]
    private Text title;



    public DialogData GetData()
    {
        return data;
    }

    public void Init(DialogData dialogData)
    {
        this.data = dialogData;
    }

    private void Start()
    {
        if (data != null)
        {
            image.sprite = data.teksture;
            image.color = Color.white;
            title.text = data.title;
        }
    }

    public void OnClick()
    {
        if (data != null)
        {
            if (DialogsManager.GetLastClicked() == this)
            {
                DialogController.Instance.OpenPersonDescription(this);
                DialogsManager.RemoveLastClicked();
                return;
            }
            DialogsManager.SetLastClicked(this);
        }
    }

}

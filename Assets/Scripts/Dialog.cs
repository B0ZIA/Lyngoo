using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    [SerializeField]
    private DialogData dialogData;

    [SerializeField]
    private Image image;
    [SerializeField]
    private Text title;



    private void Start()
    {
        if (dialogData != null)
        {
            image.sprite = dialogData.teksture;
            image.color = Color.white;
            title.text = dialogData.title;
        }
    }

    public void OnClick()
    {
        if (dialogData != null)
        {
            if (DialogsManager.lastClickDialog == this)
            {
                DialogsManager.Instance.CreateDialog(dialogData);
                DialogsManager.lastClickDialog = null;
                return;
            }
            DialogsManager.lastClickDialog = this;
        }
    }

}

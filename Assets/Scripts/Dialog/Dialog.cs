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
    [SerializeField]
    private GameObject locker;



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

            if (data.locked)
            {
                LockButton();
            }
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

    private void LockButton()
    {
        image.GetComponent<Button>().interactable = false;
        locker.SetActive(true);
        title.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogsManager : MonoBehaviour
{
    public static DialogsManager Instance;

    public static Dialog lastClickDialog;

    [SerializeField]
    private GameObject listOfDialogs;
    [SerializeField]
    private GameObject openedDialog;

    [SerializeField]
    private Image wallpaper;

    private void Start()
    {
        Instance = this;
    }

    public void CreateDialog(DialogData dialogData)
    {
        ShowDialog();

        wallpaper.sprite = dialogData.teksture;
    }

    private void ShowDialog()
    {
        listOfDialogs.SetActive(false);
        openedDialog.SetActive(true);
    }
}

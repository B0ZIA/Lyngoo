using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogsManager : MonoBehaviour
{
    public static DialogsManager Instance;

    private static Dialog lastClickedDialog;
    private DialogsController controller;

    

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        controller = GetComponent<DialogsController>();
        LoadDialogs();
    }

    public static Dialog GetLastClicked()
    {
        return lastClickedDialog;
    }

    public static void SetLastClicked(Dialog dialog)
    {
        lastClickedDialog = dialog;
    }

    public static void RemoveLastClicked()
    {
        lastClickedDialog = null;
    }

    public void LoadDialogs()
    {
        DialogData[] dialogsData = Resources.LoadAll<DialogData>("Dialogs");

        for (int i = 0; i < dialogsData.Length; i++)
        {
            controller.CreateDialog(dialogsData[i]);
        }
    }
}

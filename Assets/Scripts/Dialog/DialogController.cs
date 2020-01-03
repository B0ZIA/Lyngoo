using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public static DialogController Instance;
    private static Dialog currentDialog;

    [SerializeField]
    private DialogComponents components;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public DialogComponents GetComponents()
    {
        return components;
    }

    public static Dialog GetDialog()
    {
        return currentDialog;
    }

    public void ShowDialogs()
    {
        components.dialogsField.SetActive(true);
        components.descriptionField.SetActive(false);
        components.dialogField.SetActive(false);
    }

    public void OpenPersonDescription(Dialog dialog)
    {
        components.dialogsField.SetActive(false);
        components.descriptionField.SetActive(true);
        components.dialogField.SetActive(false);

        currentDialog = dialog;
        PersonDescriptionController.Instance.LoadData(dialog.GetData());
        PersonDescriptionController.Instance.SetTranslator();

    }

    public void OpenMainDialog()
    {
        components.dialogsField.SetActive(false);
        components.descriptionField.SetActive(false);
        components.dialogField.SetActive(true);
    }
}

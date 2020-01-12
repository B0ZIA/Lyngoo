using UnityEngine;

public class PersonDescriptionController : MonoBehaviour
{
    public static PersonDescriptionController Instance;

    [SerializeField]
    private PersonDescriptionComponents components;
    [SerializeField]
    private PersonDescriptionTranslator translator;




    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void SetTranslator()
    {
        Translator.translator = translator;
    }

    public void LoadData(DialogData dialogData)
    {
        components.image.sprite = dialogData.teksture;
        components.nick.text = dialogData.firstName;
        components.shortDescription.text = dialogData.shortDescription;
        SetLongDescription(dialogData.longDescritorin);
    }

    public void SetLongDescription(string longDescription)
    {
        components.longDescription.text = longDescription;
    }

    public void OnFinishTranslate()
    {
        DialogController.Instance.OpenMainDialog();
        MainDialogController.Instance.Setup();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogsController : MonoBehaviour
{
    private DialogsComponents components;



    private void Awake()
    {
        components = GetComponent<DialogsComponents>();
    }

    public void CreateDialog(DialogData data)
    {
        GameObject prefab = Resources.Load<GameObject>("Dialog");
        GameObject dialog = Instantiate(prefab, components.content);
        dialog.GetComponent<Dialog>().Init(data);
    }
}

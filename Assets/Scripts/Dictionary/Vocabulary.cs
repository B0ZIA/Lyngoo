using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vocabulary : MonoBehaviour
{
    private DictionaryData data;

    [SerializeField]
    private Text title;



    public void Init(DictionaryData data)
    {
        this.data = data;
        if (data.isComplite)
            title.text = data.dictionaryName + " <color=green>(Complite)</color>";
        else
            title.text = data.dictionaryName;
    }

    public DictionaryData GetData()
    {
        return data;
    }

    public void SetCompliteTitle()
    {
        title.text += " <color=green>(Complite)</color>";
    }

    public void OnClick()
    {
        if (data != null && !data.isComplite)
        {
            if (DictionariesManager.GetLastClicked() == this)
            {
                DictionaryController.Instance.ShowFiche();
                FicheController.Instance.Setup(data);
                DictionariesManager.RemoveLastClicked();
                return;
            }
            DictionariesManager.SetLastClicked(this);
        }
    }
}

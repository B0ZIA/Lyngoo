using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionariesController : MonoBehaviour
{
    private DictionariesComponents components;



    private void Start()
    {
        components = GetComponent<DictionariesComponents>();

        LoadDictionaries();
    }

    

    public void LoadDictionaries()
    {
        DictionaryData[] dictionariesData = Resources.LoadAll<DictionaryData>("Dictionaries/Pack");

        for (int i = 0; i < dictionariesData.Length; i++)
        {
            CreateDictionary(dictionariesData[i]);
        }
    }

    public void CreateDictionary(DictionaryData data)
    {
        GameObject prefab = Resources.Load<GameObject>("Dictionary");
        GameObject dictionary = Instantiate(prefab, components.content);
        dictionary.GetComponent<Vocabulary>().Init(data);
    }
}

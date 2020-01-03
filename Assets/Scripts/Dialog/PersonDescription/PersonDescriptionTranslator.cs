using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonDescriptionTranslator : MonoBehaviour, ITranslator
{
    private int progress = 0;
    private PersonDescriptionController controller;



    private void Start()
    {
        controller = GetComponent<PersonDescriptionController>();
    }

    private void TranslatePartThatPlayerUnderstend()
    {
        string source = DialogController.GetDialog().GetData().longDescritorin;

        List<Word> words = new List<Word>();

        for (int i = 0; i < PlayerData.knowWords.Count; i++)
        {
            if (Random.Range(0, 100) <= 30)
                words.Add(PlayerData.knowWords[i]);
        }

        string translate = TranslateSystem.TranslateToPolishByWords(source, words);

        controller.SetLongDescription(translate);
    }

    private void TranslateThatPlayerUnderstend()
    {
        string source = DialogController.GetDialog().GetData().longDescritorin;

        string translate = TranslateSystem.TranslateToPolishByWords(source, PlayerData.knowWords);

        controller.SetLongDescription(translate);
    }

    private void TranslateAll()
    {
        string translate = DialogController.GetDialog().GetData().longDescritorinInSpanish;

        controller.SetLongDescription("<color=green>" + translate + "</color>");
    }

    public void Translate()
    {
        if (progress == 0)
        {
            TranslatePartThatPlayerUnderstend();
        }
        else if (progress == 1)
        {
            TranslateThatPlayerUnderstend();
        }
        else if (progress == 2)
        {
            TranslateAll();
        }
        else if (progress == 3)
        {
            controller.OnFinishTranslate();
            progress = 0;
            return;
        }

        progress++;
    }
}

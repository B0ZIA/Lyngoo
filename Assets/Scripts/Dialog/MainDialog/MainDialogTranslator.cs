using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDialogTranslator : MonoBehaviour, ITranslator
{
    private MainDialogController controller;


    private void Start()
    {
        controller = GetComponent<MainDialogController>();
    }

    public void Translate()
    {
        var sentences = controller.GetSentences();
        for (int i = 0; i < sentences.Count; i++)
        {
            StartCoroutine(Translating(sentences[i]));
        } 
    }

    IEnumerator Translating(Sentence sentence)
    {
        sentence.TranslateToPolish();
        yield return new WaitForSecondsRealtime(2);
        sentence.TranslateToSpanish();
    }
}

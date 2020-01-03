using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sentence : MonoBehaviour
{
    [SerializeField]
    private ConversationItem item;

    [SerializeField]
    private string sentenceAnSpanish;
    [SerializeField]
    private string sentenceAnPolish;

    [SerializeField]
    private Text sentenceText;



    public void Init(ConversationItem item)
    {
        this.item = item;

        this.sentenceAnSpanish = item.Text;
        sentenceText.text = sentenceAnSpanish;

        this.sentenceAnPolish = item.Text;
    }

    public void OnClick()
    {
        MainDialogController.Instance.ReplyToSim(item);
    }

    public void TranslateToPolish()
    {
        sentenceText.text = "<color=green>"+sentenceAnPolish+"</color>";
    }

    public void TranslateToSpanish()
    {
        sentenceText.text = sentenceAnSpanish;
    }
}

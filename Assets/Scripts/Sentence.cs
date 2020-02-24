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

    public delegate void Reply(ConversationItem item);
    public event Reply ReplyToSim;


    public void Init(ConversationItem item)
    {
        this.item = item;

        this.sentenceAnSpanish = item.TextInSpanish;

        this.sentenceAnPolish = item.TextInPolish;
        sentenceText.text = sentenceAnPolish;
    }

    public void OnClick()
    {
        if (ReplyToSim != null)
            ReplyToSim.Invoke(item);
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

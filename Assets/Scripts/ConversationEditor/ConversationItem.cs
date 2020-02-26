using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Audio;


[Serializable]
public class ConversationItem
{
    public ItemType type = ItemType.Sentence;
    [SerializeField]
    public string TextInPolish;
    [SerializeField]
    private string Text;
    [SerializeField] 
    public string TextInSpanish;
    public RuntimeAnimatorController animator;
    public AudioClip[] audioClip;
    public string ID;
    [HideInInspector]
    public Rect Box;

    public ConversationItem(Vector2 position, float width, float height, string textInPolish)
    {
        Box = new Rect(position, new Vector2(325, height));
        TextInPolish = textInPolish;
        ID = Guid.NewGuid().ToString();
    }

    public void Paint()
    {
#if UNITY_EDITOR
        if (type == ItemType.Answer)
        {
            Handles.DrawSolidRectangleWithOutline(Box, ItemController.answerBoxColor, ItemController.answerBoxColor);
        }
        else if (type == ItemType.Sentence)
        {
            Handles.DrawSolidRectangleWithOutline(Box, ItemController.sentenceBoxColor, ItemController.sentenceBoxColor);
        }

        var centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.UpperCenter;
        centeredStyle.normal.textColor = Color.white;
        centeredStyle.fontStyle = FontStyle.Bold;

        if (TextInPolish == "")
        {
            TextInPolish = Text;
        }

        if (TextInPolish != null)
        {
            if (TextInPolish.Length > 0)
                Box.width = TextInPolish.Length * 8;
    }
    
        Box.height = 40;
        if (TextInSpanish == "")
        {
            Box.height /= 2;
        }
        GUI.Label(Box, TextInPolish, centeredStyle);

        if (TextInSpanish != "")
        {
            var secondLine = Box;
            var style = centeredStyle;
            style.normal.textColor = Color.green;
            secondLine.y += 20;
            GUI.Label(secondLine, TextInSpanish, style);
        }
#endif
    }

    public void SetupChildrenType(ItemType parentType)
    {
        if (parentType == ItemType.Sentence)
            type = ItemType.Answer;
        else if (parentType == ItemType.Answer)
            type = ItemType.Sentence;
    }

    public void Move()
    {
        Box.position = Event.current.mousePosition - new Vector2(Box.width / 2, Box.height / 2);
    }



    public enum ItemType
    {
        Sentence = 0,
        Answer = 1
    }
}
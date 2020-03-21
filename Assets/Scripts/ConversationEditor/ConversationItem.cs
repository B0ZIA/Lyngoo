using System;
using UnityEditor;
using UnityEngine;

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


#if UNITY_EDITOR
    public ConversationItem(Vector2 position, float width, float height, string textInPolish)
    {
        Box = new Rect(position, new Vector2(325, height));
        TextInPolish = textInPolish;
        ID = Guid.NewGuid().ToString();
    }

    public void Paint()
    {
        PaintBox();
        GUI.Label(Box, TextInPolish, ItemGUIStyle());
        SetPolishText();
        SetBoxHeight();

        if (TextInSpanish != "")
        {
            DrawSecondLine();
        }
    }

    private void DrawSecondLine()
    {
        Rect secondLine = Box;
        secondLine.y += 20;

        var style = ItemGUIStyle();
        style.normal.textColor = Color.green;
        GUI.Label(secondLine, TextInSpanish, style);
    }

    private void SetPolishText()
    {
        if (TextInPolish != null)
        {
            if (TextInPolish.Length > 0)
                Box.width = TextInPolish.Length * 8;
        }
    }

    private void SetBoxHeight()
    {
        Box.height = 40;
        if (TextInSpanish == "")
        {
            Box.height /= 2;
        }
    }

    private static GUIStyle ItemGUIStyle()
    {
        var centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.UpperCenter;
        centeredStyle.normal.textColor = Color.white;
        centeredStyle.fontStyle = FontStyle.Bold;
        return centeredStyle;
    }

    private void PaintBox()
    {
        if (type == ItemType.Answer)
        {
            Handles.DrawSolidRectangleWithOutline(Box, ItemController.answerBoxColor, ItemController.answerBoxColor);
        }
        else if (type == ItemType.Sentence)
        {
            Handles.DrawSolidRectangleWithOutline(Box, ItemController.sentenceBoxColor, ItemController.sentenceBoxColor);
        }
    }

    public void PaintAsMaster()
    {
        Handles.DrawSolidRectangleWithOutline(Box, ItemController.masterBoxColor, ItemController.masterBoxColor);

        GUI.Label(Box, TextInPolish, ItemGUIStyle());
        SetPolishText();
        SetBoxHeight();

        if (TextInSpanish != "")
        {
            DrawSecondLine();
        }
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
#endif



    public enum ItemType
    {
        Sentence = 0,
        Answer = 1,
    }
}
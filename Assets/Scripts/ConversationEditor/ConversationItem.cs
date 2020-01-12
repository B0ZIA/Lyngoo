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
    public string Text;
    public RuntimeAnimatorController animator;
    public AudioClip[] audioClip;
    public string ID;
    [HideInInspector]
    public Rect Box;

    public ConversationItem(Vector2 position, float width, float height, string text)
    {
        Box = new Rect(position, new Vector2(width, height));
        Text = text;
        ID = Guid.NewGuid().ToString();
    }

    public void Paint()
    {
#if UNITY_EDITOR
        if (type == ItemType.Answer)
        {
            Handles.DrawSolidRectangleWithOutline(Box, Color.cyan, Color.grey);
        }
        else if (type == ItemType.Sentence)
        {
            Handles.DrawSolidRectangleWithOutline(Box, Color.magenta, Color.grey);
        }

        var centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.MiddleCenter;
        GUI.Label(Box, Text, centeredStyle);
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
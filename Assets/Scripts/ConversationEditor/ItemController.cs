using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController
{
    public ItemEditor itemEditor;
    public ConversationItem movedItem = null;
    private Conversation conversation = null;
    public bool movingItem = false;

    public static Color sentenceBoxColor = new Color32(78, 28, 28, 255);
    public static Color answerBoxColor = new Color32(34, 41, 48, 255);
    public static Color masterBoxColor = new Color32(153, 101, 21, 255);



    public ItemController()
    {
        itemEditor = new ItemEditor();
    }

    public void Setup(Conversation conversation)
    {
        this.conversation = conversation;
    }

    public void PaintItems()
    {
#if UNITY_EDITOR

        if (conversation != null)
        {
            foreach (var item in conversation.Items)
            {
                if (conversation.GetMasterItem() == item)
                {
                    item.PaintAsMaster();
                }
                else
                {
                    item.Paint();
                }
            }
        }
#endif
    }

    public void CreateItem(object obj)
    {
#if UNITY_EDITOR

        Vector2 mousePosition = (Vector2)obj;
        ConversationItem item = new ConversationItem(mousePosition, 400, 30, "Default Text");

        conversation.Items.Add(item);
        item.TextInPolish = conversation.Items.Count - 1 + ". is your ID";

        if (conversation.Items.Count == 0)
        {
            conversation.SetMasterItem(item);
        }
#endif
    }

    public void EditItem(object obj)
    {
        itemEditor.editingItem = (ConversationItem)obj;
        //editItem = true;
        itemEditor.editItem = true;
    }

    public void ChangeItemType(object obj)
    {
        ConversationItem item = (ConversationItem)obj;

        if (item.type == ConversationItem.ItemType.Answer)
            item.type = ConversationItem.ItemType.Sentence;
        else if (item.type == ConversationItem.ItemType.Sentence)
            item.type = ConversationItem.ItemType.Answer;
    }

    public void SetAsMasterItem(object obj)
    {
        ConversationItem item = (ConversationItem)obj;

        conversation.SetMasterItem(item);
    }
}

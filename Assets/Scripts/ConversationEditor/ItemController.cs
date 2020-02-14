using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController
{
    public ItemEditor itemEditor;
    public ConversationItem movedItem = null;
    private Conversation conversation = null;
    public bool movingItem = false;

    public static Color sentenceBoxColor = new Color32(51,63,72,255);
    public static Color answerBoxColor = new Color32(0,92,76,255);

    //private ConversationItem editingItem = null;
    //private bool editItem = false;


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
        if (conversation != null)
        {
            foreach (var item in conversation.Items)
            {
                item.Paint();
            }
        }
    }

    public void CreateItem(object obj)
    {
        Vector2 mousePosition = (Vector2)obj;
        ConversationItem item = new ConversationItem(mousePosition, 400, 30, "Default Text");

        conversation.Items.Add(item);
        item.Text = conversation.Items.Count - 1 + ". is your ID";
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
}

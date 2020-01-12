using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController
{
    public ItemEditor itemEditor;
    public ConversationItem movedItem = null;
    private DialogData dialogData = null;
    public bool movingItem = false;

    //private ConversationItem editingItem = null;
    //private bool editItem = false;


    public ItemController()
    {
        itemEditor = new ItemEditor();
    }

    public void Setup(DialogData dialogData)
    {
        this.dialogData = dialogData;
    }

    public void PaintItems()
    {
        foreach (var item in dialogData.conversation.Items)
        {
            item.Paint();
        }
    }

    public void CreateItem(object obj)
    {
        Vector2 mousePosition = (Vector2)obj;
        ConversationItem item = new ConversationItem(mousePosition, 200, 30, "Default Text");

        dialogData.conversation.Items.Add(item);
        item.Text = dialogData.conversation.Items.Count - 1 + ". is your ID";
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

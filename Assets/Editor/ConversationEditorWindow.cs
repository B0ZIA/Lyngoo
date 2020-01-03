using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class ConversationEditorWindow : EditorWindow
{
    public static DialogData dialogData = null;

    private static EdgeController edgeController;
    private static ItemController itemController;

    public Vector2 scrollPos = Vector2.zero;



    [MenuItem("Window/Conversation Editor")]
    public static ConversationEditorWindow OpenConversationEditorWindow()
    {
        itemController = new ItemController();
        edgeController = new EdgeController();

        return EditorWindow.GetWindow<ConversationEditorWindow>();
    }

    private void OnGUI()
    {
        Repaint();
        Event currentEvent = Event.current;

        if (itemController != null)
        {
            if (itemController.itemEditor.editItem)
            {
                BeginWindows();
                itemController.itemEditor.DrawItemEditor();
                EndWindows();
                return;
            }
        }

        SetStartScrollView();

        BeginWindows();

        DrawDialogDataLoader();

        if (dialogData == null)
        {
            EditorGUILayout.LabelField("Nie został wczytany plik danych!");

            EndWindows();
            GUI.EndScrollView();
            return;
        }
        else
        {
            edgeController.Setup(dialogData);
            edgeController.PaintEdges();

            itemController.Setup(dialogData);
            itemController.PaintItems();

            if (edgeController.makingEdge)
            {
                edgeController.MakeEdge(currentEvent);
            }

            if (itemController.movingItem)
            {
                itemController.movedItem.Move();
            }

            switch (currentEvent.type)
            {
                case EventType.ContextClick:
                    DrawGenericMenu(currentEvent);
                    break;
                case EventType.MouseDown:
                    SetMovedItem();
                    break;
                case EventType.MouseUp:
                    itemController.movingItem = false;
                    break;
            }

            EndWindows();
            GUI.EndScrollView();
        }
    }

    private static void DrawDialogDataLoader()
    {
        EditorGUILayout.BeginHorizontal();
        dialogData = (DialogData)EditorGUILayout.ObjectField(dialogData, typeof(DialogData), true);
        EditorGUILayout.LabelField("");
        EditorGUILayout.EndHorizontal();
    }

    private void SetStartScrollView()
    {
        scrollPos = GUI.BeginScrollView(new Rect(0, 0, position.width, position.height), scrollPos, new Rect(0, 0, 2000, 10000));
    }

    private void SetMovedItem()
    {
        foreach (var item in dialogData.conversation.Items)
        {
            if (item.Box.Contains(Event.current.mousePosition))
            {
                itemController.movedItem = item;
            }
        }
    }

    //TODO: Wyodrębnić do oddizielnej klasy wraz z poniższymi metodami
    private void DrawGenericMenu(Event currentEvent)
    {
        Vector2 mousePos = currentEvent.mousePosition;

        if (dialogData.conversation.Items.Count != 0)
        {
            foreach (var item in dialogData.conversation.Items)
            {
                if (item.Box.Contains(mousePos))
                {
                    DrawItemMenu(currentEvent, item);
                    break;
                }
                else
                {
                    DrawMainMenu(currentEvent);
                }
            }
        }
        else
        {
            DrawMainMenu(currentEvent);
        }
    }

    private void DrawItemMenu(Event currentEvent, ConversationItem item)
    {
        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("Edytuj"), false, itemController.EditItem, item);
        menu.AddSeparator("");

        menu.AddItem(new GUIContent("Stwóż połącznie"), false, edgeController.CreateEdge, item);
        menu.AddItem(new GUIContent("Zniszcz powyższe połącznia"), false, edgeController.DestroyAboveEdges, item);
        menu.AddItem(new GUIContent("Zniszcz poniższe połączenia"), false, edgeController.DestroyBelowEdges, item);
        menu.AddSeparator("");

        bool mySentence = Convert.ToBoolean((int)item.type);

        menu.AddItem(new GUIContent("Właściciel zdania/Ty"), mySentence, itemController.ChangeItemType, item);
        menu.AddItem(new GUIContent("Właściciel zdania/Przyjaciel"), !mySentence, itemController.ChangeItemType, item);
        menu.AddItem(new GUIContent("ID: "+item.ID), false, null);

        menu.ShowAsContext();
        currentEvent.Use();
    }

    

    private void DrawMainMenu(Event currentEvent)
    {
        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("Create Item"), false, itemController.CreateItem, currentEvent.mousePosition);
        menu.ShowAsContext();
        currentEvent.Use();
    }
}
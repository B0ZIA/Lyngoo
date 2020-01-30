using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class ConversationEditorWindow : EditorWindow
{
    private static DataType dataType;
    public static DialogData dialogData = null;
    public static MissionData missionData = null;
    private static int selectedMissionData;
    private static Conversation currentMissionConversation;

    private static EdgeController edgeController;
    private static ItemController itemController;

    public Vector2 scrollPos = Vector2.zero;


    [MenuItem("Window/Conversation Editor")]
    public static ConversationEditorWindow OpenConversationEditorWindow()
    {
        return EditorWindow.GetWindow<ConversationEditorWindow>();
    }

    void Awake()
    {
        itemController = new ItemController();
        edgeController = new EdgeController();
    }

    private void OnGUI()
    {
        Repaint();
        Event currentEvent = Event.current;

        if (itemController == null || edgeController == null)
        {
            Awake();
        }

        if (itemController.itemEditor.editItem)
        {
            BeginWindows();
            itemController.itemEditor.DrawItemEditor();
            EndWindows();
            return;
        }

        SetStartScrollView();

        BeginWindows();

        DrawDialogDataLoader();

        switch (dataType)
        {
            case DataType.Dialogs:
                if (dialogData == null)
                {
                    BreakEditorWindow();
                    return;
                }
                else
                {
                    edgeController.Setup(dialogData.conversation);
                    itemController.Setup(dialogData.conversation);
                }
                break;
            case DataType.Missions:
                if (missionData == null)
                {
                    BreakEditorWindow();
                    return;
                }
                else
                {
                    edgeController.Setup(currentMissionConversation);
                    itemController.Setup(currentMissionConversation);
                }
                break;
            default:
                break;
        }

        edgeController.PaintEdges();
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

    private void BreakEditorWindow()
    {
        EditorGUILayout.LabelField("Nie został wczytany plik danych!");

        EndWindows();
        GUI.EndScrollView();
        return;
    }

    private static void DrawDialogDataLoader()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Gdzie znajduje się porządany dialog: ");
        dataType = (DataType)EditorGUILayout.EnumPopup(dataType);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.EndHorizontal();

        switch (dataType)
        {
            case DataType.Dialogs:
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Podaj plik Dialogów: ");
                dialogData = (DialogData)EditorGUILayout.ObjectField(dialogData, typeof(DialogData), true);
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.EndHorizontal();

                break;
            case DataType.Missions:
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Podaj plik Misji: ");
                missionData = (MissionData)EditorGUILayout.ObjectField(missionData, typeof(MissionData), true);
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.EndHorizontal();

                if (missionData != null)
                {

                    string[] personsName = new string[missionData.personDialogs.Count];

                    for (int i = 0; i < missionData.personDialogs.Count; i++)
                    {
                        personsName[i] = missionData.personDialogs[i].personName;
                    }

                    EditorGUILayout.BeginHorizontal();
                    selectedMissionData = GUILayout.Toolbar(selectedMissionData, personsName);
                    EditorGUILayout.EndHorizontal();
                    currentMissionConversation = missionData.personDialogs[selectedMissionData].conversation;
                }
                break;
            default:
                break;
        }
    }

    private void SetStartScrollView()
    {
        scrollPos = GUI.BeginScrollView(new Rect(0, 0, position.width, position.height), scrollPos, new Rect(0, 0, 2000, 10000));
    }

    private void SetMovedItem()
    {
        switch (dataType)
        {
            case DataType.Dialogs:
                foreach (var item in dialogData.conversation.Items)
                {
                    if (item.Box.Contains(Event.current.mousePosition))
                    {
                        itemController.movedItem = item;
                        itemController.movingItem = true;
                    }
                }
                break;
            case DataType.Missions:
                foreach (var item in currentMissionConversation.Items)
                {
                    if (item.Box.Contains(Event.current.mousePosition))
                    {
                        itemController.movedItem = item;
                        itemController.movingItem = true;
                    }
                }
                break;
            default:
                break;
        }
    }

    //TODO: Wyodrębnić do oddizielnej klasy wraz z poniższymi metodami
    private void DrawGenericMenu(Event currentEvent)
    {
        Vector2 mousePos = currentEvent.mousePosition;

        switch (dataType)
        {
            case DataType.Dialogs:
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
                break;
            case DataType.Missions:
                if (currentMissionConversation.Items.Count != 0)
                {
                    foreach (var item in currentMissionConversation.Items)
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
                break;
            default:
                break;
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
        menu.AddItem(new GUIContent("Zniszcz zdanie (zniszcz wcześniej połącznia!)"), false, DestroyItem, item);
        menu.AddItem(new GUIContent("ID: "+item.ID), false, null);

        menu.ShowAsContext();
        currentEvent.Use();
    }

    private void DestroyItem(object obj)
    {
        ConversationItem item = (ConversationItem)obj;

        switch (dataType)
        {
            case DataType.Dialogs:
                dialogData.conversation.Items.Remove(item);
                break;
            case DataType.Missions:
                currentMissionConversation.Items.Remove(item);
                break;
            default:
                break;
        }
    }



    private void DrawMainMenu(Event currentEvent)
    {
        GenericMenu menu = new GenericMenu();
        menu.AddItem(new GUIContent("Stwóż zdanie"), false, itemController.CreateItem, currentEvent.mousePosition);
        menu.ShowAsContext();
        currentEvent.Use();
    }

    private enum DataType
    {
        Dialogs,
        Missions
    }
}
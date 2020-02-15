using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Video;

public class ItemEditor
{
    public ConversationItem editingItem = null;
    public bool editItem = false;



    public void DrawItemEditor()
    {
        var style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontStyle = FontStyle.Bold;

#if UNITY_EDITOR

        GUILayout.Space(10);

        EditorGUILayout.LabelField("Edytujesz item o ID: " + editingItem.ID, style);

        GUILayout.Space(30);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Tekst po polsku: ", style);
        editingItem.TextInPolish = EditorGUILayout.TextField(editingItem.TextInPolish);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Tekst po hiszpańsku: ", style);
        editingItem.TextInSpanish = EditorGUILayout.TextField(editingItem.TextInSpanish);
        EditorGUILayout.EndHorizontal();

        //EditorGUILayout.BeginHorizontal();
        //EditorGUILayout.LabelField("Plik Audio: ");
        //editingItem.audioClip = (AudioClip)EditorGUILayout.ObjectField(editingItem.audioClip, typeof(AudioClip), false);
        //EditorGUILayout.EndHorizontal();

        //EditorGUILayout.BeginHorizontal();
        //EditorGUILayout.LabelField("Plik Wideo: ");
        //editingItem.videoClip = (VideoClip)EditorGUILayout.ObjectField(editingItem.videoClip, typeof(VideoClip), false);
        //EditorGUILayout.EndHorizontal();

        GUILayout.Space(30);

        editItem = !GUILayout.Button("Zakończ");
#endif
    }

    public void CheckEditItem()
    {
        if (editingItem != null)
            DrawItemEditor();
    }

    public void EditItem(ConversationItem editingItem)
    {
        this.editingItem = editingItem;
    }
}

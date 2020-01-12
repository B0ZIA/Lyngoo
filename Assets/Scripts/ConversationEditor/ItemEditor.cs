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
#if UNITY_EDITOR

        GUILayout.Space(10);

        EditorGUILayout.LabelField("Edytujesz item o ID: " + editingItem.ID);

        GUILayout.Space(30);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Tekst: ");
        editingItem.Text = EditorGUILayout.TextField(editingItem.Text);
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

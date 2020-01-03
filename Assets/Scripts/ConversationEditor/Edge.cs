using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Edge
{
    public string FromId;
    public string ToId;



    public Edge(ConversationItem from)
    {
        FromId = from.ID;
    }

    public void SetToItem(ConversationItem item)
    {
        ToId = item.ID;
    }

    public void Paint(Conversation conversation)
    {
#if UNITY_EDITOR

        if (String.IsNullOrEmpty(FromId) || String.IsNullOrEmpty(ToId))
            return;

        if (conversation.FindItem(FromId).type != conversation.FindItem(ToId).type)
            Handles.color = Color.white;
        else
            Handles.color = Color.red;

        Rect from = conversation.FindItem(FromId).Box;
        Rect to = conversation.FindItem(ToId).Box;

        Vector2 pStart = new Vector2(from.width / 2 + from.x, from.y + from.height);
        Vector2 pEnd = new Vector2(to.width / 2 + to.x, to.y-5);
        Vector3[] points = new Vector3[2];
        points[0] = pStart;
        points[1] = pEnd;
        Handles.DrawAAPolyLine(4, points);

        Rect rect = new Rect(pEnd.x - 5, pEnd.y, 10, 5);

        Handles.DrawSolidRectangleWithOutline(rect, Color.white, Color.grey);
#endif
    }

    public void Paint(Vector2 mousePosition, Conversation conversation)
    {
#if UNITY_EDITOR

        if (FromId == "")
            return;

        Handles.color = Color.white;
        Rect from = conversation.FindItem(FromId).Box;

        Vector2 pStart = new Vector2(from.width / 2 + from.x, from.y + from.height);

        Handles.DrawLine(pStart, mousePosition);
#endif
    }
}
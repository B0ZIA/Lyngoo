using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeController
{
    private DialogData dialogData = null;

    private Edge currentEdge = null;
    public bool makingEdge = false;


    
    public void Setup(DialogData dialogData)
    {
        this.dialogData = dialogData;
    }

    public void PaintEdges()
    {
        foreach (var edge in dialogData.conversation.Edges)
        {
            edge.Paint(dialogData.conversation);
        }
    }

    public void MakeEdge(Event currentEvent)
    {
        currentEdge.Paint(Event.current.mousePosition, dialogData.conversation);

        if (currentEvent.type == EventType.MouseDown)
        {
            foreach (var item in dialogData.conversation.Items)
            {
                Vector2 mousePos = currentEvent.mousePosition;
                if (item.Box.Contains(mousePos))
                {
                    if (dialogData.conversation.FindEdgeByToID(item.ID) != null)
                        return;
                    currentEdge.SetToItem(item);

                    item.SetupChildrenType(dialogData.conversation.FindItem(currentEdge.FromId).type);
                    makingEdge = false;
                }
            }
        }
    }

    public void DestroyBelowEdges(object obj)
    {
        ConversationItem item = (ConversationItem)obj;
        List<Edge> edgesToRemove = new List<Edge>();

        foreach (var edge in dialogData.conversation.Edges)
        {
            if (dialogData.conversation.FindEdgeByFromID(edge.FromId).FromId == item.ID)
                edgesToRemove.Add(edge);
        }

        foreach (var edgeToRemove in edgesToRemove)
        {
            if (edgesToRemove.Count > 0)
            {
                dialogData.conversation.Edges.Remove(edgeToRemove);
            }
        }
    }

    public void DestroyAboveEdges(object obj)
    {
        ConversationItem item = (ConversationItem)obj;
        List<Edge> edgesToRemove = new List<Edge>();

        foreach (var edge in dialogData.conversation.Edges)
        {
            if (dialogData.conversation.FindEdgeByToID(edge.ToId).ToId == item.ID)
                edgesToRemove.Add(edge);
        }

        foreach (var edgeToRemove in edgesToRemove)
        {
            if (edgesToRemove.Count > 0)
            {
                dialogData.conversation.Edges.Remove(edgeToRemove);
            }
        }
    }

    public void CreateEdge(object obj)
    {
        ConversationItem parent = (ConversationItem)obj;

        currentEdge = new Edge(parent);

        dialogData.conversation.Edges.Add(currentEdge);
        makingEdge = true;
    }
}

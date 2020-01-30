using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeController
{
    private Conversation conversation = null;

    private Edge currentEdge = null;
    public bool makingEdge = false;


    
    public void Setup(Conversation conversation)
    {
        this.conversation = conversation;
    }

    public void PaintEdges()
    {
        if (conversation != null)
        {
            foreach (var edge in conversation.Edges)
            {
                edge.Paint(conversation);
            }
        }
    }

    public void MakeEdge(Event currentEvent)
    {
        currentEdge.Paint(Event.current.mousePosition, conversation);

        if (currentEvent.type == EventType.MouseDown)
        {
            foreach (var item in conversation.Items)
            {
                Vector2 mousePos = currentEvent.mousePosition;
                if (item.Box.Contains(mousePos))
                {
                    foreach (var edge in conversation.Edges)
                    {
                        if (edge.ToId == item.ID && edge.FromId == currentEdge.FromId)
                            return;
                    }

                    currentEdge.SetToItem(item);

                    item.SetupChildrenType(conversation.FindItem(currentEdge.FromId).type);
                    makingEdge = false;
                }
            }
        }
    }

    public void DestroyBelowEdges(object obj)
    {
        ConversationItem item = (ConversationItem)obj;
        List<Edge> edgesToRemove = new List<Edge>();

        foreach (var edge in conversation.Edges)
        {
            if (conversation.FindEdgeByFromID(edge.FromId).FromId == item.ID)
                edgesToRemove.Add(edge);
        }

        foreach (var edgeToRemove in edgesToRemove)
        {
            if (edgesToRemove.Count > 0)
            {
                conversation.Edges.Remove(edgeToRemove);
            }
        }
    }

    public void DestroyAboveEdges(object obj)
    {
        ConversationItem item = (ConversationItem)obj;
        List<Edge> edgesToRemove = new List<Edge>();

        foreach (var edge in conversation.Edges)
        {
            if (conversation.FindEdgeByToID(edge.ToId).ToId == item.ID)
                edgesToRemove.Add(edge);
        }

        foreach (var edgeToRemove in edgesToRemove)
        {
            if (edgesToRemove.Count > 0)
            {
                conversation.Edges.Remove(edgeToRemove);
            }
        }
    }

    public void CreateEdge(object obj)
    {
        ConversationItem parent = (ConversationItem)obj;

        currentEdge = new Edge(parent);

        conversation.Edges.Add(currentEdge);
        makingEdge = true;
    }
}

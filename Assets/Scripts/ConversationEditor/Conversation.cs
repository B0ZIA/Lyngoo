﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Conversation
{
    private List<ConversationItem> conversationItem;

    public List<ConversationItem> Items = new List<ConversationItem>();
    public List<Edge> Edges = new List<Edge>();



    public List<ConversationItem> FindItemChilds(ConversationItem parent)
    {
        List<ConversationItem> children = new List<ConversationItem>();

        foreach (var edge in Edges)
        {
            if (edge.FromId == parent.ID)
                children.Add(FindItem(edge.ToId));
        }

        return children;
    }

    public void FindItemParents()
    {

    }

    public ConversationItem FindItem(string ID)
    {
        foreach (var item in Items)
        {
            if (item.ID == ID)
                return item;
        }
        Debug.LogError("Can not find ConversatonItem!");
        return null;
    }

    public Edge FindEdgeByFromID(string fromID)
    {
        foreach (var edge in Edges)
        {
            if (edge.FromId == fromID)
                return edge;
        }

        return null;
    }

    public Edge FindEdgeByToID(string toID)
    {
        foreach (var edge in Edges)
        {
            if (edge.ToId == toID)
                return edge;
        }

        return null;
    }

}
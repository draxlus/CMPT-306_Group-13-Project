using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Node :IComparable
{
    public Node parent;

    public int nodeName; //unique id
    public int gCost, hCost, fCost;
    public int row, col, height;
    public bool isObstacle;


    //public Node(Node _parent, int _xPos, int _yPos, int _name)
    //{
    //    isObstacle = false;
    //    parent = _parent;
    //    row = _xPos;
    //    col = _yPos;
    //    nodeName = _name;

    //}
    public Node(int _xPos, int _yPos, int _name, bool _isObstacle)
    {
        isObstacle = _isObstacle;
        row = _xPos;
        col = _yPos;
        nodeName = _name;
        gCost = 0;
    }

    public Node()
    {

    }

    public int getfCost()
    {
        return gCost + hCost;
    }

    public override bool Equals(object obj)
    {
        Node o = obj as Node;
        return (this.nodeName == o.nodeName) && (this.fCost == o.fCost);
    }

    public int CompareTo(object obj)
    {
        Node n = obj as Node;
        if(this.getfCost() < n.getfCost())
        {
            return -1;
        }
        if(this.getfCost() > n.getfCost())
        {
            return 1;
        }
        return 0;
    }

    public override string ToString()
    {
        return this.nodeName.ToString();
    }

    

}

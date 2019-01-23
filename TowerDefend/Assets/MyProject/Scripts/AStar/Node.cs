using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node{
    /// <summary>
    /// 2D网格坐标
    /// </summary>
    public Point GridPosition { get; private set; }
    /// <summary>
    /// 地图块
    /// </summary>
    public TileScript tileScript { get; private set; }

    /// <summary>
    /// 父节点
    /// </summary>
    public Node parent { get; private set; }


    /// <summary>
    /// 世界坐标
    /// </summary>
    public Vector3 worldPosition { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public float G;

    public float F;

    public float H;

    public Node(TileScript ts)
    {
        this.tileScript = ts;
        this.GridPosition = ts.GridPosition;
        this.worldPosition = ts.WorldPosition;
    }

    public void CalcValues(Node parent,Node end,int gScore)
    {
        this.parent = parent;
        this.G = parent.G + gScore;
        this.H = (Mathf.Abs(GridPosition.X - end.GridPosition.X) + Mathf.Abs(end.GridPosition.Y - GridPosition.Y)) * 10;
        this.F = G + H;
    }
}

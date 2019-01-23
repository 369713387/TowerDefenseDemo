using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AStar {

    private static Dictionary<Point, Node> nodeDict;

    private static void CreateNodes()
    {
        nodeDict = new Dictionary<Point, Node>();

        foreach (TileScript tile in LevelManager.Instance.TileDict.Values)
        {
            nodeDict.Add(tile.GridPosition, new Node(tile));
        }
    }

    public static Stack<Node> GetPath(Point start, Point end)
    {
        if (nodeDict == null)
        {
            CreateNodes();
        }

        HashSet<Node> openList = new HashSet<Node>();

        HashSet<Node> closedList = new HashSet<Node>();

        Stack<Node> finalPath = new Stack<Node>();

        Node currentNode = nodeDict[start];

        openList.Add(currentNode);

        while (openList.Count > 0)
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    


                    //求出邻居节点的位置
                    Point neighbourPos = new Point(currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y);

                    Node neighbour = nodeDict[neighbourPos];

                    int gCost = 0;

                    if (Mathf.Abs(x - y) == 1)
                    {
                        gCost = 10;
                    }
                    else
                    {
                        if (!ConnectedDiagonally(currentNode, neighbour))
                        {
                            continue;
                        }
                        gCost = 14;
                    }

                    //排除不可以走的邻居节点
                    if (LevelManager.Instance.InBounds(neighbourPos) && LevelManager.Instance.TileDict[neighbourPos].WalkAble && neighbourPos != currentNode.GridPosition)
                    {
                        

                        if (openList.Contains(neighbour))
                        {
                            if (currentNode.G + gCost < neighbour.G)
                            {
                                neighbour.CalcValues(currentNode, nodeDict[end], gCost);
                            }
                        }
                        else if (!closedList.Contains(neighbour))
                        {
                            openList.Add(neighbour);
                            neighbour.CalcValues(currentNode, nodeDict[end], gCost);
                        }

                        ////将所有的邻居节点加入到openlist中
                        //if (!openList.Contains(neighbour))
                        //{
                        //    openList.Add(neighbour);
                        //}
                        ////设置所有的邻居节点的父节点为当前节点
                        //neighbour.CalcValues(currentNode, nodeDict[end], gCost);
                        //neighbour.tileScript.GetComponent<SpriteRenderer>().color = Color.black;
                    }

                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (openList.Count > 0)
            {
                currentNode = openList.OrderBy(n => n.F).First();
            }

            if (currentNode == nodeDict[end])
            {
                while (currentNode.GridPosition != start)
                {
                    finalPath.Push(currentNode);
                    currentNode = currentNode.parent;
                }
                break;
            }
        }

        //GameObject.Find("AStarDebugger").GetComponent<AStarDebugger>().DebugPath(openList, closedList, finalPath);

        return finalPath;
    }

    public static bool ConnectedDiagonally(Node currentnode,Node neighbor)
    {
        Point direction = neighbor.GridPosition - currentnode.GridPosition;

        Point first = new Point(currentnode.GridPosition.X + direction.X, currentnode.GridPosition.Y);

        Point second = new Point(currentnode.GridPosition.X , currentnode.GridPosition.Y + direction.Y);

        if (LevelManager.Instance.InBounds(first)&& !LevelManager.Instance.TileDict[first].WalkAble)
        {
            return false;
        }
        else if(LevelManager.Instance.InBounds(second) && !LevelManager.Instance.TileDict[second].WalkAble)
        {
            return false;
        }

        return true;
    }
}

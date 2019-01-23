using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarDebugger : MonoBehaviour {

    public TileScript start, end;

    public Sprite whiteSprite;

    public GameObject arrowPrefab;

    public GameObject debugPrefab;

	void Start () {
		
	}
	
	void Update () {
        //ClickTile();

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    AStar.GetPath(start.GridPosition,end.GridPosition);
        //}

    }

    /// <summary>
    /// 选择寻路算法的起点和终点
    /// </summary>
    private void ClickTile()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                TileScript temp = hit.transform.GetComponent<TileScript>();
                if (temp != null)
                {
                    if (start == null)
                    {
                        start = temp;
                        start.GetComponent<SpriteRenderer>().sprite = whiteSprite;
                        start.GetComponent<SpriteRenderer>().color = new Color32(255, 110, 0, 255);
                        start.Debuging = true;
                    }
                    else if (end == null && temp.GridPosition != start.GridPosition)
                    {
                        end = temp;
                        end.GetComponent<SpriteRenderer>().sprite = whiteSprite;
                        end.GetComponent<SpriteRenderer>().color = new Color32(255, 0, 0, 255);
                        end.Debuging = true;
                    }
                }
            }
        }
    }
    /// <summary>
    /// 输出路径
    /// </summary>
    /// <param name="openlist"></param>
    public void DebugPath(HashSet<Node> openlist,HashSet<Node> closelist,Stack<Node> finalpath)
    {
        foreach (Node node in openlist)
        {
            if(node.tileScript != start)
            {
                node.tileScript.GetComponent<SpriteRenderer>().sprite = whiteSprite;
                node.tileScript.GetComponent<SpriteRenderer>().color = Color.cyan;
                CreateDebugTile(node.tileScript.WorldPosition, Color.cyan, node);
            }
            PointToParent(node, node.tileScript.WorldPosition);
        }

        foreach (Node node in closelist)
        {
            if (node.tileScript != start && node.tileScript != end && !finalpath.Contains(node))
            {
                node.tileScript.GetComponent<SpriteRenderer>().sprite = whiteSprite;
                node.tileScript.GetComponent<SpriteRenderer>().color = Color.blue;
                CreateDebugTile(node.tileScript.WorldPosition, Color.black,node);
            }
            //PointToParent(node, node.tileScript.WorldPosition);
        }

        foreach (Node node in finalpath)
        {
            if(node.tileScript != start && node.tileScript != end)
            {
                CreateDebugTile(node.tileScript.WorldPosition, Color.green, node);
            }
        }
    }

    /// <summary>
    /// 指向父节点
    /// </summary>
    private void PointToParent(Node node ,Vector3 position)
    {
        if (node.parent != null)
        {
            GameObject arrow = Instantiate(arrowPrefab, position, Quaternion.identity);
            
        }
        
    }

    /// <summary>
    /// 创建输出指示图
    /// </summary>
    /// <param name="worldpos"></param>
    /// <param name="color"></param>
    /// <param name="node"></param>
    private void CreateDebugTile(Vector3 worldpos,Color32 color,Node node = null)
    {
        GameObject debugTile = Instantiate(debugPrefab, worldpos, Quaternion.identity);

        if(node != null)
        {
            debugTile.GetComponent<DebugTile>().G.text += node.G;
            debugTile.GetComponent<DebugTile>().H.text += node.H;
            debugTile.GetComponent<DebugTile>().F.text += node.F;
        }

        debugTile.GetComponent<SpriteRenderer>().color = color;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour {

    /// <summary>
    /// 2D网格坐标
    /// </summary>
    public Point GridPosition { get; private set; }
    /// <summary>
    /// 世界坐标
    /// </summary>
    public Vector3 WorldPosition { get { return new Vector3(transform.position.x /*- transform.GetComponent<SpriteRenderer>().sprite.bounds.size.x/2*/, transform.position.y /*+ transform.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2*/); } }

    /// <summary>
    /// 该地图块上是否为空
    /// </summary>
    public bool isEmpty;

    /// <summary>
    /// 该地图块是否可走
    /// </summary>
    public bool WalkAble;

    /// <summary>
    /// 输出模式
    /// </summary>
    public bool Debuging;

    /// <summary>
    /// 当前地图块上的角色
    /// </summary>
    private Knight knight;





    /// <summary>
    /// 不可以放置塔的颜色
    /// </summary>
    public readonly Color fullcolor = new Color32(255,118,118,255);

    /// <summary>
    /// 可以放置塔的颜色
    /// </summary>
    public readonly Color emptycolor = new Color32(96,255,90,255);

    void Update () {
		
	}
    /// <summary>
    /// 设置地图块坐标点
    /// </summary>
    /// <param name="point"></param>
    /// <param name="worldPosition"></param>
    public void SetUp(Point point,Vector3 worldPosition)
    {
        this.GridPosition = point;
        transform.position = worldPosition;
        isEmpty = true;
        Debuging = false;
        WalkAble = true;
    }
    /// <summary>
    /// 判断是否可以放置塔
    /// </summary>
    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.clickedBtn != null)
        {
            if (isEmpty)
            {
                transform.GetComponent<SpriteRenderer>().color = emptycolor;
                if (Input.GetMouseButtonDown(0))
                {
                    PlacePrefab();
                    Debug.Log("X : " + GridPosition.X + " Y : " + GridPosition.Y);
                }
            }
            else
            {
                transform.GetComponent<SpriteRenderer>().color = fullcolor;
            }
        }else if(!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.clickedBtn == null && Input.GetMouseButtonDown(0))
        {
            if (knight != null)
            {
                GameManager.Instance.SelectKnight(knight);
            }
            else
            {
                GameManager.Instance.DeselectKnight();
            }
        }                  
    }

    private void OnMouseExit()
    {
        if (GameManager.Instance.clickedBtn != null || !Debuging)
        {
            transform.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    /// <summary>
    /// 放置Prefab
    /// </summary>
    public void PlacePrefab()
    {
        //如果点击在UI上不执行下面代码
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.clickedBtn != null)
        {
            GameObject go = Instantiate(GameManager.Instance.clickedBtn.TowerPrefab, transform.position, Quaternion.identity);
            go.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;
            //将Prefab放置在地图快子物体下
            go.transform.SetParent(this.transform);
            //设置角色
            this.knight = go.transform.GetChild(0).GetComponent<Knight>();
            //隐藏Hover
            Hover.Instance.SpriteDeActive();
            //该地图块上存在塔
            isEmpty = false;
            //放置塔后不可走
            WalkAble = false;
            Debug.Log("PlacePrefab");
        }        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    static private LevelManager m_instance;
    static public LevelManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    //地图素材
    [SerializeField]
    private GameObject[] tiles;
    //子地图父物体
    [SerializeField]
    private Transform Map;
    /// <summary>
    /// 相机
    /// </summary>
    [SerializeField]
    private CameraMove cameraMove;

    /// <summary>
    /// 地图大小
    /// </summary>
    private Point mapSize;

    /// <summary>
    /// 点和地图块的映射
    /// </summary>
    public Dictionary<Point, TileScript> TileDict;
    /// <summary>
    /// 敌人出生点
    /// </summary>
    public GameObject enemy_bornpoint_prefab;
    /// <summary>
    /// 敌人目的点
    /// </summary>
    public GameObject enemy_targetpoint_prefab;
    /// <summary>
    /// 敌人起始点坐标
    /// </summary>
    public Point StartPoint;
    /// <summary>
    /// 敌人目的点坐标
    /// </summary>
    public Point EndPoint;

    /// <summary>
    /// 出生点
    /// </summary>
    public Portal startPortal;

    /// <summary>
    /// 目的点
    /// </summary>
    public Portal endPortal;

    /// <summary>
    /// 路径
    /// </summary>
    private Stack<Node> path;

    /// <summary>
    /// 路径
    /// </summary>
    public Stack<Node> Path
    {
        get
        {
            if(path == null)
            {
                GeneFinalPath();
            }
            return new Stack<Node>(new Stack<Node>(path));
        }
        set
        {
        }
    }

    /// <summary>
    /// 地图块大小
    /// </summary>
    public float TileSize
    {
        get
        {
            return tiles[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        }
    }

    private void Awake()
    {
        m_instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        InitData();
        CreateLevel();
    }

    /// <summary>
    /// 数据初始化
    /// </summary>
    private void InitData()
    {
        StartPoint = new Point(1, 2);
        EndPoint = new Point(33, 8);
    }

    /// <summary>
    /// UI初始化
    /// </summary>
	private void InitUI()
    {


    }

    /// <summary>
    /// 创建地图
    /// </summary>
    private void CreateLevel()
    {
        //初始化字典
        TileDict = new Dictionary<Point, TileScript>();
        //读取地图信息
        string[] mapData = ReadLevelTxt();

        mapSize = new Point(mapData[0].Length, mapData.Length);

        //初始化地图大小
        int mapXSize = mapData[0].ToCharArray().Length;
        int mapYSize = mapData.Length;

        //以左上角为世界坐标原点
        Vector3 worldOrigin = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < mapYSize; y++)
        {
            char[] newTiles = mapData[y].ToCharArray();

            for (int x = 0; x < mapXSize; x++)
            {
                Vector3 worldPos = PlaceTile(int.Parse(newTiles[x].ToString()), x, y, worldOrigin);
            }
        }
        TileScript lastTile;
        if (TileDict.TryGetValue(new Point(mapXSize - 1,mapYSize-1), out lastTile))
        {
            cameraMove.SetLimits(new Vector3(lastTile.transform.position.x + TileSize / 2, lastTile.transform.position.y - TileSize / 2));
            //产生敌人出生点和目的点
            SpawnPortal();
        }
        print(lastTile.WorldPosition);

    }

    /// <summary>
    /// 放置一块小地图
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="worldOrigin">世界坐标原点</param>
    private Vector3 PlaceTile(int tilePrefabIndex, int x, int y, Vector3 worldOrigin)
    {
        TileScript newTile = Instantiate(tiles[tilePrefabIndex]).GetComponent<TileScript>();
        //设置世界坐标
        newTile.transform.position = new Vector3(worldOrigin.x + x * TileSize, worldOrigin.y - y * TileSize);
        newTile.transform.SetParent(Map);
        //设置网格坐标,世界坐标
        newTile.SetUp(new Point(x, y),newTile.transform.position);
        //添加进字典里
        TileDict.Add(new Point(x, y), newTile);
        return newTile.transform.position;
    }

    /// <summary>
    /// 从文本读取地图信息
    /// </summary>
    /// <returns></returns>
    private string[] ReadLevelTxt()
    {
        TextAsset textAsset = Resources.Load("LevelMap") as TextAsset;

        string tempdata = textAsset.text.Replace(Environment.NewLine, string.Empty);
        return tempdata.Split('-');
    }

    /// <summary>
    /// 生成敌人出生点和目的点
    /// </summary>
    public void SpawnPortal()
    {
        Transform parent = GameObject.Find("Portal").transform;
        GameObject go_born_point = GameObject.Instantiate(enemy_bornpoint_prefab, TileDict[StartPoint].WorldPosition, Quaternion.identity);
        GameObject go_target_point = GameObject.Instantiate(enemy_targetpoint_prefab, TileDict[EndPoint].WorldPosition, Quaternion.identity);
        go_born_point.transform.SetParent(parent);
        go_target_point.transform.SetParent(parent);
        startPortal = go_born_point.GetComponent<Portal>();
        endPortal = go_target_point.GetComponent<Portal>();
    }

    /// <summary>
    /// 该点是否在地图范围内
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool InBounds(Point currentPoint)
    {
        return currentPoint.X >= 0 && currentPoint.Y >= 0 && currentPoint.X < mapSize.X && currentPoint.Y < mapSize.Y;
    }

    /// <summary>
    /// 生成路径
    /// </summary>
    /// <returns></returns>
    public void GeneFinalPath()
    {
        path = AStar.GetPath(StartPoint,EndPoint);
    }
}

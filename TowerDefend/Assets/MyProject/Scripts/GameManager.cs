using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    static private GameManager m_instance;
    static public GameManager Instance
    {
        get
        {
            return m_instance;
        }
    }
    #region 数据   
    /// <summary>
    /// 存活的敌人
    /// </summary>
    private List<Enemy> Active_enemies = new List<Enemy>();

    /// <summary>
    /// 当前波次是否结束
    /// </summary>
    private bool InWave
    {
        get
        {
            if(wave_EnemyNum == PlayerData.Instance.wave)
            {
                return Active_enemies.Count > 0;
            }
            else
            {
                return true;
            }
            
        }
    }

    private Knight selected_Knight;

    private int wave_EnemyNum;


    #endregion

    [Header("UI")]
    public Text TotalCoinsNum;
    public Text wave_Text;
    public Text hp_Text;
    public Button NextWaveBtn;

    /// <summary>
    /// 当前选择的塔
    /// </summary>
    public TowerBtn clickedBtn;

    /// <summary>
    /// 对象池
    /// </summary>
    public ObjectPool pool;



    private void Awake()
    {
        m_instance = this;
        PlayerData.Create();
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        Init_UI();
        Update_Ui();
    }

    private void Init_UI()
    {
        TotalCoinsNum.text = "$" + PlayerData.Instance.coins;
        
    }

    void Update()
    {

    }

    /// <summary>
    /// 开始产生敌人
    /// </summary>
    public void StartSpawn()
    {
        //开始产生敌人后隐藏按钮
        if (InWave)
        {
            NextWaveBtn.gameObject.SetActive(false);
            wave_EnemyNum = 0;
        }
        
        PlayerData.Instance.NextWave();

        Update_Ui();
        //产生敌人
        StartCoroutine(SpawnEnemy());

        
    }

    public void Update_Ui()
    {
        //更新Wave        
        wave_Text.text = string.Format("Wave : <color=#00ff8Aff>{0}</color>", PlayerData.Instance.wave);
        //更新血量
        hp_Text.text = string.Format("Hp : {0}", PlayerData.Instance.hp);
    }

    /// <summary>
    /// 2s产生一个敌人
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnEnemy()
    {
        //为敌人生成路径
        LevelManager.Instance.GeneFinalPath();
        //根据wave产生敌人数量
        for (int i = 0; i < PlayerData.Instance.wave; i++)
        {
            Enemy tempEnemy = null;
            int randomIndex = Random.Range(0, pool.objectsPrefab.Length);
            string enemyName = string.Empty;
            switch (randomIndex)
            {
                case 0:
                    enemyName = "Enemy1";
                    //初始化敌人Obj
                    tempEnemy = pool.GetGameObject(enemyName).GetComponent<Enemy1>();
                    break;
                case 1:
                    enemyName = "Enemy2";
                    //初始化敌人Obj
                    tempEnemy = pool.GetGameObject(enemyName).GetComponent<Enemy2>();
                    break;
                case 2:
                    enemyName = "Enemy3";
                    //初始化敌人Obj
                    tempEnemy = pool.GetGameObject(enemyName).GetComponent<Enemy3>();
                    break;
                case 3:
                    enemyName = "Enemy4";
                    //初始化敌人Obj
                    tempEnemy = pool.GetGameObject(enemyName).GetComponent<Enemy4>();
                    break;
                case 4:
                    enemyName = "Enemy5";
                    //初始化敌人Obj
                    tempEnemy = pool.GetGameObject(enemyName).GetComponent<Enemy5>();
                    break;
                default:
                    break;
            }
            
            //设置敌人出生位置
            tempEnemy.transform.position = LevelManager.Instance.startPortal.transform.position + Vector3.down * 0.5f;
            //记录当前存活的Enemy
            Active_enemies.Add(tempEnemy);
            //Debug.Log("生成敌人，当前存活敌人数量 ： " + Active_enemies.Count);
            //记录当前生成的敌人数量
            wave_EnemyNum++;
            //Debug.Log(wave_EnemyNum);
            yield return new WaitForSeconds(2f);
        }        
    }

    /// <summary>
    /// 移除存活的敌人
    /// </summary>
    /// <param name="enemy"></param>
    public void RemoveEnemy(Enemy enemy)
    {
        Active_enemies.Remove(enemy);
        //Debug.Log("销毁敌人，当前存活敌人数量 ： " + Active_enemies.Count);
    }

    /// <summary>
    /// 当前关卡是否结束
    /// </summary>
    /// <returns></returns>
    public void EndWave()
    {
        if (!InWave)
        {
            NextWaveBtn.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 选择角色
    /// </summary>
    public void SelectKnight(Knight k)
    {
        if(selected_Knight != null)
        {
            selected_Knight.Select();
        }

        selected_Knight = k;
        k.Select();
    }

    public void DeselectKnight()
    {
        if (selected_Knight != null)
        {
            selected_Knight.Select();
        }

        selected_Knight = null;        
    }

    #region Test
    public void Clear()
    {
        PlayerData.Instance.Clear();
    }
    #endregion
}

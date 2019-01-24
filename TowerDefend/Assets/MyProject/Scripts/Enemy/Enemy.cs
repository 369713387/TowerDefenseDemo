using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    /// <summary>
    /// 击杀后获得的金币
    /// </summary>
    [SerializeField]
    protected int killed_coins;

    /// <summary>
    /// 敌人最大生命值
    /// </summary>
    public float max_hp;

    /// <summary>
    /// 敌人移动速度
    /// </summary>
    [SerializeField]
    protected float speed;

    /// <summary>
    /// 敌人移动速度
    /// </summary>
    public float Speed
    {
        get { return speed; } set { speed = value; }
    }

    /// <summary>
    /// 防御力
    /// </summary>
    [SerializeField]
    protected float def;

    /// <summary>
    /// 敌人身上的Debuff
    /// </summary>
    private List<AbsDebuff> debuffs;

    /// <summary>
    /// 敌人移动路径
    /// </summary>
    private Stack<Node> path;
    /// <summary>
    /// 敌人网格坐标
    /// </summary>
    public Point GridPosition { get; set; }
    /// <summary>
    /// 敌人移动方向
    /// </summary>
    private Vector3 destination;
    /// <summary>
    /// 敌人动画机
    /// </summary>
    protected Animator animator;

    /// <summary>
    /// 血条
    /// </summary>
    public SimpleBar hpBar;

    /// <summary>
    /// 是否处于冰冻状态
    /// </summary>
    public bool iceDebuffing;

    public bool IsActive { get; set; }   

    public virtual void OnEnable () {
        Init();
    }

    public virtual void Update()
    {
        if (hpBar.Hp_now<=0)
        {
            EnemyDeath();
        }
        else if(IsActive)
        {
            HandleDebuffs();
            Move();
        }
        
    }

    void Init()
    {
        IsActive = true;
        iceDebuffing = false;
        animator = transform.GetComponent<Animator>();
        StartCoroutine(EnemyBornEx(new Vector3(0.1f, 0.1f, 1f), new Vector3(1, 1, 1)));

        hpBar = transform.GetComponentInChildren<SimpleBar>();
        hpBar.Hp_total = max_hp;
        hpBar.Hp_now = max_hp;
        debuffs = new List<AbsDebuff>();

        SetPath(LevelManager.Instance.Path);
    }

    /// <summary>
    /// 敌人生成特效
    /// </summary>
    /// <returns></returns>
    public IEnumerator EnemyBornEx(Vector3 from, Vector3 to)
    {
        float progress = 0;
        while (progress <= 1)
        {
            transform.localScale = Vector3.Lerp(from, to, progress);

            progress += Time.deltaTime;

            yield return new WaitForEndOfFrame();

        }

        transform.localScale = to;
    }
    
    /// <summary>
    /// 敌人移动
    /// </summary>
    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        if(transform.position == destination)
        {
            if(path != null && path.Count > 0)
            {                
                GridPosition = path.Peek().GridPosition;
                destination = path.Pop().worldPosition;
            }
        }
        

    }

    /// <summary>
    /// 设置路径
    /// </summary>
    private void SetPath(Stack<Node> newPath)
    {
        if(newPath != null)
        {
            this.path = newPath;
            GridPosition = path.Peek().GridPosition;
            destination = path.Pop().worldPosition;
        }
    }

    /// <summary>
    /// 释放go
    /// </summary>
    public virtual void Release()
    {
        hpBar.Hp_now = max_hp;
        IsActive = false;
        iceDebuffing = false;
        debuffs.Clear();
        GameManager.Instance.pool.ReleaseObject(this.gameObject);
        GameManager.Instance.RemoveEnemy(this);
        GameManager.Instance.EndWave();
    }

    public virtual void TakeDamage(float attack,ElementType type)
    {
        hpBar.Hp_now -= attack - GetTotalDef(type);
        Debug.Log(string.Format("{0} 伤害 ：{1}", type ,attack - GetTotalDef(type)));
    }

    /// <summary>
    /// 敌人死亡
    /// </summary>
    public virtual void EnemyDeath()
    {
        if (hpBar.Hp_now<=0)
        {
            Release();
        }
        else
        {

        }        
    }

    /// <summary>
    /// 获得总的防御力
    /// </summary>
    public virtual float GetTotalDef(ElementType type = ElementType.None)
    {
        float totalDef = 0;
        totalDef += def;
        return totalDef;
    }

    /// <summary>
    /// 获得Debuff
    /// </summary>
    /// <param name="debuff"></param>
    public void AddDebuff(AbsDebuff debuff)
    {
        if (!debuffs.Contains(debuff))
        {
            debuffs.Add(debuff);
        }
    }

    /// <summary>
    /// Debuffs生效函数
    /// </summary>
    private void HandleDebuffs()
    {
        if (debuffs.Count > 0)
        {
            foreach (AbsDebuff item in debuffs)
            {
                item.Effect();
            }
        }        
    }

}

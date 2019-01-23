using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    /// <summary>
    /// 敌人最大生命值
    /// </summary>
    public float max_hp;

    /// <summary>
    /// 敌人移动速度
    /// </summary>
    public float speed;

    /// <summary>
    /// 防御力
    /// </summary>
    [SerializeField]
    protected float def;

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
    private Animator animator;

    /// <summary>
    /// 血条
    /// </summary>
    public SimpleBar hpBar;

    public bool IsActive { get; set; }

    public abstract void OnTriggerEnter2D(Collider2D collision);    

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
            Move();
        }
        
    }

    void Init()
    {
        IsActive = true;
        animator = transform.GetComponent<Animator>();
        StartCoroutine(EnemyBornEx(new Vector3(0.1f, 0.1f, 1f), new Vector3(1, 1, 1)));

        hpBar = transform.GetComponentInChildren<SimpleBar>();
        hpBar.Hp_total = max_hp;
        hpBar.Hp_now = max_hp;

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
        GameManager.Instance.pool.ReleaseObject(this.gameObject);
        GameManager.Instance.RemoveEnemy(this);
        GameManager.Instance.EndWave();
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
    public virtual float GetTotalDef()
    {
        float totalDef = 0;
        totalDef += def;
        return totalDef;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsProjectile : MonoBehaviour {

    /// <summary>
    /// 子弹类型
    /// </summary>
    [SerializeField]
    protected string projectileType;

    /// <summary>
    /// 移动速度
    /// </summary>
    [SerializeField]
    protected float moveSpeed;

    /// <summary>
    /// 目标敌人
    /// </summary>
    protected Enemy target_Enemy;

    /// <summary>
    /// 发射子弹的角色
    /// </summary>
    protected Knight parent_Knight;
    public Knight Parent_Knight
    {
        get
        {
            return parent_Knight;
        }

        set
        {
            parent_Knight = value;
        }
    }

    /// <summary>
    /// 子弹伤害
    /// </summary>
    protected float attack;

    public float Attack
    {
        get
        {
            return attack;
        }

        set
        {
            attack = value;
        }
    }

    public abstract void Start();

    /// <summary>
    /// 初始化子弹
    /// </summary>
    /// <param name="knight"></param>
    public void Init(Knight knight)
    {
        this.parent_Knight = knight;
        this.target_Enemy = knight.attacking_Enemy;
    }

    /// <summary>
    /// 追踪敌人
    /// </summary>
    public void TrackTarget(Enemy enemy)
    {
        //敌人没有死
        if (enemy.IsActive && enemy != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, Time.deltaTime * moveSpeed);

            Vector2 dir = enemy.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
        else if (!enemy.IsActive)
        {
            //敌人死亡
            GameObjectPool.GetInstance().RecycleObj(this.gameObject);
        }
    }

    /// <summary>
    /// 给敌人附加Debuff
    /// </summary>
    /// <param name="debuff"></param>
    abstract public void ApplyDebuff(Enemy enemy);

    /// <summary>
    /// 回收子弹
    /// </summary>
    public virtual void Release()
    {
        GameObjectPool.GetInstance().RecycleObj(this.gameObject);
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (target_Enemy.gameObject.name == collision.gameObject.name)
            {
                //目标敌人被子弹击中                
                target_Enemy.TakeDamage(attack, parent_Knight.E_Type);
                ApplyDebuff(target_Enemy);
                Release();
            }
        }
    }
    
}

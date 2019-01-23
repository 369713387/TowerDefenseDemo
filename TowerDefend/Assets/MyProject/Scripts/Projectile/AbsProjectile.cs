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


    /// <summary>
    /// 追踪敌人
    /// </summary>
    abstract public void TrackTarget(Enemy enemy);

    /// <summary>
    /// 回收子弹
    /// </summary>
    public virtual void Release()
    {
        GameObjectPool.GetInstance().RecycleObj(this.gameObject);
    }

}

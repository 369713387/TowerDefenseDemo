using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Knight : MonoBehaviour {

    /// <summary>
    /// 正在攻击的敌人
    /// </summary>
    public Enemy attacking_Enemy;

    /// <summary>
    /// 所有可以攻击的敌人
    /// </summary>
    public List<Enemy> enemies_InTrigger = new List<Enemy>();

    /// <summary>
    /// 角色动画机
    /// </summary>
    public Animator animator;

    /// <summary>
    /// 攻击范围指示器
    /// </summary>
    public SpriteRenderer rangeRenderer;

    /// <summary>
    /// 攻击冷却时间
    /// </summary>
    public float attackColdTime;

    /// <summary>
    /// 攻击冷却时间
    /// </summary>
    protected float attackTimer;

    /// <summary>
    /// 是否可以攻击
    /// </summary>
    protected bool canAttack;

    /// <summary>
    /// 攻击力
    /// </summary>
    [SerializeField]
    protected float attack;

    public float K_Attack
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
    /// 攻击动画
    /// </summary>
    /// <returns></returns>
    abstract public IEnumerator Attack();

    /// <summary>
    /// 跳跃动画
    /// </summary>
    /// <returns></returns>
    abstract public IEnumerator Jump();

    /// <summary>
    /// 死亡动画
    /// </summary>
    /// <returns></returns>
    abstract public IEnumerator Dead();

    /// <summary>
    /// 受伤动画
    /// </summary>
    /// <returns></returns>
    abstract public IEnumerator Hurt();

    /// <summary>
    /// 站立动画
    /// </summary>
    /// <returns></returns>
    abstract public void Idle();

    /// <summary>
    /// 当敌人进入触发器
    /// </summary>
    /// <param name="collision"></param>
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (attacking_Enemy == null)
            {
                attacking_Enemy = collision.GetComponent<Enemy>();
            }
            else
            {
                if (!enemies_InTrigger.Contains(collision.GetComponent<Enemy>()))
                {
                    enemies_InTrigger.Add(collision.GetComponent<Enemy>());
                }
            }
        }
    }

    /// <summary>
    /// 当敌人离开触发器
    /// </summary>
    /// <param name="collision"></param>
    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log(collision.gameObject.name + "离开触发器");

            enemies_InTrigger.Remove(collision.GetComponent<Enemy>());

            attacking_Enemy = null;
        }
    }

    /// <summary>
    /// 攻击敌人
    /// </summary>
    public void Attack(Enemy enemy)
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackColdTime)
        {
            canAttack = true;
            attackTimer = 0;
        }

        if (!canAttack)
        {
            return;
        }

        if (attacking_Enemy == null && enemies_InTrigger.Count > 0)
        {
            attacking_Enemy = enemies_InTrigger[enemies_InTrigger.Count-1];
        }
        else if (attacking_Enemy != null)
        {
            if (canAttack)
            {
                //StartCoroutine(Attack());
                Knight_Attack();
                attackTimer = 0;
                canAttack = false;
            }
        }
    }
    /// <summary>
    /// 具体不同角色的攻击逻辑
    /// </summary>
    protected virtual void Knight_Attack()
    {

    }

    public virtual void Start()
    {
        animator = transform.parent.GetComponent<Animator>();
        rangeRenderer = transform.GetComponent<SpriteRenderer>();
        canAttack = true;
    }

    public void Select()
    {
        rangeRenderer.enabled = !rangeRenderer.enabled;
    }

    
}

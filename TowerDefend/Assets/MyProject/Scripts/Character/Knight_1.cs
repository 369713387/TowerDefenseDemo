using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_1 : Knight
{
    private int attackTimes;

    public override void Start()
    {
        base.Start();
        e_Type = ElementType.None;
        Walk();
    }

    void Update()
    {
        Attack(attacking_Enemy);
    }
    /// <summary>
    /// 攻击
    /// </summary>
    public override IEnumerator Attack()
    {
        animator.SetInteger("attack", 1);
        animator.SetInteger("idle", 0);
        yield return null;
    }

    public override IEnumerator Jump()
    {
        animator.SetInteger("jump", 1);
        animator.SetInteger("hurt", 0);
        animator.SetInteger("attack", 0);
        yield return new WaitForSeconds(1.2f);
        animator.SetInteger("jump", 0);
    }

    public void Walk()
    {
        animator.SetInteger("walk", 1);
    }

    public override void Idle()
    {
        animator.SetInteger("attack", 0);
        animator.SetInteger("idle", 1);
    }

    public override IEnumerator Hurt()
    {
        yield return null;
    }

    public override IEnumerator Dead()
    {
        yield return null;
    }

    protected override void Knight_Attack()
    {
        StartCoroutine(Attack());
        Fight();
        //Debug.Log("角色1攻击"+ attackTimes++);
    }

    /// <summary>
    /// 近战攻击
    /// </summary>
    protected void Fight()
    {
        attacking_Enemy.hpBar.Hp_now -= attack - attacking_Enemy.GetTotalDef();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight_2 : Knight {

    private int attackTimes;

    public GameObject FireProjectile_Pf;

    public override IEnumerator Attack()
    {
        animator.SetInteger("attack", 1);
        animator.SetInteger("idle", 0);
        yield return new WaitForSeconds(0.8f);
        Idle();
    }

    public override IEnumerator Dead()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator Hurt()
    {
        throw new System.NotImplementedException();
    }

    public override void Idle()
    {
        animator.SetInteger("attack", 0);
        animator.SetInteger("idle", 1);
    }

    public override IEnumerator Jump()
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        Attack(attacking_Enemy);
    }

    protected override void Knight_Attack()
    {
        StartCoroutine(Attack());

        Fire_Shoot();

        Debug.Log("角色2攻击" + attackTimes++);
    }

    /// <summary>
    /// 发射火焰攻击
    /// </summary>
    private void Fire_Shoot()
    {
        //生成子弹Pf
        GameObject FireProjectile = GameObjectPool.GetInstance().GetObj("FireProjectile", FireProjectile_Pf);
        FireProjectile.transform.position = transform.position;
        //初始化子弹
        FireProjectile.GetComponent<FireProjectile>().Init(this);
    }
}

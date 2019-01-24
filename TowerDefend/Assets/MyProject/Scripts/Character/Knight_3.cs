using System.Collections;
using UnityEngine;

public class Knight_3 : Knight
{

    public GameObject IceProjectile_Pf;

    public override void Start()
    {
        base.Start();
        e_Type = ElementType.Ice;
        Idle();
    }

    public override IEnumerator Attack()
    {
        animator.SetBool("attack",true);
        animator.SetBool("idle", false);
        yield return null;
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
        animator.SetBool("attack", false);
        animator.SetBool("idle", true);
    }

    public override IEnumerator Jump()
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        Attack(attacking_Enemy);
    }

    protected override void Knight_Attack()
    {
        StartCoroutine(Attack());

        Ice_Shoot();
    }

    /// <summary>
    /// 发射冰冻攻击
    /// </summary>
    private void Ice_Shoot()
    {
        GameObject IceProjectile = GameObjectPool.GetInstance().GetObj("IceProjectile",IceProjectile_Pf);
        IceProjectile.transform.position = transform.position;
        IceProjectile.GetComponent<IceProjectile>().Init(this);
    }
}

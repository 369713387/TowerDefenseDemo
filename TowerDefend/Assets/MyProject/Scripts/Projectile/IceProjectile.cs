using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceProjectile : AbsProjectile
{
    private IceDebuff iceDebuff;

    private float slowPercent;

    public override void Start()
    {
        projectileType = "Ice";
        moveSpeed = 8;
        attack = parent_Knight.K_Attack;
        slowPercent = 0.5f;
    }

    private void Update()
    {
        TrackTarget(target_Enemy);
    }

    public override void ApplyDebuff(Enemy enemy)
    {
        iceDebuff = new IceDebuff(enemy, 0.5f);
        enemy.AddDebuff(iceDebuff);
    }

    //protected override void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Enemy")
    //    {
    //        if (target_Enemy.gameObject.name == collision.gameObject.name)
    //        {
    //            //目标敌人被冰属性子弹击中                
    //            target_Enemy.TakeDamage(attack, parent_Knight.E_Type);
    //            ApplyDebuff(target_Enemy);
    //            Release();
    //        }
    //    }
    //}
}

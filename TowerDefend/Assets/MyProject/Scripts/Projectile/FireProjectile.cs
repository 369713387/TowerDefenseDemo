using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : AbsProjectile {

    private FireDebuff fireDebuff;

    [SerializeField]
    private float fireDamage;

    [SerializeField]
    private float coolTime;

    public override void Start()
    {
        projectileType = "Fire";
        moveSpeed = 10;
        attack = parent_Knight.K_Attack;
        fireDamage = 10;
        coolTime = 0.5f;
    }

    //public void Init(Knight knight)
    //{
    //    this.parent_Knight = knight;
    //    this.target_Enemy = knight.attacking_Enemy;
    //}

    private void Update()
    {
        TrackTarget(target_Enemy);
    }

    /// <summary>
    /// 给敌人附加Fire_Debuff
    /// </summary>
    /// <param name="debuff"></param>
    public override void ApplyDebuff(Enemy enemy)
    {
        fireDebuff = new FireDebuff(enemy,fireDamage,coolTime);
        enemy.AddDebuff(fireDebuff);
    }    

    //protected override void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Enemy")
    //    {
    //        if (target_Enemy.gameObject.name == collision.gameObject.name)
    //        {
    //            //目标敌人被火属性子弹击中                
    //            target_Enemy.TakeDamage(attack, parent_Knight.E_Type);
    //            ApplyDebuff(target_Enemy);
    //            Release();
    //        }
    //    }
    //}
}

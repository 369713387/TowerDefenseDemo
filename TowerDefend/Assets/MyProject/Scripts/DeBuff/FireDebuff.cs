using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDebuff : AbsDebuff
{
    private int damageTimes;

    [SerializeField]
    private float fireDamage;

    [SerializeField]
    private float coolTime;

    private float coolTimer;

    

    public FireDebuff(Enemy enemy,float damage,float coolTime) : base(enemy)
    {
        this.fireDamage = damage;
        this.coolTime = coolTime;
        this.coolTimer = 0;
    }

    /// <summary>
    /// Fire_Debuff的效果
    /// </summary>
    public override void Effect()
    {
        coolTimer += Time.deltaTime;

        if(coolTimer>= coolTime)
        {
            coolTimer = 0;

            if (deBuff_Enemy.hpBar.Hp_now >= 0)
            {
                //持续伤害
                deBuff_Enemy.hpBar.Hp_now -= fireDamage;
                Debug.Log(string.Format("伤害 ：{0}火属性伤害次数{1} ：", fireDamage , damageTimes++));
            }
        }        
    }
}

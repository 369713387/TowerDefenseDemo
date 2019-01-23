using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        //敌人被子弹击中
        string tag = collision.tag.ToString();
        switch (tag)
        {
            case "FireProjectile":
                FireProjectile fireProjectile = collision.GetComponent<FireProjectile>();
                hpBar.Hp_now -= fireProjectile.Attack - def;
                Debug.Log(string.Format("伤害 ：{0}", fireProjectile.Attack - def));

                fireProjectile.Release();
                break;
            default:
                break;
        }
    }
}

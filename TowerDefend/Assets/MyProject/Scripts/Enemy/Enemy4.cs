using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : Enemy
{
    /// <summary>
    /// 火防御力
    /// </summary>
    [SerializeField]
    protected float fire_def;

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        //敌人被子弹击中
        string tag = collision.tag.ToString();
        switch (tag)
        {
            case "FireProjectile":
                FireProjectile fireProjectile = collision.GetComponent<FireProjectile>();
                hpBar.Hp_now -= fireProjectile.Attack - def - fire_def;
                Debug.Log(string.Format("伤害 ：{0}", fireProjectile.Attack - def - fire_def));

                fireProjectile.Release();
                break;
            default:
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : Enemy
{

    private void Start()
    {
        killed_coins = 50;
    }

    /// <summary>
    /// 死亡播放动画
    /// </summary>
    public override void EnemyDeath()
    {
        if (hpBar.Hp_now <= 0)
        {
            animator.SetTrigger("Dead");
            IsActive = false;
        }
        else
        {

        }
    }
}

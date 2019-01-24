using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : Enemy
{
    private void Start()
    {
        killed_coins = 40;
    }

    /// <summary>
    /// 火防御力
    /// </summary>
    [SerializeField]
    protected float fire_def;

    /// <summary>
    /// 具有火属性防御的敌人
    /// </summary>
    /// <returns></returns>
    public override float GetTotalDef(ElementType type)
    {
        if(type == ElementType.Fire)
        {
            Debug.Log("具有火属性防御的敌人" + gameObject.name);
            return def + fire_def;
        }
        else
        {
            return def;
        }
        
    }   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDebuff : AbsDebuff
{
    private float slowPercent;

    public IceDebuff(Enemy enemy,float slowPercent) : base(enemy)
    {
        this.slowPercent = slowPercent;
    }

    public override void Effect()
    {
        if (!deBuff_Enemy.iceDebuffing)
        {
            deBuff_Enemy.Speed *= slowPercent;
            deBuff_Enemy.iceDebuffing = true;
        }        
    }
}

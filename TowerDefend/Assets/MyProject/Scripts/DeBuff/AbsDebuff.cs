using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsDebuff{

    public Enemy deBuff_Enemy;

    public AbsDebuff(Enemy enemy)
    {
        this.deBuff_Enemy = enemy;
    }

    /// <summary>
    /// Debuff的效果
    /// </summary>
    public abstract void Effect();
}

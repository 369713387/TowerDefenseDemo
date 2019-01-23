using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : AbsProjectile {
    
    private void Start()
    {
        projectileType = "Fire";
        moveSpeed = 10;
        Attack = parent_Knight.K_Attack;
    }

    public void Init(Knight knight)
    {
        this.parent_Knight = knight;
        this.target_Enemy = knight.attacking_Enemy;
    }

    private void Update()
    {
        TrackTarget(target_Enemy);
    }

    public override void TrackTarget(Enemy enemy)
    {
        //敌人没有死
        if(enemy.IsActive && enemy != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, Time.deltaTime * moveSpeed);

            Vector2 dir = enemy.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        } else if (!enemy.IsActive)
        {
            //敌人死亡
            GameObjectPool.GetInstance().RecycleObj(this.gameObject);
        }
    }
}

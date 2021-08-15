using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : EnemyHealth
{
    BossHealth bossHealth;

    private void Start()
    {
        if(bossHealth == null)
        {
            bossHealth = transform.parent.GetComponent<BossHealth>();
        }
    }

    override public void GetDamage(float damage)
    {
        bossHealth.SetHealth(bossHealth.GetHealth() - damage);
        if (bossHealth.GetHealth() <= 0)
        {
            bossHealth.KillBoss();
        }
    }
}

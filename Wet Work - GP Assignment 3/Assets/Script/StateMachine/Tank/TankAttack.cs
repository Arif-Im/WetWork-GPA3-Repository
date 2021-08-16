using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttack : State
{
    public TankAttack(EnemyTankBehavior passedEnemyTankBehavior) : base(passedEnemyTankBehavior)
    {
    }

    // Start is called before the first frame update
    override public IEnumerator Start()
    {
        yield return new WaitForSeconds(0);
        EnemyTankBehavior.lineRenderer.enabled = false;
        EnemyTankBehavior.MoveEnemyTankAttack();
        EnemyTankBehavior.EnemyTankFire();
    }
}

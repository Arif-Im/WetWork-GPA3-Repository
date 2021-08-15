using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAlert : State
{
    public TankAlert(EnemyTankBehavior passedEnemyTankBehavior) : base(passedEnemyTankBehavior)
    {
    }

    override public IEnumerator Start()
    {
        yield return new WaitForSeconds(0);
    }
}

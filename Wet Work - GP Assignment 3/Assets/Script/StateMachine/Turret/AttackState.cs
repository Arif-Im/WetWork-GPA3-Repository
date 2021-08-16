using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

class AttackState : State
{
    public AttackState(EnemyTurretBehavior passedEnemyBehavior) : base(passedEnemyBehavior)
    {
    }

    public override IEnumerator Start()
    {
        yield return new WaitForSeconds(0);

        EnemyBehavior.Fire();
    }
}
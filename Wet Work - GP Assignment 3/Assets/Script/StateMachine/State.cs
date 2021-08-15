using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected EnemyTurretBehavior EnemyBehavior;
    protected BossBehavior BossBehavior;
    protected MovingWallBehavior MovingWallBehavior;
    protected EnemyTankBehavior EnemyTankBehavior;

    protected float distance;
    protected Vector3 targetPosition;
    protected float timeBetweenStates = 5;

    protected State(EnemyTurretBehavior passedEnemyBehavior)
    {
        EnemyBehavior = passedEnemyBehavior;
    }

    protected State(BossBehavior passedBossBehavior)
    {
        BossBehavior = passedBossBehavior;
    }

    protected State(MovingWallBehavior passedMovingWallBehavior)
    {
        MovingWallBehavior = passedMovingWallBehavior;
    }

    protected State(EnemyTankBehavior passedEnemyTankBehavior)
    {
        EnemyTankBehavior = passedEnemyTankBehavior;
    }

    //The following Coroutines are virtual because they are meant to be overridden. If these Coroutines are not overridden,
    //the Coroutines will break immediately.
    public virtual IEnumerator Start()
    {
        yield break;
    }
    public virtual IEnumerator Patrol()
    {
        yield break;
    }
    public virtual IEnumerator Alert()
    {
        yield break;
    }
    public virtual IEnumerator Attack()
    {
        yield break;
    }
    public virtual IEnumerator MissileGunState()
    {
        yield break;
    }
    public virtual IEnumerator MachineGunState()
    {
        yield break;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected EnemyBehavior EnemyBehavior;

    protected float distance;
    protected Vector3 targetPosition;

    protected State(EnemyBehavior passedEnemyBehavior)
    {
        EnemyBehavior = passedEnemyBehavior;
    }

    public void SetTargetPosition(Vector3 position)
    {
        targetPosition = position;
    }

    public Vector3 GetTargetPosition()
    {
        return targetPosition;
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
}

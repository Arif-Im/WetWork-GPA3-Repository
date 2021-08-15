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

    public override IEnumerator Attack()
    {
        yield return new WaitForSeconds(0);
        RaycastHit2D hit = Physics2D.Raycast(EnemyBehavior.transform.position, Vector2.down);

        if (hit.collider != null)
        {
            if (hit.transform.gameObject.layer == EnemyBehavior.layerMaskIndex)
            {
                Debug.DrawRay(EnemyBehavior.transform.position, EnemyBehavior.transform.TransformDirection(EnemyBehavior.idleTarget) * hit.distance, Color.red);
                EnemyBehavior.lineRenderer.SetPosition(1, hit.transform.position);
                EnemyBehavior.Fire();
            }
            else
            {
                Debug.DrawRay(EnemyBehavior.transform.position, EnemyBehavior.transform.TransformDirection(EnemyBehavior.idleTarget) * hit.distance, Color.blue);
                EnemyBehavior.lineRenderer.SetPosition(1, EnemyBehavior.transform.position + (EnemyBehavior.idleTarget * hit.distance));
            }
        }
        else
        {
            Debug.DrawRay(EnemyBehavior.transform.position, EnemyBehavior.transform.TransformDirection(EnemyBehavior.idleTarget) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PatrolState : State
{
    public PatrolState(EnemyTurretBehavior passedEnemyBehavior) : base(passedEnemyBehavior)
    {
    }

    public override IEnumerator Start()
    {
        yield return new WaitForSeconds(0);
        RaycastHit2D hit = Physics2D.Raycast(EnemyBehavior.transform.position, EnemyBehavior.idleTarget);

        var direction = EnemyBehavior.idleTarget;

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            EnemyBehavior.enemySprite.transform.rotation = Quaternion.RotateTowards(EnemyBehavior.enemySprite.transform.rotation, toRotation, 1000 * Time.fixedDeltaTime);
        }

        if (EnemyBehavior.gate.IsGateOpen())
        {
            EnemyBehavior.SetNewState(new AlertState(EnemyBehavior));
        }

        if (hit.collider != null)
        {
            if (hit.transform.gameObject.layer == EnemyBehavior.layerMaskIndex)
            {
                EnemyBehavior.lineRenderer.enabled = false;
                EnemyBehavior.SetNewState(new AttackState(EnemyBehavior));
            }
            else
            {
                Vector2 v2 = EnemyBehavior.transform.position;
                Debug.DrawRay(EnemyBehavior.transform.position, hit.point - v2, Color.blue);
                EnemyBehavior.lineRenderer.SetPosition(1, hit.point);
            }
        }
        else
        {
            Debug.DrawRay(EnemyBehavior.transform.position, EnemyBehavior.transform.TransformDirection(EnemyBehavior.idleTarget) * 1000, Color.white);
        }
    }
}


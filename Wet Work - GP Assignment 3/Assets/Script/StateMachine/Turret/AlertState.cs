using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class AlertState : State
{
    bool startOnce = false;

    public AlertState(EnemyTurretBehavior passedEnemyBehavior) : base(passedEnemyBehavior)
    {
    }

    public override IEnumerator Start()
    {
        yield return new WaitForSeconds(0);
        if(startOnce == false)
        {
            EnemyBehavior.targetPosition.transform.position = EnemyBehavior.idleTarget * 5;
            startOnce = true;
        }
        EnemyBehavior.targetPosition.transform.RotateAround(EnemyBehavior.transform.position, Vector3.forward, 20f * Time.fixedDeltaTime);
        RaycastHit2D hit = Physics2D.Raycast(EnemyBehavior.transform.position, EnemyBehavior.transform.TransformDirection(EnemyBehavior.targetPosition.transform.position) - EnemyBehavior.transform.position);

        if (hit.collider != null)
        {
            if (hit.transform.gameObject.layer == EnemyBehavior.layerMaskIndex)
            {
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
            Debug.DrawRay(EnemyBehavior.transform.position, EnemyBehavior.transform.TransformDirection(EnemyBehavior.targetPosition.transform.position) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}


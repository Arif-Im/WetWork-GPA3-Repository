using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begin : State
{
    public Begin(EnemyBehavior passedEnemyBehavior) : base(passedEnemyBehavior)
    {
    }

    public override IEnumerator Patrol()
    {
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

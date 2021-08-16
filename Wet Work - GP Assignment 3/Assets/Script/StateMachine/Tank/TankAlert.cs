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
        
        EnemyTankBehavior.lineRenderer.SetPosition(0, EnemyTankBehavior.transform.position);
        EnemyTankBehavior.MoveEnemyTankPatrol();

        EnemyTankBehavior.targetPosition.transform.RotateAround(EnemyTankBehavior.transform.position, Vector3.forward, EnemyTankBehavior.rotateSpeed * Time.fixedDeltaTime);
        RaycastHit2D hit = Physics2D.Raycast(EnemyTankBehavior.transform.position, EnemyTankBehavior.transform.TransformDirection(EnemyTankBehavior.targetPosition.transform.position) - EnemyTankBehavior.transform.position);

        var direction = EnemyTankBehavior.transform.TransformDirection(EnemyTankBehavior.targetPosition.transform.position) - EnemyTankBehavior.transform.position;

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            EnemyTankBehavior.enemySprite.transform.rotation = Quaternion.RotateTowards(EnemyTankBehavior.enemySprite.transform.rotation, toRotation, 1000 * Time.fixedDeltaTime);
        }

        if (hit.collider != null)
        {
            if (hit.transform.gameObject.layer == EnemyTankBehavior.layerMaskIndex && Vector2.Distance(hit.transform.gameObject.transform.position, EnemyTankBehavior.transform.position) <= Vector2.Distance(EnemyTankBehavior.transform.position, EnemyTankBehavior.targetPosition.transform.position))
            {
                EnemyTankBehavior.SetNewState(new TankAttack(EnemyTankBehavior));
            }
            else
            {
                if(Vector2.Distance(EnemyTankBehavior.transform.position, hit.point) < Vector2.Distance(EnemyTankBehavior.transform.position, EnemyTankBehavior.targetPosition.transform.position))
                {
                    Vector2 v2 = EnemyTankBehavior.transform.position;
                    Debug.DrawRay(EnemyTankBehavior.transform.position, hit.point - v2, Color.blue);
                    EnemyTankBehavior.lineRenderer.SetPosition(1, hit.point);
                }
                else
                {
                    Debug.DrawRay(EnemyTankBehavior.transform.position, EnemyTankBehavior.targetPosition.transform.position - EnemyTankBehavior.transform.position, Color.blue);
                    EnemyTankBehavior.lineRenderer.SetPosition(1, EnemyTankBehavior.targetPosition.transform.position);
                }
            }
        }
        else
        {
            Debug.DrawRay(EnemyTankBehavior.transform.position, EnemyTankBehavior.transform.TransformDirection(EnemyTankBehavior.movementDirection) * 1000, Color.white);
            EnemyTankBehavior.lineRenderer.SetPosition(1, EnemyTankBehavior.targetPosition.transform.position);
        }
    }
}

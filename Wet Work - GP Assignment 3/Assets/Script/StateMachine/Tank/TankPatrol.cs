﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPatrol : State
{
    public TankPatrol(EnemyTankBehavior passedEnemyTankBehavior) : base(passedEnemyTankBehavior)
    {
    }

    // Start is called before the first frame update
    override public IEnumerator Start()
    {
        yield return new WaitForSeconds(0);
        Vector2 v2 = EnemyTankBehavior.enemyTankRigidbody2D.velocity;
        Vector3 v3 = v2;

        EnemyTankBehavior.lineRenderer.SetPosition(0, EnemyTankBehavior.transform.position);
        EnemyTankBehavior.MoveEnemyTankPatrol();

        RaycastHit2D hit = Physics2D.Raycast(EnemyTankBehavior.transform.position, EnemyTankBehavior.movementDirection);

        var direction = EnemyTankBehavior.movementDirection;

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            EnemyTankBehavior.enemySprite.transform.rotation = Quaternion.RotateTowards(EnemyTankBehavior.enemySprite.transform.rotation, toRotation, 1000 * Time.fixedDeltaTime);
        }

        if (EnemyTankBehavior.gate.IsGateOpen())
        {
            EnemyTankBehavior.SetNewState(new TankAlert(EnemyTankBehavior));
        }

        if (hit.collider != null)
        {
            if (hit.transform.gameObject.layer == EnemyTankBehavior.layerMaskIndex && Vector2.Distance(EnemyTankBehavior.transform.position, EnemyTankBehavior.player.transform.position) <= 1.5)
            {
                EnemyTankBehavior.SetNewState(new TankAttack(EnemyTankBehavior));
            }
            else
            {
                if(Vector2.Distance(EnemyTankBehavior.transform.position, hit.point) < Vector2.Distance(EnemyTankBehavior.transform.position, v3.normalized * 2))
                {
                    Debug.DrawRay(EnemyTankBehavior.transform.position, hit.point - v2, Color.blue);
                    EnemyTankBehavior.lineRenderer.SetPosition(1, hit.point);
                }
                else
                {
                    Debug.DrawRay(EnemyTankBehavior.transform.position, v3.normalized * 2, Color.blue);
                    EnemyTankBehavior.lineRenderer.SetPosition(1, EnemyTankBehavior.transform.position + v3.normalized * 2);
                }
            }
        }
        else
        {
            Debug.DrawRay(EnemyTankBehavior.transform.position, EnemyTankBehavior.transform.TransformDirection(EnemyTankBehavior.movementDirection) * 1000, Color.white);
            EnemyTankBehavior.lineRenderer.SetPosition(1, EnemyTankBehavior.transform.position + v3.normalized * 2);
        }
    }
}

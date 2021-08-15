using System.Collections;
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
        RaycastHit2D hit = Physics2D.Raycast(EnemyTankBehavior.transform.position, EnemyTankBehavior.movementDirection);

        if (EnemyTankBehavior.gate.IsGateOpen())
        {
            EnemyTankBehavior.SetNewState(new TankAlert(EnemyTankBehavior));
        }

        if (hit.collider != null)
        {
            if (hit.transform.gameObject.layer == EnemyTankBehavior.layerMaskIndex)
            {
                EnemyTankBehavior.SetNewState(new TankAttack(EnemyTankBehavior));
            }
            else
            {
                Vector2 v2 = EnemyTankBehavior.transform.position;
                Debug.DrawRay(EnemyTankBehavior.transform.position, hit.point - v2, Color.blue);
                EnemyTankBehavior.lineRenderer.SetPosition(1, hit.point);
                EnemyTankBehavior.MoveEnemyTank();
            }
        }
        else
        {
            Debug.DrawRay(EnemyTankBehavior.transform.position, EnemyTankBehavior.transform.TransformDirection(EnemyTankBehavior.movementDirection) * 1000, Color.white);
        }
    }
}

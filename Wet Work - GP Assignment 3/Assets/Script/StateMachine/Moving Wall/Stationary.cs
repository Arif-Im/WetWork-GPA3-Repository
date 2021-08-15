using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stationary : State
{
    public Stationary(MovingWallBehavior passedMovingWallBehavior) : base(passedMovingWallBehavior)
    {
    }

    // Start is called before the first frame update
    override public IEnumerator Start()
    {
        yield return new WaitForSeconds(0);

        if(MovingWallBehavior.gate.IsGateOpen())
        {
            MovingWallBehavior.SetNewState(new RepeatedActivation(MovingWallBehavior));
        }

        RaycastHit2D hit = Physics2D.Raycast(MovingWallBehavior.transform.position, MovingWallBehavior.idleTarget);

        if (hit.collider != null)
        {
            if (hit.transform.gameObject.layer == MovingWallBehavior.layerMaskIndex)
            {
                MovingWallBehavior.SetNewState(new SingleActivation(MovingWallBehavior));
            }
            else
            {
                Vector2 v2 = MovingWallBehavior.transform.position;
                Debug.DrawRay(MovingWallBehavior.transform.position, hit.point - v2, Color.blue);
            }
        }
        else
        {
            Debug.DrawRay(MovingWallBehavior.transform.position, MovingWallBehavior.transform.TransformDirection(MovingWallBehavior.idleTarget) * 1000, Color.white);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

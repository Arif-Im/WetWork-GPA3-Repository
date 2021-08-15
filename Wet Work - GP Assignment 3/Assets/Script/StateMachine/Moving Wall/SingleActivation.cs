using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleActivation : State
{
    public SingleActivation(MovingWallBehavior passedMovingWallBehavior) : base(passedMovingWallBehavior)
    {
    }

    // Start is called before the first frame update
    override public IEnumerator Start()
    {
        yield return new WaitForSeconds(0);
        MovingWallBehavior.MoveWall();
        if (MovingWallBehavior.IsHitWall())
            MovingWallBehavior.isHitWall = false;
            MovingWallBehavior.SetNewState(new Stationary(MovingWallBehavior));
    }

    
}

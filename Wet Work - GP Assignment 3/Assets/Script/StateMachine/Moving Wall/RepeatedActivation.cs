using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatedActivation : State
{
    public RepeatedActivation(MovingWallBehavior passedMovingWallBehavior) : base(passedMovingWallBehavior)
    {
    }

    // Start is called before the first frame update
    override public IEnumerator Start()
    {
        yield return new WaitForSeconds(0);
        MovingWallBehavior.MoveWall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

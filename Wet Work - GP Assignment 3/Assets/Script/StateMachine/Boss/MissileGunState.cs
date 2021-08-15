using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileGunState : State
{
    public MissileGunState(BossBehavior passedBossBehavior) : base(passedBossBehavior)
    {
    }

    public override IEnumerator Start()
    {
        foreach (GunBehavior missileGun in BossBehavior.missileGuns)
        {
            missileGun.FireGun();
        }
        yield return new WaitForSeconds(timeBetweenStates);
        BossBehavior.SetNewState(new MachineGunState(BossBehavior));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunState : State
{
    public MachineGunState(BossBehavior passedBossBehavior) : base(passedBossBehavior)
    {
    }

    public override IEnumerator Start()
    {
        foreach (GunBehavior machineGun in BossBehavior.machineGuns)
        {
            machineGun.FireGun();
        }
        yield return new WaitForSeconds(timeBetweenStates);
        BossBehavior.SetNewState(new MissileGunState(BossBehavior));
    }
}

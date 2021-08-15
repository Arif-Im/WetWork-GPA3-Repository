using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : StateMachine
{
    [HideInInspector]
    public List<GunBehavior> machineGuns = new List<GunBehavior>();
    [HideInInspector]
    public List<GunBehavior> missileGuns = new List<GunBehavior>();

    private void Start()
    {
        foreach(GunBehavior child in transform.GetComponentsInChildren<GunBehavior>())
        {
            if(child.CompareTag("Machine Gun"))
            {
                machineGuns.Add(child);
            }
            else if(child.CompareTag("Missile Gun"))
            {
                missileGuns.Add(child);
            }
        }
        SetNewState(new MissileGunState(this));
        SetState(GetNewState());
    }

    private void FixedUpdate()
    {
        SetState(GetNewState());
    }
}

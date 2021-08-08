using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : State
{
    PlayerHealth player;

    private void Start()
    {
        player = FindObjectOfType<PlayerHealth>();
    }

    new void SetTargetPosition(Vector3 position)
    {
        targetPosition = player.transform.position;
    }

    new Vector3 GetTargetPosition()
    {
        return targetPosition;
    }
}

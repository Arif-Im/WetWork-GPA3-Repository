using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : State
{
    Vector3 updateRotation = new Vector3(0, 0, 0);
    float startingTimeBetweenUpdates = 0.1f;
    float timeBetweenUpdates;

    void UpdateTargetPosition()
    {
        if (timeBetweenUpdates <= 0)
        {
            SetTargetPosition(updateRotation);
            timeBetweenUpdates = startingTimeBetweenUpdates;
        }
        else
        {
            timeBetweenUpdates -= Time.deltaTime;
        }

    }
}

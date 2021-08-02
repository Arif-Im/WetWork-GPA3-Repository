using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 100;

    public void GetDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Debug.Log("Player is dead");
        }
    }
}

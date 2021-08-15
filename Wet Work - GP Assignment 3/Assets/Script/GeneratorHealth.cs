using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorHealth : EnemyHealth
{
    [Header("Health")]
    [SerializeField] float health = 10;

    GateBehavior gate;

    // Start is called before the first frame update
    void Start()
    {
        gate = FindObjectOfType<GateBehavior>();   
    }

    // Update is called once per frame

    override public void GetDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gate.OpenGate();
        }
    }
}

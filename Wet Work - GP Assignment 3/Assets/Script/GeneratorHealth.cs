using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorHealth : EnemyHealth
{
    [Header("Health")]
    [SerializeField] float health = 10;
    [SerializeField] Slider generatorHealthBarPrefab;

    bool isDead = false;
    Slider generatorHealthbar;

    float startHealth;
    GateBehavior gate;

    // Start is called before the first frame update
    void Start()
    {
        startHealth = health;
        gate = FindObjectOfType<GateBehavior>();   
        if(generatorHealthbar == null)
        {
            generatorHealthbar = Instantiate(generatorHealthBarPrefab);
            generatorHealthbar.transform.SetParent(FindObjectOfType<Canvas>().transform);
            generatorHealthbar.transform.SetAsFirstSibling();
        }
    }

    private void Update()
    {
        if (health <= 0)
        {
            generatorHealthbar.enabled = false;
        }
        generatorHealthbar.value = health / startHealth * generatorHealthbar.maxValue;
        Vector3 pos = Camera.main.WorldToScreenPoint(new Vector2(transform.position.x, transform.position.y + 1));
        generatorHealthbar.transform.position = pos;
    }

    // Update is called once per frame

    override public void GetDamage(float damage)
    {
        if(isDead) { return; }
        health -= damage;
        if (health <= 0)
        {
            gate.SetTotalNumberOfGeneratorsDisabled();
            isDead = true;
        }
    }

    public float GetHealth()
    {
        return health;
    }
}

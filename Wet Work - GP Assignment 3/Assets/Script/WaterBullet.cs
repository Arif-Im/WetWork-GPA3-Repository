using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBullet : Bullet
{
    [Header("Damage")]
    [SerializeField] float waterBulletDamage = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.GetDamage(waterBulletDamage);
        }
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : BossBehavior
{
    [Header("Shooting")]
    [SerializeField] float startingTimeBetweenShots = 1f;
    [SerializeField] float minTime = 0.1f;
    [SerializeField] float maxTime = 1f;
    [SerializeField] float bulletSpeed = 10;
    [SerializeField] Bullet bulletPrefab;

    BulletPool bulletPool;
    float timeBetweenShots = 0;

    PlayerHealth playerPos;

    private void Start()
    {
        bulletPool = GetComponent<BulletPool>();
        playerPos = FindObjectOfType<PlayerHealth>();
    }

    public void FireGun()
    {
        if(timeBetweenShots > 0)
        {
            timeBetweenShots -= Time.deltaTime;
        }
        else
        {
            Bullet enemyBullet = bulletPool.GetPooledBullet();
            if(enemyBullet == null) { return; }
            Rigidbody2D bulletRigidbody2D = enemyBullet.GetComponent<Rigidbody2D>();

            enemyBullet.transform.position = transform.position;
            enemyBullet.gameObject.SetActive(true);
            bulletRigidbody2D.velocity = transform.TransformDirection(playerPos.transform.position - transform.position).normalized * bulletSpeed;
            timeBetweenShots = Random.Range(minTime, maxTime);
        }
    }

    public void FireGun(string message)
    {
        if (timeBetweenShots > 0)
        {
            timeBetweenShots -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Fire " + message);
        }
    }
}

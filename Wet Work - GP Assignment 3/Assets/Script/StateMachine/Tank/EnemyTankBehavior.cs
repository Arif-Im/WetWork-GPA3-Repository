using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankBehavior : StateMachine
{
    [Header("Checkpoints")]
    [SerializeField] GameObject[] checkpoints;
    int checkpointIndex = 0;

    [Header("Movement")]
    [SerializeField] float movementSpeed = 300f;
    [HideInInspector] public Vector3 movementDirection;

    [Header("Shooting")]
    [SerializeField] public int layerMaskIndex = 8;
    [SerializeField] float bulletSpeed = 5;
    [SerializeField] float startingTimeBetweenShots = 1;
    [SerializeField] Bullet bulletPrefab;

    [HideInInspector]
    public GateBehavior gate;

    float timeBetweenShots = 0;

    Rigidbody2D enemyTankRigidbody2D;
    PlayerHealth player;
    BulletPool bulletPool;

    [HideInInspector]
    public LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        enemyTankRigidbody2D = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        bulletPool = GetComponent<BulletPool>();
        player = FindObjectOfType<PlayerHealth>();
        gate = FindObjectOfType<GateBehavior>();
        SetNewState(new TankPatrol(this));
        SetState(GetNewState());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movementDirection = enemyTankRigidbody2D.velocity.normalized;
        SetNewState(GetNewState());
    }

    public void MoveEnemyTank()
    {
        if(transform.position == checkpoints[checkpointIndex].transform.position)
        {
            checkpointIndex++;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, checkpoints[checkpointIndex].transform.position, movementSpeed);
        }
    }

    public void EnemyTankFire()
    {
        if(timeBetweenShots <= 0)
        {
            Bullet enemyBullet = bulletPool.GetPooledBullet();
            Rigidbody2D bulletRigidbody2D = enemyBullet.GetComponent<Rigidbody2D>();

            enemyBullet.transform.position = transform.position;
            enemyBullet.gameObject.SetActive(true);
            var direction = transform.TransformDirection(player.transform.position - transform.position).normalized;
            bulletRigidbody2D.velocity = direction * bulletSpeed;
            timeBetweenShots = startingTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }
}

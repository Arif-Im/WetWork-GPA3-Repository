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
    [SerializeField] float movementSpeed = 3f;
    [HideInInspector] public Vector3 movementDirection;

    [Header("Shooting")]
    [SerializeField] public int layerMaskIndex = 8;
    [SerializeField] float bulletSpeed = 5;
    [SerializeField] float startingTimeBetweenShots = 1;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] public float rotateSpeed = 50;

    [HideInInspector]
    public GateBehavior gate;

    float timeBetweenShots = 0;

    [HideInInspector]
    public Rigidbody2D enemyTankRigidbody2D;
    [HideInInspector]
    public PlayerHealth player;
    BulletPool bulletPool;

    [HideInInspector]
    public LineRenderer lineRenderer;

    [HideInInspector]
    public GameObject targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = new GameObject
        {
            layer = 2
        };
        targetPosition.transform.position = transform.position + new Vector3(0, 5, 0);
        targetPosition.transform.parent = transform;

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
        SetState(GetNewState()); 
    }

    public void MoveEnemyTankPatrol()
    {
        if (Vector2.Distance(transform.position, checkpoints[checkpointIndex].transform.position) <= 0.15f)
        {
            if (checkpointIndex >= checkpoints.Length - 1)
            {
                checkpointIndex = 0;
            }
            else
            {
                checkpointIndex++;
            }
        }
        else
        {
            enemyTankRigidbody2D.velocity = transform.TransformDirection(checkpoints[checkpointIndex].transform.position - transform.position).normalized * movementSpeed * Time.fixedDeltaTime;
        }
    }

    public void MoveEnemyTankAttack()
    {
        enemyTankRigidbody2D.velocity = transform.TransformDirection(player.transform.position - transform.position).normalized * movementSpeed * Time.fixedDeltaTime;
    }

    public void EnemyTankFire()
    {
        var direction = player.transform.position - transform.position.normalized;

        if (timeBetweenShots <= 0)
        {
            Bullet enemyBullet = bulletPool.GetPooledBullet();
            Rigidbody2D bulletRigidbody2D = enemyBullet.GetComponent<Rigidbody2D>();

            enemyBullet.transform.position = transform.position;
            enemyBullet.gameObject.SetActive(true);

            shootSound.Play();
            bulletRigidbody2D.velocity = direction * bulletSpeed;
            timeBetweenShots = startingTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            enemySprite.transform.rotation = Quaternion.RotateTowards(enemySprite.transform.rotation, toRotation, 1000 * Time.fixedDeltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<WaterBullet>())
        {
            WaterBullet waterBullet = collision.gameObject.GetComponent<WaterBullet>();
            enemyTankRigidbody2D.AddForce(-(waterBullet.transform.position - transform.position) * 500);
            waterBullet.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MovingWallBehavior>())
        {
            KillEnemy();
        }
    }

    override public void KillEnemy()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        lineRenderer.enabled = false;
        enemySprite.SetActive(false);
        explosionSound.Play();
        GameObject explosionEffect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        Destroy(explosionEffect, 1f);
        enabled = false;
    }
}

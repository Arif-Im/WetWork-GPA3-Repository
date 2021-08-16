using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

//EnemyBehavior is a subclass of StateMachine. StateMachine is derived from Monobehavior. Thus, EnemyBehavior has access to Monobehavior methods.
public class EnemyTurretBehavior : StateMachine
{
    #region Data Field
    Rigidbody2D enemyRigidbody2D;

    [Header("Raycast")]
    [SerializeField] public int layerMaskIndex = 8;

    [Header("Bullets")]
    [SerializeField] BulletPool bulletPool;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float startingTimeBetweenAttack = 1f;

    [HideInInspector]
    float timeBetweenAttack = 0;
    public State state;
    public LineRenderer lineRenderer;

    [Header("Turret")]
    [SerializeField] public Vector3 idleTarget;

    [HideInInspector] public GameObject targetPosition;
    [HideInInspector] public GateBehavior gate;

    PlayerHealth player;
    #endregion

    #region Execution
    // Start is called before the first frame update
    void Start()
    {
        gate = FindObjectOfType<GateBehavior>();
        targetPosition = new GameObject
        {
            layer = 2
        };
        targetPosition.transform.position = transform.position + new Vector3(0, 5, 0);
        targetPosition.transform.parent = transform;
        lineRenderer = GetComponent<LineRenderer>();
        player = FindObjectOfType<PlayerHealth>();
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
        bulletPool = GetComponent<BulletPool>();
        SetNewState(new PatrolState(this));
        SetState(GetNewState());
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
    }

    private void FixedUpdate()
    {
        SetState(GetNewState());
        if(enemyRigidbody2D.velocity.x > 0 || enemyRigidbody2D.velocity.y > 0)
        {
            enemyRigidbody2D.velocity *= 0.1f;
        }
        else if (enemyRigidbody2D.velocity.x < 0 || enemyRigidbody2D.velocity.y < 0)
        {
            enemyRigidbody2D.velocity *= 0.1f;
        }
    }

    public void Fire()
    {
        if (timeBetweenAttack <= 0)
        {
            Bullet enemyBullet = bulletPool.GetPooledBullet();
            Rigidbody2D bulletRigidbody2D = enemyBullet.GetComponent<Rigidbody2D>();

            enemyBullet.transform.position = transform.position;
            enemyBullet.gameObject.SetActive(true);
            shootSound.Play();
            var direction = (player.transform.position - transform.position).normalized;
            bulletRigidbody2D.velocity = direction * bulletSpeed;
            timeBetweenAttack = startingTimeBetweenAttack;

            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
                enemySprite.transform.rotation = Quaternion.RotateTowards(enemySprite.transform.rotation, toRotation, 1000 * Time.fixedDeltaTime);
            }
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<WaterBullet>())
        {
            WaterBullet waterBullet = collision.gameObject.GetComponent<WaterBullet>();
            enemyRigidbody2D.AddForce(-(waterBullet.transform.position - transform.position) * 500);
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
    #endregion
}

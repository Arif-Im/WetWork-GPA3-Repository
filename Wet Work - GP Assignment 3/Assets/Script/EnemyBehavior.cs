using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

//EnemyBehavior is a subclass of StateMachine. StateMachine is derived from Monobehavior. Thus, EnemyBehavior has access to Monobehavior methods.
public class EnemyBehavior : StateMachine
{
    #region Data Field
    Rigidbody2D enemyRigidbody2D;

    [Header("Raycast")]
    [SerializeField] public int layerMaskIndex = 8;

    [Header("Bullets")]
    [SerializeField] BulletPool bulletPool;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float startingTimeBetweenAttack = 1f;
    float timeBetweenAttack = 0;

    [Header("Turret")]
    [SerializeField] public Vector3 idleTarget;
    public LineRenderer lineRenderer;

    PlayerHealth player;
    #endregion

    #region Execution
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        player = FindObjectOfType<PlayerHealth>();
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
        bulletPool = GetComponent<BulletPool>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);

        if (hit.collider != null)
        {
            if(hit.transform.gameObject.layer == layerMaskIndex)
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(idleTarget) * hit.distance, Color.red);
                lineRenderer.SetPosition(1, hit.transform.position);
                Fire();
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(idleTarget) * hit.distance, Color.blue);
                lineRenderer.SetPosition(1, transform.position + (idleTarget * hit.distance));
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(idleTarget) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    public void Fire()
    {
        if (timeBetweenAttack <= 0)
        {
            EnemyBullet enemyBullet = bulletPool.GetPooledBullet();
            Rigidbody2D bulletRigidbody2D = enemyBullet.GetComponent<Rigidbody2D>();

            enemyBullet.transform.position = transform.position;
            enemyBullet.gameObject.SetActive(true);
            var direction = (transform.position - player.transform.position).normalized;
            bulletRigidbody2D.velocity = -(direction * bulletSpeed);
            timeBetweenAttack = startingTimeBetweenAttack;
        }
        else
        {
            timeBetweenAttack -= Time.deltaTime;
        }
    }
    #endregion

    #region States
    public void BeginPatrolState()
    {
        StartCoroutine(State.Patrol());

    }

    public void BeginAlertState()
    {
        StartCoroutine(State.Alert());
    }

    public void BeginAttackState()
    {
        StartCoroutine(State.Attack());
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EnemyBehavior : MonoBehaviour
{
    Rigidbody2D enemyRigidbody2D;

    [Header("Raycast")]
    [SerializeField] int layerMaskIndex = 8;

    [Header("Bullets")]
    [SerializeField] BulletPool bulletPool;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float startingTimeBetweenAttack = 1f;
    float timeBetweenAttack = 0;

    [Header("Turret")]
    [SerializeField] Vector2 idleTarget;
    LineRenderer lineRenderer;

    PlayerHealth player;

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
                Debug.Log("Did Hit");
                Debug.Log(hit.distance);
                Debug.Log("Player Layer: " + hit.transform.gameObject.layer + " Layer Mask: " + layerMaskIndex);
                Fire();
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(idleTarget) * hit.distance, Color.blue);
                lineRenderer.SetPosition(0, hit.transform.position);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(idleTarget) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    void Fire()
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
}

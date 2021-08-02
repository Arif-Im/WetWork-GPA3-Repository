using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Rigidbody")]
    [SerializeField] Rigidbody2D enemyRigidbody2D;

    [Header("Raycast")]
    [SerializeField] int layerMaskIndex = 8;

    [Header("Bullets")]
    [SerializeField] BulletPool bulletPool;
    [SerializeField] float bulletSpeed = 5f;

    PlayerHealth player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerHealth>();
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
        bulletPool = GetComponent<BulletPool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);

        if (hit.collider != null && hit.transform.gameObject.layer == layerMaskIndex)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            Debug.Log("Player Layer: " + hit.transform.gameObject.layer + " Layer Mask: " + layerMaskIndex);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.down) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    void Fire()
    {
        EnemyBullet enemyBullet = bulletPool.GetPooledBullet();
        Rigidbody2D bulletRigidbody2D = enemyBullet.GetComponent<Rigidbody2D>();

        var direction = (transform.position - player.transform.position).normalized;
        bulletRigidbody2D.velocity = direction * bulletSpeed;
        //var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}

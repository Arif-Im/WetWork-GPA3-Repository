using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileGunBullet : Bullet
{
    PlayerHealth player;
    [SerializeField] protected float changeTrajectorySpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerHealth>();
        bulletRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 v3 = transform.TransformDirection(player.transform.position - transform.position).normalized * changeTrajectorySpeed * Time.fixedDeltaTime;
        Vector2 v2 = v3;
        bulletRigidbody2D.velocity += v2;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] float startingTimeBetweenShots = 0.1f;
    [SerializeField] float waterBulletSpeed;
    [SerializeField] float timeWaterBulletActive = 1f;
    [SerializeField] WaterBullet waterBulletPrefab;
    [SerializeField] AudioSource shootSound;

    BulletPool bulletPool;

    float xDirection;
    float yDirection;
    float timeBetweenShots;

    private void Start()
    {
        bulletPool = GetComponent<BulletPool>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenShots > 0)
        {
            timeBetweenShots -= Time.deltaTime;
        }
        else
        {
            xDirection = 0;
            yDirection = 0;

            if (Input.GetKey(KeyCode.RightArrow))
            {
                xDirection = 1;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                xDirection = -1;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                yDirection = 1;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                yDirection = -1;
            }


            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                Bullet playerBullet = bulletPool.GetPooledBullet();
                Rigidbody2D bulletRigidbody2D = playerBullet.GetComponent<Rigidbody2D>();
                shootSound.Play();

                playerBullet.transform.position = transform.position;
                playerBullet.gameObject.SetActive(true);
                bulletRigidbody2D.velocity = new Vector2(xDirection, yDirection).normalized * waterBulletSpeed;
            }

            timeBetweenShots = startingTimeBetweenShots;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] float startingTimeBetweenShots = 0.1f;
    [SerializeField] float waterBulletSpeed;
    [SerializeField] float timeWaterBulletActive = 1f;
    [SerializeField] WaterBulletBehavior waterBulletPrefab;

    float xDirection;
    float yDirection;
    float timeBetweenShots;
    Rigidbody2D waterBulletRigidbody2D;

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
                WaterBulletBehavior waterBullet = Instantiate(waterBulletPrefab, transform.position, Quaternion.identity);
                waterBulletRigidbody2D = waterBullet.GetComponent<Rigidbody2D>();
                Destroy(waterBullet.gameObject, timeWaterBulletActive);
                waterBulletRigidbody2D.velocity = new Vector2(xDirection, yDirection).normalized * waterBulletSpeed;
            }

            timeBetweenShots = startingTimeBetweenShots;
        }
    }
}

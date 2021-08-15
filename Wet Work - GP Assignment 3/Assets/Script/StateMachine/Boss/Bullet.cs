using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("Bullets")]
    [SerializeField] float damage = 10;
    protected Rigidbody2D bulletRigidbody2D;

    private void Start()
    {
        bulletRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.position.y > 8f ||
            transform.position.y < -8f ||
            transform.position.x > 8f ||
            transform.position.x < -8f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>())
        {
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            player.GetDamage(damage);
            gameObject.SetActive(false);
        }
    }
}

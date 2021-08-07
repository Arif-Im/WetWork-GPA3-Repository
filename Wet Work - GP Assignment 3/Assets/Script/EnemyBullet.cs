using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Vector3 iniPos;

    void Update()
    {
        if (transform.position.y > 8f ||
            transform.position.y < -8f ||
            transform.position.x > 8f ||
            transform.position.x < -8f)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void SetIniPos(Vector3 iniPos)
    {
        this.iniPos = iniPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>())
        {
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
        }
    }
}

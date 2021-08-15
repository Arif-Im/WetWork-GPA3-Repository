using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] List<Bullet> bullets = new List<Bullet>();

    public Bullet GetPooledBullet()
    {
        for(int i = 0; i < bullets.Count; i++)
        {
            if(!bullets[i].gameObject.activeInHierarchy)
            {
                return bullets[i];
            }
        }

        Bullet newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.transform.parent = transform;
        bullets.Add(newBullet);
        return newBullet;
    }
}

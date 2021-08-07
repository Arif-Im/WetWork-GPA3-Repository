using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] EnemyBullet bulletPrefab;
    [SerializeField] List<EnemyBullet> bullets = new List<EnemyBullet>();

    public EnemyBullet GetPooledBullet()
    {
        for(int i = 0; i < bullets.Count; i++)
        {
            if(!bullets[i].gameObject.activeInHierarchy)
            {
                return bullets[i];
            }
        }

        EnemyBullet newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        newBullet.transform.parent = transform;
        bullets.Add(newBullet);
        return newBullet;
    }
}

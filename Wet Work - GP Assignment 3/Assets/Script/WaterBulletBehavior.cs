using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBulletBehavior : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] float waterBulletDamage = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<GeneratorHealth>())
        {
            GeneratorHealth generator = collision.gameObject.GetComponent<GeneratorHealth>();
            generator.GetDamage(waterBulletDamage);
            Destroy(this.gameObject);
        }
    }
}

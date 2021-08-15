using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] SceneLoader sceneLoaderPrefab;
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        if(sceneLoader == null)
        {
            sceneLoader = Instantiate(sceneLoaderPrefab);
        }    
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        StartCoroutine(sceneLoader.ReloadScene());
            if (GetComponent<BoxCollider2D>().enabled == false)
                return;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}

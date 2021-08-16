using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] SceneLoader sceneLoaderPrefab;
    [SerializeField] Slider playerHealthbarPrefab;

    PlayerMovement playerMovement;
    float startHealth;

    Slider playerHealthbar;
    SceneLoader sceneLoader;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        startHealth = health;
        sceneLoader = FindObjectOfType<SceneLoader>();
        if(sceneLoader == null)
        {
            sceneLoader = Instantiate(sceneLoaderPrefab);
        }
        if (playerHealthbar == null)
        {
            playerHealthbar = Instantiate(playerHealthbarPrefab);
            playerHealthbar.transform.SetParent(FindObjectOfType<Canvas>().transform);
            playerHealthbar.transform.SetAsFirstSibling();
        }
    }

    private void Update()
    {
        if (health <= 0)
        {
            playerHealthbar.enabled = false;
        }
        playerHealthbar.value = health / startHealth * playerHealthbar.maxValue;
        Vector3 pos = Camera.main.WorldToScreenPoint(new Vector2(transform.position.x, transform.position.y + 1));
        playerHealthbar.transform.position = pos;
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
        playerMovement.GetPlayerSprite().SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;
    }
}

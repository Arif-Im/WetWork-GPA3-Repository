﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : StateMachine
{
    [Header("Health")]
    [SerializeField] float health = 100;
    [SerializeField] Slider bossHealthbarPrefab;
    [SerializeField] GameObject healthbarPosition;

    Slider bossHealthbar;
    float startHealth;
    SceneLoader sceneLoader;

    private void Start()
    {
        startHealth = health;
        sceneLoader = FindObjectOfType<SceneLoader>();
        if (bossHealthbar == null)
        {
            bossHealthbar = Instantiate(bossHealthbarPrefab);
            bossHealthbar.transform.SetParent(FindObjectOfType<Canvas>().transform);
            bossHealthbar.transform.SetAsFirstSibling();
        }
        bossHealthbar.value = health / startHealth * bossHealthbar.maxValue;
        Vector3 pos = Camera.main.WorldToScreenPoint(healthbarPosition.transform.position);
        bossHealthbar.transform.position = pos;
    }

    private void Update()
    {
        if (health <= 0)
        {
            bossHealthbar.enabled = false;
        }
        bossHealthbar.value = health / startHealth * bossHealthbar.maxValue;
    }

    public void SetHealth(float health)
    {
        this.health = health;
    }

    public float GetHealth()
    {
        return health;
    }

    override public void KillEnemy()
    {
        StartCoroutine(sceneLoader.LoadNextScene());
        explosionSound.Play();
        GameObject explosionEffect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        Destroy(explosionEffect, 1);
        foreach(SpriteRenderer child in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            child.gameObject.SetActive(false);
        }
    }
}

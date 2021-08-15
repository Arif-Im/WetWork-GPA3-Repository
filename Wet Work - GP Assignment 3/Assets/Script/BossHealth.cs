using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float health = 100;
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void SetHealth(float health)
    {
        this.health = health;
    }

    public float GetHealth()
    {
        return health;
    }

    public void KillBoss()
    {
        gameObject.SetActive(false);
    }
}

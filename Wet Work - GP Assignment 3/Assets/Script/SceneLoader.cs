using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Reload")]
    [SerializeField] float loadSceneDuration = 1f;
    [SerializeField] float reloadSceneDuration = 1f;

    public IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(reloadSceneDuration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(loadSceneDuration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    int numberOfMusicPlayers = 0;

    void Awake()
    {
        numberOfMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        if(numberOfMusicPlayers > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}

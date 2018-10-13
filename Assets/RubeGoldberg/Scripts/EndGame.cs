using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{

    public AudioManager audioManager;
    public bool hasCompletedGame;



    //End Game
    public void PressRestart()
    {
        SteamVR_LoadLevel.Begin("Level1");
    }

    public void PlayAudio()
    {
        audioManager.source.clip = audioManager.gameover;
        audioManager.source.Play();

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public AudioManager audioManager;
	public int level = 0;
	public EndGame endGame;
	public Puzzle puzzle;
	public bool isCheating;

	private Vector3 playerPosition {
		get {
			return SteamVR_Render.Top().head.position;
		}
	}

	//Initialize
	void Start () {
	}

   //Audio
	private void PlayAudio () {
		if (audioManager.source.clip != null) {
			audioManager.source.Play ();
		}
	}
    //The loading scheme for levels
	public static void LoadNextStage (int currentLevel) {
		string nextLevel = "";
		switch (currentLevel) {
		case 0:
			nextLevel = "Level1";
			break;
		case 1:
			nextLevel = "Level2";
			break;
		case 2:
			nextLevel = "Level3";
			break;
		case 3:
			nextLevel = "Level4";
			break;
		}

		SteamVR_LoadLevel.Begin (nextLevel);
	}

    //End Game Sequence
	public void EndGame () {
		if (endGame != null) {
			endGame.hasCompletedGame = true;
			endGame.PlayAudio ();
		    endGame.PressRestart();


            foreach (Star star in puzzle.stars) {
				star.gameObject.SetActive (false);
			}
		}
	}

}

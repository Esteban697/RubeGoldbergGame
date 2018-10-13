using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	public GameManager gameManager;

    //Goal reached condition
    void OnTriggerStay(Collider other) {
		if ((gameManager.puzzle.starsCollected == gameManager.puzzle.stars.Count) && (!ValidThrow.notValid))
		{
		    Destroy(other.gameObject);
			print ("goal reached");
            if (gameManager.puzzle.number < 4) {
				GameManager.LoadNextStage (gameManager.puzzle.number);
			} else {
				gameManager.EndGame (); //End Game if all 4 levels completed
                Debug.Log("Game Won");
			}
		}
	}

}

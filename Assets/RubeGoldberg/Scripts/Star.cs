using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

	public Puzzle puzzle;
	public AudioManager audioManager;

	//The collectible stars
	void OnTriggerStay (Collider other) {
		if (other.CompareTag("Throwable") && (!ValidThrow.notValid)) {
			Rigidbody rigidBody = other.GetComponent<Rigidbody> ();
			if (!rigidBody.isKinematic) {
			    audioManager.source.clip = audioManager.stars;
			    audioManager.source.Play();
                gameObject.SetActive (false);
				puzzle.CollectStar ();
			}
		}
	}
}

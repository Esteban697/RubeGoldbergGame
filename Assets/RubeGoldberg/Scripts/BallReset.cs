using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour {

	public GameManager gameManager;
	public AudioSource audioSource;
	public Rigidbody rigidBody;
	public Transform ballStart;
	public float resetInSeconds;


	//Initialization
	void Start () {
		ResetPosition ();
	}
	
	//Compare Tag of Collision
	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.CompareTag("Ground")) {
			PlayAudio ();
			if (gameManager.endGame == null) {
				StartCoroutine (Reset ());
			} else {
				if (!gameManager.endGame.hasCompletedGame) {
					StartCoroutine (Reset ());
				}
			}
		}
	}

	//Play Sound Ball in the Floor
	private void PlayAudio () {
		if (!audioSource.isPlaying) {
			audioSource.Play ();
		}
	}

	//Reseting Position with Coroutine
	public IEnumerator Reset () {
		// Wait for n seconds
		yield return new WaitForSeconds (resetInSeconds);

		// Reset physics
		ResetPhysics ();

		// Reset position and rotation
		ResetPosition ();

		// Reset Puzzle Collectibles
		gameManager.puzzle.ResetStars ();
	}

	private void ResetPhysics () {
		rigidBody.angularVelocity = Vector3.zero;
		rigidBody.velocity = Vector3.zero;
		rigidBody.isKinematic = false;
	}

	public void ResetPosition () {
		transform.position = ballStart.position;
		transform.rotation = Quaternion.identity;
	}
}

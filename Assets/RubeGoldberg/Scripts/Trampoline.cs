using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour {

	public AudioSource audioSource;
	public float bounceMultiplier = 1f;

	//Sound and Physics of Trampolinne
	void OnCollisionEnter(Collision collision) {
		Rigidbody rigidBody = collision.gameObject.GetComponent<Rigidbody> ();
		if (rigidBody != null && rigidBody.CompareTag("Throwable")) {
			rigidBody.AddForce (transform.forward * bounceMultiplier, ForceMode.Impulse);
		}

		if (!audioSource.isPlaying && collision.gameObject.CompareTag("Throwable")) {
			audioSource.Play ();
		}
	}
}

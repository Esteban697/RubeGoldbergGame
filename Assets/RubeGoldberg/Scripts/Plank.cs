using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : MonoBehaviour {


	public AudioSource audioSource;

    //Sound Plank
	void OnCollisionEnter (Collision collision) {
		if (!audioSource.isPlaying && collision.gameObject.CompareTag("Throwable")) {
			audioSource.Play ();
		}
	}
}

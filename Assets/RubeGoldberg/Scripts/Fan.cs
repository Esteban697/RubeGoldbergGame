using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour {

	public int strength = 5;

    //Blowing Fan
	void OnTriggerStay (Collider other) {
		Rigidbody rigidBody = other.gameObject.GetComponent<Rigidbody> ();
		if (rigidBody != null) {
			rigidBody.AddForce (transform.forward * strength);
		}
	}
}

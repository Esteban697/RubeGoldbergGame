using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidThrow : MonoBehaviour {

	public static bool notValid;
	public Material areaMaterial;
    private Renderer areaRenderer;

    // Use this for initialization
    void Start () {
		areaRenderer = GetComponent<Renderer>();
	}

	// Update is called once per frame
	void Update () {
	    if (notValid == true)
	    {
	        areaMaterial.color = Color.red;
	        areaRenderer.material = areaMaterial;
	        //notValid = false;
	    }
    }

    //If the ball is inside the valid zone then the throw is valid
    void OnCollisionStay(Collision other)
	{
	    if (other.gameObject.name == "Ball")
        {
			areaMaterial.color = Color.green;
		    areaRenderer.material = areaMaterial;
		    notValid = false;
		} 
    }

}
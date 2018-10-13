using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandInteraction : MonoBehaviour {

    //Input
    [HideInInspector]
    public SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device device;

    //Left Controller Flag
    public bool Lconrtollerflag;

    //Instructions to destroy after grabbing ball 1st time
    public GameObject instructObject;

    //Variables to Change Ball materials
    public GameObject ballObject;
    private Renderer ballRenderer;
    public Material activeMaterial;
    public Material inactiveMaterial;

    //Throw
    public float throwForce = 2f;

    //Menu variables
	private float swipeSum;
	private float touchLast;
	private float touchCurrent;
	private float distance;
	private bool hasSwipedLeft;
	private bool hasSwipedRight;
	public ObjectMenuManager objectMenuManager;

	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	    ballRenderer = ballObject.GetComponent<Renderer>();
    }

	// Update is called once per frame
	void Update () {
		device = SteamVR_Controller.Input((int)trackedObj.index);

	    if (Lconrtollerflag == false)
	    {

	        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
	        {
	            touchLast = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
	        }

	        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
	        {
	            touchCurrent = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
	            distance = touchCurrent - touchLast;
	            touchLast = touchCurrent;
	            swipeSum += distance;

	            if (!hasSwipedRight)
	            {
	                if (swipeSum > 0.5f)
	                {
	                    swipeSum = 0;
	                    SwipeRight();
	                    hasSwipedRight = true;
	                    hasSwipedLeft = false;
	                }
	            }

	            if (!hasSwipedLeft)
	            {
	                if (swipeSum < -0.5f)
	                {
	                    swipeSum = 0;
	                    SwipeLeft();
	                    hasSwipedLeft = true;
	                    hasSwipedRight = false;
	                }
	            }
	        }

	        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
	        {
	            //Reset Swipe
	            SwipeReset();
	        }

	        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
	        {
	            //Spawn object currently selected by menu
	            SpawnObject();

	        }

	        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
	        {
	            objectMenuManager.EnableDisableMenu();
	        }
	    }
	}

	void SpawnObject(){
		objectMenuManager.SpawnCurrentObject();
	}

	private void SwipeReset() {
		swipeSum = 0;
		touchCurrent = 0;
		touchLast = 0;
		hasSwipedRight = false;
		hasSwipedLeft = false;
	}
		
	void SwipeLeft(){
		objectMenuManager.MenuLeft();
		Debug.Log("SwipeLeft");
	}
	void SwipeRight(){
		objectMenuManager.MenuRight();
		Debug.Log("SwipeRight");
	}

    //Collision with controller's collider
	void OnTriggerStay(Collider col)
	{
		if (col.gameObject.CompareTag("Throwable"))
		{
			if (device.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
			{
					ThrowObject(col);
			}
			else if (device.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
			{
				GrabObject(col);
			}
		}
	}

    //Grab Object
	void GrabObject(Collider coli)
	{
		coli.transform.SetParent(gameObject.transform);
		coli.GetComponent<Rigidbody>().isKinematic = true;
		device.TriggerHapticPulse(1000);
	    if (SceneManager.GetActiveScene().name == "Level1" && coli.gameObject.name == "Ball")
	    {
            instructObject.SetActive(false);
	    }
        SetActive(true);
	}

    //ThrowObject
	void ThrowObject(Collider coli)
	{
		coli.transform.SetParent(null);
		
	    if (coli.gameObject.layer != 11)
	    {
	        Rigidbody rigidBody = coli.GetComponent<Rigidbody>();
            rigidBody.isKinematic = false;
	        rigidBody.velocity = device.velocity * throwForce;
	        rigidBody.angularVelocity = device.angularVelocity;
        }
        SetActive(false);
	}

    //Ball Active Material
    public void SetActive(bool isActive)
    {
        if (isActive)
        {
            ballRenderer.material = activeMaterial;
        }
        else
        {
            ballRenderer.material = inactiveMaterial;
        }
    }
}

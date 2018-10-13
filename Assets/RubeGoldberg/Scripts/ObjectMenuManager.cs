using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectMenuManager : MonoBehaviour {
	
	//Controller Input
	public ControllerInputManager controllerInputManager;
	private SteamVR_Controller.Device controller;

    //Variables
    private GameManager gameManager;
    private float swipeSum;
	private float touchLast;
	private float touchCurrent;
	private float distance;
	private bool hasSwipedLeft;
	private bool hasSwipedRight;
    public bool isActive = false;
    public int currentObject = 0;

    //Lists
    public List<GameObject> objectList; 
	public List<GameObject> objectPrefabList; //Manually set


	//Initialization
	void Start () {

		// Generate the list of objects
		foreach (Transform child in transform) {
			objectList.Add (child.gameObject);
		}
		// And hide all objects 
		foreach (GameObject obj in objectList) {
			obj.SetActive (false);
		}	
	}

	private void InitializeController () {
		// Controller Input Manager
		if (controllerInputManager == null) {
			controllerInputManager = GetComponent<ControllerInputManager> ();
		}
		// Controller
		if (controller == null && controllerInputManager != null) {
			controller = controllerInputManager.device;
		}
	}


	//EnableDisableMenu method
	public void EnableDisableMenu(){
		if (!isActive)
		{
			// show current
			objectList [currentObject].SetActive (true);
		    isActive = true;
		}
		else
		{
			// hide all objects 
			foreach (GameObject obj in objectList) {
				obj.SetActive (false);
			}
		    isActive = false;
		}
	}

	//Swipes methods
	public void MenuLeft () {
		objectList [currentObject].SetActive (false);
		currentObject--;
		if (currentObject < 0) {
			currentObject = objectList.Count - 1;
		}
		objectList [currentObject].SetActive (true);
	
	}
    public void MenuRight () {
		objectList [currentObject].SetActive (false);
		currentObject++;
		if (currentObject > objectList.Count - 1) {
			currentObject = 0;
		}
		objectList [currentObject].SetActive (true);

	}
	
	//Spawn the Curernt Object
	public void SpawnCurrentObject () {
	    if (isActive)
	    {
	        Instantiate(objectPrefabList[currentObject],
	            objectList[currentObject].transform.position,
	            objectList[currentObject].transform.rotation);
        }
	}
}

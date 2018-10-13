using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInputManager : MonoBehaviour {

    //Controller Input
    public SteamVR_TrackedObject trackedObject;
    public SteamVR_Controller.Device device;
    public float touchCurrent;

    // Teleporter
    private LineRenderer laser;
    public GameObject teleportAimerObject;
    public Vector3 teleportLocation;
    public GameObject player;
    public LayerMask laserMask;
    private float laserLength = 5f;
    public static float yNudgeAmount = 0.01f;
    private static readonly Vector3 yNudgeVector = new Vector3(0f, yNudgeAmount, 0f);

    // Use this for initialization
    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        laser = GetComponentInChildren<LineRenderer>();
    }

    void setLaserStart(Vector3 startPos)
    {
        laser.SetPosition(0, startPos);
    }

    void setLaserEnd(Vector3 endPos)
    {
        laser.SetPosition(1, endPos);
    }

    // Update is called once per frame
    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObject.index);

        if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            touchCurrent = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y;

            laserLength = 5 + (touchCurrent * 5);
            laser.gameObject.SetActive(true);
            teleportAimerObject.SetActive(true);
            laser.SetPosition(0, gameObject.transform.position);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, laserLength, laserMask))
            {
                teleportLocation = hit.point;
            }
            else
            {
                teleportLocation = transform.position + laserLength * transform.forward;
            }

            if (teleportLocation.y > 5.0f)
                teleportLocation.y = 5.0f; //Limit teleport height
            if (teleportLocation.y < 0.0f)
                teleportLocation.y = 0.0f; //Limit teleport ground
            if (teleportLocation.z > 17.0f)
                teleportLocation.z = 17.0f;
            if (teleportLocation.z < -14.0f)
                teleportLocation.z = -14.0f;
            if (teleportLocation.x > 12.0f)
                teleportLocation.x = 12.0f;
            if (teleportLocation.x < -13.0f)
                teleportLocation.x = -13.0f; //Game box where you can teleport
            //Laser
            setLaserEnd(teleportLocation);
            //Aimer
            teleportAimerObject.transform.position = teleportLocation + yNudgeVector;
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            laser.gameObject.SetActive(false);
            teleportAimerObject.SetActive(false);
            player.transform.position = teleportLocation + new Vector3(0.0f,0.0f,0.5f);
            ValidThrow.notValid = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameState : MonoBehaviour {
    public GameObject Player;
    public GameObject Flashlight;
    public Text textKeys;
    public bool flashlightOn = false;
    public bool followed = false;
    public bool lastFollowedCheck = false;
    public Follow[] Followers;
    public int keysPickedUp = 0;
    public bool isEnding = false;
    public bool windOn = true;
    public bool inPark = false;
    private static Vector3 PlayerInitPos;
    private static Quaternion PlayerInitRot;
    private float lastX;
    private float lastZ;
    // Use this for initialization
    void Start () {
        PlayerInitPos = Player.transform.position;
        PlayerInitRot = Player.transform.rotation;
        lastX = Player.transform.position.x;
        lastZ = Player.transform.position.z;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        textKeys.text = "Keys Collected: "+keysPickedUp.ToString()+"/6";
        //**************************Work In-Progress
        if ((lastX < Player.transform.position.x - 2.5 || lastX > Player.transform.position.x + 2.5))
        {
            lastX = Player.transform.position.x;
            lastZ = Player.transform.position.z;
            if (inPark)
            {
                Player.gameObject.GetComponent<CustomTrigger>().Hit();
            }
            else if (windOn)
            {
                Player.gameObject.GetComponent<LightOnTrigger>().Hit();
            }
            else
            {
                Player.gameObject.GetComponent<LightOffTrigger>().Hit();
            }
        } else if ((lastZ < Player.transform.position.z - 2.5 || lastZ > Player.transform.position.z + 2.5))
        {
            lastX = Player.transform.position.x;
            lastZ = Player.transform.position.z;
            if (inPark)
            {
                Player.gameObject.GetComponent<CustomTrigger>().Hit();
            }
            else if (windOn)
            {
                Player.gameObject.GetComponent<LightOnTrigger>().Hit();
            }
            else
            {
                Player.gameObject.GetComponent<LightOffTrigger>().Hit();
            }

        }
        //**************************
        Flashlight.GetComponent<Light>().enabled = flashlightOn;
        if (!lastFollowedCheck && followed)
        {
            GetComponent<CustomTrigger>().Hit();
            lastFollowedCheck = followed;
        }
        if (followed)
        {
            for (int i = 0; i < Followers.Length; ++i)
            {
                if (Followers[i].following)
                {
                    break;
                }
                if (i == Followers.Length - 1)
                {
                    followed = false;
                    lastFollowedCheck = followed;
                    GetComponent<StopCustomTrigger>().Hit();
                }
            }
        }
        if (isEnding)
        {
            GetComponent<StoppedChargingTrigger>().Hit();
        }
    }
    public void ResetPlayerPosition()
    {
        inPark = false;
        windOn = true;
        Player.transform.position = PlayerInitPos;
        Player.transform.rotation = PlayerInitRot;
    }

    public void ResetVariables()
    {
        Player.transform.position = PlayerInitPos;
        Player.transform.rotation = PlayerInitRot;
    }
}

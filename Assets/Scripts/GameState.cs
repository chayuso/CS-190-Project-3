using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
    public GameObject Player;
    public GameObject Flashlight;
    public bool flashlightOn = false;
    private static Vector3 PlayerInitPos;
    private static Quaternion PlayerInitRot;
	// Use this for initialization
	void Start () {
        PlayerInitPos = Player.transform.position;
        PlayerInitRot = Player.transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Flashlight.GetComponent<Light>().enabled = flashlightOn;
	}

    public void ResetPlayerPosition()
    {
        Player.transform.position = PlayerInitPos;
        Player.transform.rotation = PlayerInitRot;
    }

    public void ResetVariables()
    {
        Player.transform.position = PlayerInitPos;
        Player.transform.rotation = PlayerInitRot;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingEnter : MonoBehaviour {
    private GameState GS;
    // Use this for initialization
    void Start()
    {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "FPSController")
        {
                GS.gameObject.GetComponent<StoppedChargingTrigger>().Hit();
                GS.windOn = false;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.name == "FPSController")
        {
                GS.gameObject.GetComponent<LightOffTrigger>().Hit();
                GS.windOn = true;
        }
    }
}

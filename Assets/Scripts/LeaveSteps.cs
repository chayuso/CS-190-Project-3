using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveSteps : MonoBehaviour {
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
            GS.inPark = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.name == "FPSController")
        {
            GS.inPark = false;
        }
    }
}

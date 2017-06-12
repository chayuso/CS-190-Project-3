using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Ending : MonoBehaviour {
    private GameState GS;
    private Vector3 FinalPosition;
    public Text Interact;
    private bool playedEnding = false;
    // Use this for initialization
    void Start()
    {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
    }
    void FixedUpdate()
    {
        if (GS.isEnding)
        {
            GS.Player.transform.position = FinalPosition;
        }
    }
    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if (col.name == "FPSController")
        {
            FinalPosition = GS.Player.transform.position;
            GS.isEnding = true;
            Interact.text = "You've escaped the dome!";
            Interact.color = Color.grey;
            Interact.enabled = true;
            if (!playedEnding)
            {
                GetComponent<CustomTrigger>().Hit();
                playedEnding = true;
            }
        }

    }
}

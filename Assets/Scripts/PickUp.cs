using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PickUp : MonoBehaviour {
    public Text Interact;
    public GameObject Key;
    private GameState GS;
    void Start()
    {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
    }
    void OnMouseEnter()
    {
        Interact.text = "Pick Up Key";
        Interact.color = Color.white;
        Interact.enabled = true;

    }
    void OnMouseExit()
    {
        Interact.enabled = false;
    }
    public void OnMouseDown()
    {
        Key.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        ++GS.keysPickedUp;
    }
}

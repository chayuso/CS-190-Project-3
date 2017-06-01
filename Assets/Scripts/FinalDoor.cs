using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FinalDoor : MonoBehaviour {
    public Animator ToggleASM;
    public Text Interact;
    private GameState GS;

    void Start()
    {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
    }

    void OnMouseEnter()
    {
        if (GS.keysPickedUp >= 6)
        {
            Interact.text = "Unlocked: Open/Close";
            Interact.color = Color.white;
            Interact.enabled = true;
        }
        else
        {
            int missing = 6 - GS.keysPickedUp;
            if (missing == 1)
            {
                Interact.text = "Locked: Missing " + missing.ToString() + " Key";
            }
            else
            {
                Interact.text = "Locked: Missing " + missing.ToString() + " Keys";
            }
            Interact.color = Color.white;
            Interact.enabled = true;
        }
    }
    void OnMouseExit()
    {
        Interact.enabled = false;
    }
    public void OnMouseDown()
    {
        if (GS.keysPickedUp >=6)
        {
            ToggleASM.SetTrigger("Toggle");
        }
    }
}

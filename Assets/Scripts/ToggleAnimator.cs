using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ToggleAnimator : MonoBehaviour {
    public Animator ToggleASM;
    public Text Interact;

    void OnMouseEnter()
    {
        Interact.text = "Open/Close";
        Interact.color = Color.white;
        Interact.enabled = true;

    }
    void OnMouseExit()
    {
        Interact.enabled = false;
    }
    public void OnMouseDown()
    {
        ToggleASM.SetTrigger("Toggle");
    }
}

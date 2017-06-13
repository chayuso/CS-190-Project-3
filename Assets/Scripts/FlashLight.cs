using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour {
    private GameState GS;
    public string colname = "";
    public bool keyflash = false;
    private Follow Enemy = null;
    public GameObject key = null;
    // Use this for initialization
    void Start () {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Enemy != null)
        {
            if (GS.flashlightOn)
            {
                if (colname != "")
                {
                    Enemy.flashed = true;
                }
            }
            else
            {
                Enemy.flashed = false;

            }
        }
	}
    void OnTriggerEnter(Collider col)
    {
        if (col is BoxCollider)
        {
            if (col.name.Length >= 5)
            {
                if (col.name.Substring(0, 5) == "Enemy")
                {
                    if (colname == "")
                    {
                        colname = col.name;
                    }
                    Enemy = col.gameObject.GetComponent<Follow>();
                }
            }
            if(col.name.Length >=14)
            {
                if (col.name.Substring(0, 14) == "KeyCollectable")
                {
                    keyflash = true;
                    key = col.gameObject;
                    if (GS.flashlightOn)
                    {
                        col.gameObject.GetComponent<ChargingTrigger>().Hit();
                    }
                }
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col is BoxCollider)
        {
            if (col.name.Length >= 5)
            {
                if (col.name.Substring(0, 5) == "Enemy")
                {
                    if (colname != "")
                    {
                        if (col.name == colname)
                        {
                            Enemy.flashed = false;
                            colname = "";
                            Enemy = null;
                        }
                    }
                }
            }
            if (col.name.Length >= 14)
            {
                if (col.name.Substring(0, 14) == "KeyCollectable")
                {
                    keyflash = false;
                    key = null;
                }
            }
        }
    }
    public void Reset()
    {
        colname = "";
    }
}

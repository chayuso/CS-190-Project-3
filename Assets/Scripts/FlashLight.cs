using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour {
    private GameState GS;
    public string colname = "";
    private Follow Enemy = null;
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
        if (col.name.Length>=5)
        {
            if (col.name.Substring(0,5) == "Enemy")
            {
                if (colname == "")
                {
                    colname = col.name;
                }
                Enemy = col.gameObject.GetComponent<Follow>();
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.name.Length >= 5)
        {
            if (col.name.Substring(0, 5) == "Enemy")
            {
                if (colname!= "")
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
    }
    public void Reset()
    {
        colname = "";
    }
}

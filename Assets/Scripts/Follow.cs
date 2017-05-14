using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {
    public string colname = "";
    public bool following = false;
    public bool flashed = false;
    public bool stunned = false;
    public float speedModifier = 1.0f;
    private GameState GS;
    private static Vector3 InitPos;
    private static Quaternion InitRot;
    private int timeFlashed = 0;
    private int timeStunned = 0;
    // Use this for initialization
    void Start () {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
        InitPos = gameObject.transform.position;
        InitRot = gameObject.transform.rotation;
        StartCoroutine(TimeFlashedRate());
        StartCoroutine(TimeStunnedRate());
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (flashed||stunned)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, gameObject.transform.position, Time.deltaTime * speedModifier);
        }
        else if (following)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, GS.Player.transform.position, Time.deltaTime*speedModifier);
        }
	}
    void OnCollisionEnter(Collision col)
    {
        colname = col.gameObject.name;
        if (colname == "FPSController")
        {
            GS.ResetPlayerPosition();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "FPSController")
        {
            following = true;
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (col.name == "FPSController")
        {
            following = false;
        }
        /*if (col.name == colname)
        {
            colname = "";
        }*/       
    }

    IEnumerator TimeFlashedRate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (flashed && !stunned)
            {
                timeFlashed++;
            }
            if (timeFlashed >= 3)
            {
                stunned = true;
                timeFlashed = 0;
            }
        }
    }

    IEnumerator TimeStunnedRate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (!flashed && stunned)
            {
                timeStunned++;
            }
            if (timeStunned >= 3)
            {
                stunned = false;
                timeStunned = 0;
            }
        }
    }

    public void Reset()
    {
        gameObject.transform.position = InitPos;
        gameObject.transform.rotation = InitRot;
        colname = "";
        timeFlashed = 0;
        timeStunned = 0;
        following = false;
        flashed = false;
        stunned = false;
    }
}

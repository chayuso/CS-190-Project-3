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
    private bool lastFlashed = false;
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
        if (lastFlashed && !flashed)
        {
            GetComponent<LightOffTrigger>().Hit();
            lastFlashed = flashed;
        } else if (!lastFlashed && flashed)
        {
            GetComponent<LightOnTrigger>().Hit();
            lastFlashed = flashed;
        }
        if (flashed||stunned)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, gameObject.transform.position, Time.deltaTime * speedModifier);
        }
        else if (following && !GS.isEnding)
        {
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, GS.Player.transform.position, Time.deltaTime*speedModifier);
        }
        if (GS.isEnding)
        {
            GetComponent<StopCustomTrigger>().Hit();
        }
	}
    void OnCollisionEnter(Collision col)
    {
        colname = col.gameObject.name;
        if (colname == "FPSController")
        {
            GS.ResetPlayerPosition();
            if (!GS.windOn)
            {
                GS.gameObject.GetComponent<LightOffTrigger>().Hit();
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.name == "FPSController")
        {
            GS.followed = true;
            following = true;
            if (!stunned)
            {
                GetComponent<CustomTrigger>().Hit();
            }
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (col.name == "FPSController")
        {
            following = false;
            GetComponent<StopCustomTrigger>().Hit();
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
            if (!flashed)
            {
                timeFlashed = 0;
            }
            else if (flashed && !stunned)
            {
                timeFlashed++;
            }
            if (timeFlashed >= 3)
            {
                stunned = true;
                timeFlashed = 0;
                GetComponent<ChargingTrigger>().Hit();
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
                GetComponent<DoneChargingTrigger>().Hit();
                if (following)
                {
                    GetComponent<CustomTrigger>().Hit();
                }
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

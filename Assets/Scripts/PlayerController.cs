using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private GameState GS;
    public GameObject FPC;
    public bool flashing = false;
    public int timeFlashed = 0;
    public int timeRecharged = 0;
    public bool RechargeNeeded = false;
    public bool Charging = false;
    public Vector3 ChargingPosition;
    // Use this for initialization
    void Start () {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
        StartCoroutine(TimeFlashingRate());
        StartCoroutine(TimeRechargedRate());
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F) && !RechargeNeeded)
        {
            GS.flashlightOn = flashing = !GS.flashlightOn;
            if (flashing)
            {
                GetComponent<LightOnTrigger>().Hit();
            }
            else
            {
                GetComponent<LightOffTrigger>().Hit();
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && RechargeNeeded)
        {
            Charging = true;
            ChargingPosition = GS.Player.transform.position;
            GetComponent<ChargingTrigger>().Hit();
        }
        else if (Input.GetKeyUp(KeyCode.R) && RechargeNeeded)
        {
            Charging = false;
            GetComponent<StoppedChargingTrigger>().Hit();
        }
        if (Charging)
        {
            GS.Player.transform.position = ChargingPosition;
        }
    }
    IEnumerator TimeFlashingRate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (flashing)
            {
                timeFlashed++;
            }
            if (timeFlashed >= 5)
            {
                GS.flashlightOn = false;
                flashing = false;
                RechargeNeeded = true;
                timeFlashed = 0;
                GetComponent<LightOffTrigger>().Hit();
                GetComponent<DoneChargingTrigger>().Hit();
            }
        }
    }
    IEnumerator TimeRechargedRate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (RechargeNeeded && Charging)
            {
                timeRecharged++;
            }
            if (timeRecharged >= 5)
            {
                GetComponent<StoppedChargingTrigger>().Hit();
                RechargeNeeded = false;
                Charging = false;
                timeRecharged = 0;
                GetComponent<DoneChargingTrigger>().Hit();
            }
        }
    }
}

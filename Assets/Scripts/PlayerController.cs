using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private GameState GS;
    public GameObject FPC;
    public GameObject FlashLight;
    public bool flashing = false;
    public int batteryPower = 5;
    public int lastBatteryPower = 5;
    public int timeRecharged = 0;
    public bool RechargeNeeded = false;
    public bool Charging = false;
    public Vector3 ChargingPosition;
    public Image batRed;
    public Image batOrange;
    public Image batYellow;
    public Image batGreen;
    public Image batDarkGreen;
    // Use this for initialization
    void Start () {
        GS = GameObject.Find("GameState").GetComponent<GameState>();
        
        StartCoroutine(TimeFlashingRate());
        StartCoroutine(TimeRechargedRate());
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F)&&batteryPower > 0 && !Charging)
        {
            GS.flashlightOn = flashing = !GS.flashlightOn;
            if (flashing)
            {
                GetComponent<LightOnTrigger>().Hit();
                if (FlashLight.GetComponent<FlashLight>().keyflash)
                {
                    FlashLight.GetComponent<FlashLight>().key.GetComponent<ChargingTrigger>().Hit();
                }
            }
            else
            {
                GetComponent<LightOffTrigger>().Hit();
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && batteryPower < 5 && !flashing)
        {
            Charging = true;
            ChargingPosition = GS.Player.transform.position;
            GetComponent<ChargingTrigger>().Hit();
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            Charging = false;
            GetComponent<StoppedChargingTrigger>().Hit();
        }
        if (Charging)
        {
            GS.Player.transform.position = ChargingPosition;
        }
        batteryGraphic();
    }
    void batteryGraphic()
    {
        if (lastBatteryPower != batteryPower)
        {
            batDarkGreen.enabled = false;
            batGreen.enabled = false;
            batYellow.enabled = false;
            batOrange.enabled = false;
            batRed.enabled = false;
            if (batteryPower == 5)
            {
                batDarkGreen.enabled = true;
                batGreen.enabled = true;
                batYellow.enabled = true;
                batOrange.enabled = true;
                batRed.enabled = true;
            }
            if (batteryPower == 4)
            {
                batGreen.enabled = true;
                batYellow.enabled = true;
                batOrange.enabled = true;
                batRed.enabled = true;
            }
            else if (batteryPower == 3)
            {
                batYellow.enabled = true;
                batOrange.enabled = true;
                batRed.enabled = true;
            }
            else if (batteryPower == 2)
            {
                batOrange.enabled = true;
                batRed.enabled = true;
            }
            else if (batteryPower == 1)
            {
                batRed.enabled = true;
            }
            lastBatteryPower = batteryPower;
        }
    }
    IEnumerator TimeFlashingRate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (flashing && batteryPower > 0 && !Charging)// && !RechargeNeeded)
            {
                batteryPower--;
            }
            if (batteryPower <= 0)
            {
                if (flashing)
                {
                    GetComponent<LightOffTrigger>().Hit();
                }
                GS.flashlightOn = false;
                flashing = false;
                RechargeNeeded = true;
                //GetComponent<DoneChargingTrigger>().Hit();
            }
        }
    }
    IEnumerator TimeRechargedRate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (Charging && !flashing && batteryPower<5)
            {
                batteryPower++;
            }
            if (batteryPower >= 5)
            {
                GetComponent<StoppedChargingTrigger>().Hit();
                RechargeNeeded = false;
                Charging = false;
                //GetComponent<DoneChargingTrigger>().Hit();
            }
        }
    }
}

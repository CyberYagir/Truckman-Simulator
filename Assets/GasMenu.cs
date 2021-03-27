using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class GasMenu : MonoBehaviour
{
    public float tax;
    public TruckManager truckManager;
    public float load, loadspeed = 1;
    public FirstPersonController player;
    public Image indic;
    public TMP_Text textIndic, info, maxLitres;
    public GameObject noTruck;
    public GameObject fuelTruck;
    private void Start()
    {
        truckManager = FindObjectOfType<TruckManager>();
        player = FindObjectOfType<FirstPersonController>();
    }

    private void Update()
    {
        player.enabled = false;
        info.text = $"Info:\nTax: {tax}\nPay: {(int)(tax * load)}$"; 
        if (fuelTruck.active)
        {
            if (Input.GetAxis("Vertical") == 0)
            {
                loadspeed = 0.5f;
            }
            else
            {
                if (tax * load < Player.p.money-1)
                {
                    load += loadspeed * (Input.GetAxis("Vertical") / 2f);
                    loadspeed += Time.deltaTime;
                }
            }
            if (InputManager.GetKey("BuyGet").isDown)
            {
                truckManager.fuel += (int)load;
                Player.p.money -= (int)(load * tax);
                TruckSounds.Pay();
                load = 0;
            }
            if (load < 0) load = 0;
            if (truckManager.fuel + load > truckManager.maxFuel)
                load = truckManager.maxFuel - truckManager.fuel;
            indic.transform.localScale = new Vector3((truckManager.fuel + load) / truckManager.maxFuel, 1, 1);
            textIndic.text = (int)load + " l";
            maxLitres.text = (int)truckManager.maxFuel + " l";
        }
        if (InputManager.GetKey("Close").isDown)
        {
            player.enabled = true;
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public TMP_Text money;
    public TMP_Text food, sleep, fuel;
    public RectTransform foodM, sleepM, fuelM;


    public GasMenu gasMenu;
    public SleepMenu sleepMenu;
    public Image blackScreen;
    public BuyNightMenu nightMenu;
    public EatMenu eatMenu;
    public BuyTrucker buyTrucker;
    public TruckManager truckManager;
    private void Start()
    {
        truckManager = FindObjectOfType<TruckManager>();
    }
    private void Update()
    {
        money.text = Player.p.money.ToString();
        food.text = (int)Player.p.hungry + "%";
        sleep.text = (int)Player.p.sleep + "%";
        fuel.text = (int)truckManager.fuel + "/" + (int)truckManager.maxFuel + " l";
        foodM.sizeDelta = new Vector2(60f, 58f * Player.p.hungry / 100f);
        sleepM.sizeDelta = new Vector2(60f, 58f * Player.p.sleep / 100f);
        fuelM.sizeDelta = new Vector2(60f, 58f * truckManager.fuel / truckManager.maxFuel);
    }
}

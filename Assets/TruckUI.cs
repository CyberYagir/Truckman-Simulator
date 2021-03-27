using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TruckUI : MonoBehaviour
{
    public TruckManager manager;
    public TMP_Text speedT, fuelT, cargoT, cargoHealthT, townsT, hpT;
    public RectTransform fuelM, cargoM, truckM, nightM, hungM;
    public GameObject cargoHolder, townHolder;
    public RawImage engine, lights;
    [Space]
    public TMP_Text money;
    public TMP_Text food, sleep;
    public GameObject map, sidePanel, evacPanel, workersPanel;


    public void CloseAll()
    {
        evacPanel.SetActive(false);
        workersPanel.SetActive(false);
        sidePanel.SetActive(false);
    }

    public bool isOpenSide()
    {
        if (evacPanel.active) return true;
        if (workersPanel.active) return true;
        if (sidePanel.active) return true;

        return false;
    }
    private void Update()
    {
        if (manager.isIn)
        {
            if (InputManager.GetKey("SidePanel").isDown)
            {
                sidePanel.SetActive(!sidePanel.active);
            }
            if (InputManager.GetKey("Map").isDown)
            {
                map.active = !map.active;
            }
        }
        if (manager.isEngine)
        {
            CloseAll();
        }
        money.text = Player.p.money.ToString();
        food .text = (int)Player.p.hungry + "%";
        sleep.text = (int)Player.p.sleep + "%";
        hpT.text = (int)manager.health + "%";
        fuelM.sizeDelta = new Vector2(60f, 58f * (manager.fuel / manager.maxFuel));
        truckM.sizeDelta = new Vector2(81f, 56.6f * (manager.health / 100f));
        nightM.sizeDelta = new Vector2(60f, 58f * (Player.p.sleep / 100f));
        hungM.sizeDelta = new Vector2(60f, 58f * (Player.p.hungry / 100f));

        speedT.text = (int)(manager.truckController.localForwardVelocity * 3.5f) + " kph";
        fuelT.text = manager.fuel.ToString("0000") + "/" + manager.maxFuel.ToString("0000") + " l";
        if (manager.cargo.cargoName != "")
        {
            cargoHolder.SetActive(true);
            townHolder.SetActive(true);
            cargoM.sizeDelta = new Vector2(60f, 58f * (manager.cargo.health / 100f));
            cargoHealthT.text = (int)manager.cargo.health + "%";
            cargoT.text = manager.cargo.cargoName;
            townsT.text = manager.cargo.startTown + ">" + manager.cargo.endTown;
        }
        else
        {
            cargoHolder.SetActive(false);
            townHolder.SetActive(false);
        }
        engine.color = new Color(1, 1, 1, !manager.isEngine ? 0.2f : 0.9f);
        lights.color = new Color(1, 1, 1, !manager.isLights ? 0.2f : 0.9f);
       
    }
    

}

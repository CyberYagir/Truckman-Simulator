using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PayGas : MonoBehaviour
{
    public bool triggered;
    public List<Transform> points;
    public TruckManager truckManager;
    public int currPoint;
    public float tax;
    private void Start()
    {
        truckManager = FindObjectOfType<TruckManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        triggered = other.transform.tag == "Player";
    }
    private void Update()
    {
        currPoint = -1;
        for (int i = 0; i < points.Count; i++)
        {
            if (Vector3.Distance(points[i].transform.position, truckManager.transform.position) < 2)
            {
                currPoint = i;
                break;
            }
        }
        if (triggered)
        {
            Player.p.playerUse.enabled = false;
            Player.p.playerUse.useText.GetComponentInChildren<TMP_Text>().text = "Open [Buy/Get]";
            Player.p.playerUse.useText.gameObject.SetActive(true);
            if (InputManager.GetKey("BuyGet").isDown)
            {
                Player.p.playerUI.gasMenu.gameObject.SetActive(true);
                if (currPoint != -1)
                {
                    Player.p.playerUI.gasMenu.tax = tax;
                    Player.p.playerUI.gasMenu.fuelTruck.gameObject.SetActive(true);
                    Player.p.playerUI.gasMenu.noTruck.gameObject.SetActive(false);
                }
                else
                {
                    Player.p.playerUI.gasMenu.fuelTruck.gameObject.SetActive(false);
                    Player.p.playerUI.gasMenu.noTruck.gameObject.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        triggered = !(other.transform.tag == "Player");
        Player.p.playerUse.enabled = true;
    }
}

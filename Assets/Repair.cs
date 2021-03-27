using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Repair : MonoBehaviour
{
    public List<Transform> points;
    public bool triggered;
    public TruckManager truckManager;
    public int currPoint;
    int tax;
    private void Start()
    {
        tax = Random.Range(45, 60);
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
            if (Vector3.Distance(points[i].transform.position, truckManager.transform.position) < 3.5f)
            {
                currPoint = i;
                break;
            }
        }
        if (triggered)
        {
            Player.p.playerUse.enabled = false;
            if (currPoint != -1)
            {
                Player.p.playerUse.useText.gameObject.SetActive(true);
                if ((int)truckManager.health < 99f)
                {
                    Player.p.playerUse.useText.GetComponentInChildren<TMP_Text>().text = $"Repair {(100 - (int)FindObjectOfType<TruckManager>().health) * tax} $ [Buy/Get]";
                    if ((100 - (int)FindObjectOfType<TruckManager>().health) * tax < Player.p.money)
                    {
                        if (InputManager.GetKey("BuyGet").isDown)
                        {
                            truckManager.health = 100;
                            Player.p.money -= 100 - (int)FindObjectOfType<TruckManager>().health * tax;
                            TruckSounds.Pay();
                            if (truckManager.fuel < truckManager.fuel / 2)
                            {
                                truckManager.fuel = truckManager.fuel / 2;
                            }
                        }
                    }
                }
                else
                {
                    Player.p.playerUse.useText.GetComponentInChildren<TMP_Text>().text = $"Car is perfect";
                }
            }
            else
            {
                Player.p.playerUse.useText.GetComponentInChildren<TMP_Text>().text = $"Park your Truck";
            }
        }
    }

    public static void Fix()
    {
        var truckManager = FindObjectOfType<TruckManager>();
        truckManager.health = 100;
        Player.p.money -= 100 - (int)truckManager.health * Random.Range(45, 60);
        TruckSounds.Pay();
        if (truckManager.fuel < truckManager.fuel / 2)
        {
            truckManager.fuel = truckManager.fuel / 2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        triggered = !(other.transform.tag == "Player");
        Player.p.playerUse.enabled = true;
    }
}

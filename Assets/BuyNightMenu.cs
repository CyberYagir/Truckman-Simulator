using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyNightMenu : MonoBehaviour
{
    public RectTransform indicator;
    float time;
    public float tax;
    public GameObject backpoint;
    public int hours;
    public int payedHourse;
    public TMP_Text countHours, info;
    void Update()
    {
        if (InputManager.GetKey("Close").isDown)
        {
            Player.p.controller.enabled = true;
            gameObject.SetActive(false);
        }
        time += Time.deltaTime;
        if (time > 0.2f)
        {
            hours += 1 * Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
            time = 0;
        }
        if (hours < 1)
        {
            hours = 1;
        }
        if (hours > 24) hours = 24;
        countHours.text = hours + " h";
        info.text = "Tax: " + tax + " $\nPay: " + tax * hours + " $\n";
        indicator.localScale = new Vector3(hours / 24f, 1, 1);
        if (InputManager.GetKey("BuyGet").isDown)
        {
            if (Player.p.money >= tax * hours)
            {
                payedHourse = hours;
                Player.p.money -= tax * hours;
                TruckSounds.Pay();
                Player.p.controller.enabled = true;
                hours = 0;
                gameObject.SetActive(false);
            }
        }
    }
}

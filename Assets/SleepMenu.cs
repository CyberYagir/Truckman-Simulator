using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SleepMenu : MonoBehaviour
{
    public float hours;
    public float hoursSub;
    public TMP_Text hoursT, dateT;


    private void Update()
    {
        hoursT.text = $"{WorldManager.w.viewHours.ToString("00")}:{WorldManager.w.viewMinutes.ToString("00")}";
        dateT.text = $"{WorldManager.w.viewDay.ToString("00")}.{WorldManager.w.viewMounth.ToString("00")}.{System.DateTime.Now.Year + WorldManager.w.year}";

        WorldManager.w.time += (60f * 60f) * Time.deltaTime;
        hoursSub -= 1f * Time.deltaTime;
        if (hoursSub <= 0)
        {
            Player.p.playerUI.nightMenu.payedHourse = 0;
            Player.p.controller.enabled = true;
            Player.p.sleep += hours * Random.Range(10f, 12f);
            if (Player.p.sleep > 100)
            {
                Player.p.sleep = 100;
            }
            gameObject.SetActive(false);
        }
    }
}

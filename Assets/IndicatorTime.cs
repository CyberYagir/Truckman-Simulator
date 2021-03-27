using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class IndicatorTime : MonoBehaviour
{
    public TMP_Text time, date;


    public void Update(){

        time.text = $"{WorldManager.w.viewHours.ToString("00")}:{WorldManager.w.viewMinutes.ToString("00")}";
        date.text = $"{WorldManager.w.viewDay.ToString("00")}.{WorldManager.w.viewMounth.ToString("00")}.{System.DateTime.Now.Year +  WorldManager.w.year}";
    }
}

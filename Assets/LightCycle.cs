using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCycle : MonoBehaviour
{
    public TimeCycle timeCycle;
    public Light light;


    private void Start()
    {
        timeCycle = FindObjectOfType<TimeCycle>();
        light = GetComponent<Light>();
    }

    private void FixedUpdate()
    {
        light.enabled = (timeCycle.time >= 0.8f || timeCycle.time <= 0.33f);
    }

}

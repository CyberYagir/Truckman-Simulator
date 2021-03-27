using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCycle : MonoBehaviour
{
    public WorldManager manager;
    public Light light, lightCabine;
    public float time;
    public float exposure;


    void Update()
    {
        if (exposure > 1) exposure = 1;
        if (exposure < 0f) exposure = 0;
        RenderSettings.skybox.SetFloat("_Exposure", exposure);
        if (time >= 0.8f || time <= 0.33f)
        {
            if (light.intensity > 0)
            {
                light.intensity -= 0.2f * Time.deltaTime;
            }
            if (lightCabine.intensity > 0)
            {
                lightCabine.intensity -= 0.4f * Time.deltaTime;
            }
            exposure -= 0.1f * Time.deltaTime;
        }
        else
        {
            if (light.intensity < 1f)
            {
                light.intensity += 0.3f * Time.deltaTime;
            }
            if (lightCabine.intensity < 1.5f)
            {
                lightCabine.intensity += 0.1f * Time.deltaTime;
            }
            exposure += 0.1f * Time.deltaTime;
        }
        time = manager.viewHours / 24f;
        light.transform.rotation = Quaternion.Lerp(light.transform.rotation, Quaternion.Euler(new Vector3(180f * time, 30, 0)), 0.2f);
    }
}

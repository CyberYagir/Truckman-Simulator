using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{
    public TMP_Dropdown rez;
    public Slider dist;
    public Toggle refl, fog, fullscreen;
    public Config config;


    public void SaveToConfig()
    {
        config.displayConfig.fullscreen = fullscreen.isOn;
        config.displayConfig.distace = dist.value;
        config.displayConfig.refl = refl.isOn;
        config.displayConfig.fog = fog.isOn;
        config.displayConfig.rez = rez.value;
        Screen.SetResolution(int.Parse(rez.options[rez.value].text.Split('x')[0]), int.Parse(rez.options[rez.value].text.Split('x')[1]), fullscreen.isOn);
        QualitySettings.lodBias = dist.value;
        RenderSettings.fog = fog.isOn;
        var list = FindObjectsOfType<ReflectionProbe>();
        for (int i = 0; i < list.Length; i++)
        {
            list[i].enabled = refl.isOn;
        }
    }

    public void LoadToConfig()
    {
        rez.value = config.displayConfig.rez;
        fog.isOn = config.displayConfig.fog;
        refl.isOn = config.displayConfig.refl;
        dist.value = config.displayConfig.distace;
        fullscreen.isOn = config.displayConfig.fullscreen;
        Screen.SetResolution(int.Parse(rez.options[rez.value].text.Split('x')[0]), int.Parse(rez.options[rez.value].text.Split('x')[1]), fullscreen.isOn);
        QualitySettings.lodBias = dist.value;
        RenderSettings.fog = fog.isOn;
        var list = FindObjectsOfType<ReflectionProbe>();
        for (int i = 0; i < list.Length; i++)
        {
            list[i].enabled = refl.isOn;
        }
    }
}

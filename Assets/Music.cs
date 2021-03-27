using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public TMP_Text materT, gameT, menuT;
    public Config config;

    public void Init()
    {
        materT.text = $"Menu Music [{(int)((config.musicConfig.menu / 0.5f) * 100f)}%]";
        materT.text = $"Game Music [{(int)((config.musicConfig.game / 0.5f) * 100f)}%]";
        materT.text = $"Master [{(int)((config.musicConfig.master / 1f)*100f)}%]";
        config.LoadSound();
    }

    public void SetMaster(Slider sl)
    {
        materT.text = $"Master [{(int)((sl.value/ sl.maxValue)*100f)}%]";
        config.musicConfig.master = sl.value;
        config.LoadSound();
    }


    public void SetMenu(Slider sl)
    {
        menuT.text = $"Menu Music [{(int)((sl.value / sl.maxValue) * 100f)}%]";
        config.musicConfig.menu = sl.value;
        config.LoadSound();
    }

    public void SetInGame(Slider sl)
    {
        gameT.text = $"Game Music [{(int)((sl.value / sl.maxValue) * 100f)}%]";
        config.musicConfig.game = sl.value;
    }
}

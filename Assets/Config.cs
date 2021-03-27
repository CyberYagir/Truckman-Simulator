using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
[System.Serializable]
public class DisplayConfig {
    public int rez;
    public float distace;
    public bool refl, fog, fullscreen;
}
[System.Serializable]
public class MusicConfig
{
    public float master = 1, menu = 0.048f, game = 0.1f;
}

[CreateAssetMenu(fileName = "Config", menuName = "Game/Config", order = 1)]
public class Config : ScriptableObject
{
    public List<Inp> inputs;
    public DisplayConfig displayConfig;
    public MusicConfig musicConfig;
    public bool save;


    public void SaveCfg()
    {
        if (!save)
        {
            MonoBehaviour.print("Save");
            FindObjectOfType<Display>().SaveToConfig();
            string path = Path.GetFullPath(Path.Combine(Application.dataPath, @"..\"));
            Directory.CreateDirectory(path + @"\Files\");
            Directory.CreateDirectory(path + @"\Files\Saves\");
            File.Delete(path + @"\Files\inputs.xml");
            XmlSerializer formatter = new XmlSerializer(typeof(List<Inp>));
            using (FileStream fs = new FileStream(path + @"\Files\inputs.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, inputs);
                save = true;
            }
            File.Delete(path + @"\Files\display.xml");
            formatter = new XmlSerializer(typeof(DisplayConfig));
            using (FileStream fs = new FileStream(path + @"\Files\display.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, displayConfig);
                save = true;
            }
            File.Delete(path + @"\Files\sound.xml");
            formatter = new XmlSerializer(typeof(MusicConfig));
            using (FileStream fs = new FileStream(path + @"\Files\sound.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, musicConfig);
                save = true;
            }


            save = false;
        }
    }

    public void LoadCfg()
    {
        string path = Path.GetFullPath(Path.Combine(Application.dataPath, @"..\"));
        if (File.Exists(path + @"\Files\inputs.xml"))
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Inp>));
            using (FileStream fs = new FileStream(path + @"\Files\inputs.xml", FileMode.OpenOrCreate))
            {
                inputs = (List<Inp>)formatter.Deserialize(fs);
            }
        }
        else
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                inputs[i].key = KeyCode.None;
            }
        }
        if (File.Exists(path + @"\Files\display.xml"))
        {
            XmlSerializer formatter = new XmlSerializer(typeof(DisplayConfig));
            using (FileStream fs = new FileStream(path + @"\Files\display.xml", FileMode.OpenOrCreate))
            {
                displayConfig = (DisplayConfig)formatter.Deserialize(fs);
            }
            FindObjectOfType<Display>().LoadToConfig();
        }
        if (File.Exists(path + @"\Files\sound.xml"))
        {
            XmlSerializer formatter = new XmlSerializer(typeof(MusicConfig));
            using (FileStream fs = new FileStream(path + @"\Files\sound.xml", FileMode.OpenOrCreate))
            {
                musicConfig = (MusicConfig)formatter.Deserialize(fs);
                LoadSound();
            }
            FindObjectOfType<Music>().Init();
        }
        else
        {
            musicConfig = new MusicConfig();
        }

    }
    public void LoadSound()
    {
        AudioListener.volume = musicConfig.master;
        var menuMusic = GameObject.FindGameObjectWithTag("MenuMusic");
        if (menuMusic != null) menuMusic.GetComponent<AudioSource>().volume = musicConfig.menu;
        FindObjectOfType<BackgroundMusic>().source.volume = musicConfig.game;

    }
    public void LoadGraphic()
    {
        QualitySettings.lodBias = displayConfig.distace;
        RenderSettings.fog = displayConfig.fog;
        var list = FindObjectsOfType<ReflectionProbe>();
        for (int i = 0; i < list.Length; i++)
        {
            list[i].enabled = displayConfig.refl;
        }
    }
}

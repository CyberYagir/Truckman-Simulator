using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager m;
    public Config cfg;

    private void Start()
    {
        m = this;
    }

    private void Update()
    {
        for (int i = 0; i < cfg.inputs.Count; i++)
        {
            cfg.inputs[i].Set();
        }
    }
    public static Inp GetKey(string name)
    {
        return m.cfg.inputs.Find(x => x.name == name);
    }
    public Inp GetKeyLocal(string name)
    {
        return cfg.inputs.Find(x => x.name == name);
    }
}
[System.Serializable]
public class Inp
{
    public string name;
    public KeyCode key;
    [XmlIgnore]
    public bool isDown;
    [XmlIgnore]
    public bool isPressed;
    [XmlIgnore]
    public bool isUp;

    public void Set()
    {
        isDown = Input.GetKeyDown(key);
        isUp = Input.GetKeyUp(key);
        isPressed = Input.GetKey(key);
    }
}
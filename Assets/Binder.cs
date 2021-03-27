using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Binder : MonoBehaviour
{
    public Image horIndic, vertIndic;
    public BindKey last;
    public InputManager inputManager;
    void Start()
    {
        inputManager.cfg.LoadCfg();

        foreach (var item in FindObjectsOfType<BindKey>())
        {
            item.Init();
        }
    }


    void Update(){
        horIndic.rectTransform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
        vertIndic.rectTransform.localScale = new Vector3(Input.GetAxisRaw("Vertical"), 1, 1);
        if (last != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                last.GetComponent<TMP_Text>().text = "None";
                InputManager.GetKey(last.keyName).key = KeyCode.None;
                last = null;
                return;
            }
            last.GetComponent<TMP_Text>().text = "Press key...";
        }   
    }
    private void OnGUI()
    {
        if (last != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                last.GetComponent<TMP_Text>().text = "None";
                InputManager.GetKey(last.keyName).key = KeyCode.None;
                last = null;
                return;
            }
            var key = Event.current;
            if (key.isKey || key.isMouse)
            {
                last.GetComponent<TMP_Text>().text = key.keyCode.ToString();
                InputManager.GetKey(last.keyName).key = key.keyCode;
                last = null;
            }
            else
            {
                for (int i = 0; i < 20; i++)
                {
                    if (Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), "JoystickButton" + i)))
                    {
                        last.GetComponent<TMP_Text>().text = "JoystickButton" + i;
                        InputManager.GetKey(last.keyName).key = (KeyCode)Enum.Parse(typeof(KeyCode), "JoystickButton" + i);
                        last = null;
                    }
                }
            }
        }
    }
}

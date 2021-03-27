using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeadManager : MonoBehaviour
{
    public static DeadManager deadManager;
    public Image image;
    public TMP_Text text;
    public Transform truck, player;
    public bool dead;
    // Start is called before the first frame update
    void Start()
    {
        deadManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (dead == false)
        {
            image.color = new Color(0, 0, 0, (100f - (Player.p.hp)) / 100f);
            if (Player.p.hp <= 0)
            {
                dead = true;
                text.gameObject.SetActive(true);
            }
        }
        else
        {
            if (Input.anyKeyDown)
            {
                Application.LoadLevel(0);
            }
        }
    }
    public void Check(Transform entered)
    {
        if (entered == player || entered == truck)
        {
            dead = true;
            image.color = new Color(0, 0, 0, 1f);
            text.text = "Drowned!";
            text.gameObject.SetActive(true);
        }
    }
}

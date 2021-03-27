using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    public static Player p;
    public float sleep = 100;
    public float hungry = 100;
    public float money = 1000;
    public PlayerUse playerUse;
    public CargosList cList;
    public PlayerUI playerUI;
    public FirstPersonController controller;
    public float hp = 100f;


    public void Start()
    {
        p = this;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.M) && Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.N) && Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.Y))
        {
            money += (int)(10000 * Time.deltaTime);
        }
        sleep -= (Time.deltaTime * 0.04f);
        hungry -= (Time.deltaTime * 0.06f);
        if (sleep <= 0) sleep = 0;
        if (hungry <= 0) hungry = 0;
        if (hp > 100) hp = 100;

        if (sleep < 10 || hungry < 15)
        {
            hp -= Time.deltaTime / Random.Range(1f, 2f);
        }
        else
        {
            hp += Time.deltaTime / Random.Range(2f, 4f);
        }
    }
}

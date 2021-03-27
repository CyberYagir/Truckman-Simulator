using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUse : MonoBehaviour
{
    public Camera camera;
    public GameObject useText;
    private void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(camera.transform.position - new Vector3(0, 0.2f, 0), camera.transform.forward);
        if (Physics.Raycast(camera.transform.position - new Vector3(0,0.2f,0), camera.transform.forward, out hit, 4f))
        {
            if (hit.transform.tag == "Truck")
            {
                useText.GetComponentInChildren<TMP_Text>().text = "Sit in truck [In/Out]";
                useText.gameObject.SetActive(true);
                if (InputManager.GetKey("TruckerMode").isDown)
                {
                    var m = FindObjectOfType<TruckManager>();
                    if (!m.isIn)
                    {
                        TruckSounds.OutIn();
                        m.truckController.enabled = true;
                        m.isIn = true;
                        gameObject.SetActive(false);
                        m.camera.SetActive(true);
                        for (int i = 0; i < m.mirrors.Length; i++)
                        {
                            m.mirrors[i].gameObject.SetActive(true);
                        }
                    }
                }
            }else
            if (hit.transform.tag == "Bot")
            {
                var w = hit.transform.GetComponent<WorkerBot>();
                if (w != null)
                {
                    var user = WorldManager.w.GetComponent<PlayerBotsManager>().persons[w.userid];
                    if (!user.buyed)
                    {
                        Player.p.playerUI.buyTrucker.gameObject.SetActive(true);
                        Player.p.playerUI.buyTrucker.image.sprite = user.photo;
                        Player.p.playerUI.buyTrucker.nm.text = user.name;
                        Player.p.playerUI.buyTrucker.age.text = "Age: " + user.years + " y.";
                        Player.p.playerUI.buyTrucker.exp.text = "Exp: " + user.expir + " y.";
                        Player.p.playerUI.buyTrucker.pay.text = "Pay: " + user.cost + " $";

                        if (InputManager.GetKey("BuyGet").isDown)
                        {
                            if (MatchManager.matchManager.licenses.Count != 0)
                            {
                                if (Player.p.money >= user.cost)
                                {
                                    MatchManager.matchManager.licenses.RemoveAt(0);
                                    user.buyed = true;
                                    TruckSounds.Pay();
                                    Player.p.money -= user.cost;
                                    w.Buy();
                                }
                            }
                        }
                    }
                    else
                    {
                        Player.p.playerUI.buyTrucker.gameObject.SetActive(false);
                    }
                }
                else
                {
                    Player.p.playerUI.buyTrucker.gameObject.SetActive(false);
                }
            }
            else
            {
                Player.p.playerUI.buyTrucker.gameObject.SetActive(false);
                useText.gameObject.SetActive(false);
            }
        }
        else
        {
            Player.p.playerUI.buyTrucker.gameObject.SetActive(false);
            useText.gameObject.SetActive(false);
        }
    }
}

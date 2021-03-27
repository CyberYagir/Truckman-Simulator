using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class BuyNight : MonoBehaviour
{
    public bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        triggered = other.transform.tag == "Player";
    }
    private void Update()
    {
        if (triggered)
        {
            Player.p.playerUse.enabled = false;
            Player.p.playerUse.useText.GetComponentInChildren<TMP_Text>().text = "Open [Buy/Get]";
            Player.p.playerUse.useText.gameObject.SetActive(true);
            if (InputManager.GetKey("BuyGet").isDown)
            {
                Player.p.playerUI.nightMenu.gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        triggered = !(other.transform.tag == "Player");
        Player.p.playerUse.enabled = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class OpenCargoList : MonoBehaviour
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
                FindObjectOfType<FirstPersonController>().enabled = false;
                Player.p.cList.gameObject.SetActive(true);
                Player.p.cList.UpdateList();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        triggered = !(other.transform.tag == "Player");
        Player.p.playerUse.enabled = true;
    }
}

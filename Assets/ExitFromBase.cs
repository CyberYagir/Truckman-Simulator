using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ExitFromBase : MonoBehaviour
{
    public bool triggered, teleported, teleportActive;
    private void OnTriggerEnter(Collider other)
    {
        triggered = other.transform.tag == "Player";
    }
    IEnumerator waitTeleport()
    {
        Player.p.playerUse.useText.gameObject.SetActive(false);
        teleported = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled = false;
        yield return new WaitForSeconds(0.5f); 
        var t = WorldManager.w.townBases.Find(x => x.townName == Player.p.cList.currentTown).transform;

        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().m_MouseLook.m_CharacterTargetRot = Quaternion.Euler(t.localEulerAngles);
        GameObject.FindGameObjectWithTag("Player").transform.position = t.position;
        Player.p.playerUse.useText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled = true;
        yield return new WaitForSeconds(0.2f);
        teleported = true;
        yield return new WaitForSeconds(0.5f);
        Player.p.playerUI.blackScreen.color = new Color(0, 0, 0, 0);
        Player.p.playerUse.enabled = true;
        teleportActive = false;
    }


    private void Update()
    {
        if (teleportActive)
        {
            Player.p.playerUI.blackScreen.color += new Color(0, 0, 0, (2f * Time.deltaTime) * (teleported ? -1 : 1));
        }
        if (triggered)
        {
            Player.p.playerUse.enabled = false;
            Player.p.playerUse.useText.GetComponentInChildren<TMP_Text>().text = "Exit [Buy/Get]";
            Player.p.playerUse.useText.gameObject.SetActive(true);
            if (InputManager.GetKey("BuyGet").isDown)
            {
                StartCoroutine(waitTeleport());
                GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().enabled = false;
                teleportActive = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        triggered = !(other.transform.tag == "Player");
        Player.p.playerUse.enabled = true;
    }
}

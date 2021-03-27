using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class TeleportPlayer : MonoBehaviour
{
    public bool triggered;
    public TruckManager truckManager;
    public Transform point;
    public bool teleported;
    public bool teleportActive;
    public string fucName;
    public MonoBehaviour script;
    private void Start()
    {
        truckManager = FindObjectOfType<TruckManager>();
    }

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
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().m_MouseLook.m_CharacterTargetRot = Quaternion.Euler(point.transform.localEulerAngles);
        GameObject.FindGameObjectWithTag("Player").transform.position = point.transform.position;
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
            Player.p.playerUse.useText.GetComponentInChildren<TMP_Text>().text = "Open [Buy/Get]";
            Player.p.playerUse.useText.gameObject.SetActive(true);
            if (InputManager.GetKey("BuyGet").isDown && !teleportActive)
            {
                if (script != null)
                {
                    script.Invoke(fucName, 0);
                }
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

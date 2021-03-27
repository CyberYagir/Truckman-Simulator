using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class TownBase : MonoBehaviour
{
    public string townName;
    public List<Transform> points;
    public bool triggered;
    public TruckManager truckManager;
    public int currPoint;
    public bool teleported;
    public bool teleportActive;
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
        Player.p.cList.currentTown = townName;
        Player.p.cList.currPoint = currPoint;
        GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>().m_MouseLook.m_CharacterTargetRot = Quaternion.Euler(GameObject.FindGameObjectWithTag("StationIn").transform.localEulerAngles);
        GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.FindGameObjectWithTag("StationIn").transform.position;
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
        currPoint = -1;
        for (int i = 0; i < points.Count; i++)
        {
            if (Vector3.Distance(points[i].transform.position, truckManager.transform.position) < 3.5f)
            {
                currPoint = i;
                break;
            }
        }
        if (triggered)
        {
            Player.p.playerUse.enabled = false;
            Player.p.playerUse.useText.GetComponentInChildren<TMP_Text>().text = "Open [Buy/Get]";
            Player.p.playerUse.useText.gameObject.SetActive(true);
            if (InputManager.GetKey("BuyGet").isDown && !teleportActive)
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
[System.Serializable]
public class Cargo {
    public string cargoName;
    public int seconds;
    public string company;
    public float health;
    public string startTown, endTown;
    public int cargoType;
    public int tons;
    public float tax;
    [HideInInspector]
    public float timer;
}

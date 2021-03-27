using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public Image player;
    public Vector3 offcet;
    public Transform playerTruck;
    public float del;
    public Image town, parking, bot;
    public List<GameObject> bots;
   
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < MatchManager.matchManager.maxTrucksCount; i++)
        {
            bots.Add(Instantiate(bot.gameObject, transform));
        }

        var k = FindObjectsOfType<Park>();
        for (int i = 0; i < k.Length; i++)
        {
            var g = Instantiate(parking.gameObject, transform);
            g.GetComponent<RectTransform>().localPosition = (new Vector3(k[i].transform.position.x, k[i].transform.position.z) + offcet) / del;
            g.gameObject.SetActive(true);
        }

        var w = FindObjectOfType<WorldManager>();
        for (int i = 0; i < w.townBases.Count; i++)
        {
            var g =Instantiate(town.gameObject, transform);
            g.GetComponent<RectTransform>().localPosition = (new Vector3(w.townBases[i].transform.position.x, w.townBases[i].transform.position.z) + offcet) / del;
            g.GetComponentInChildren<TMP_Text>().text = w.townBases[i].townName;
            g.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player.GetComponent<RectTransform>().localPosition = (new Vector3(playerTruck.transform.position.x, playerTruck.transform.position.z) + offcet) / del;
        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].SetActive(false);
        }
        for (int i = 0; i < MatchManager.matchManager.bots.Count; i++)
        {
            var b = bots[i];
            b.GetComponent<RectTransform>().localPosition = (new Vector3(MatchManager.matchManager.bots[i].transform.position.x, MatchManager.matchManager.bots[i].transform.position.z) + offcet) / del;
            b.gameObject.SetActive(true);
        }
    }
}

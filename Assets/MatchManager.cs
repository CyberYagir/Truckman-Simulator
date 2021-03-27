using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    public static MatchManager matchManager;
    public List<Bot> bots;
    public int maxTrucksCount = 6;
    public TruckManager truckManager;
    public int countEnd;
    public List<License> licenses;
    public GameObject licensesHolder;
    public TMP_Text licensesT;


    [System.Serializable]
    public class License
    {
        public float seconds;
    }
    private void Start()
    {
        matchManager = this;
    }

    public void StartMatch()
    {
        countEnd = 0;
        truckManager = FindObjectOfType<TruckManager>();
        var ots = FindObjectOfType<CarsManager>().spawnedCars.FindAll(x =>Vector3.Distance(x.transform.position, truckManager.transform.position) < 800f);
        int max = 0;
        for (int i = 0; i < ots.Count; i++)
        {
            var g = ots[i].GetComponent<Bot>();
            g.inMach = true;
            var town = WorldManager.w.townBases.Find(x => x.townName == truckManager.cargo.endTown);

            g.point = town.points[Random.Range(0, town.points.Count)];
            g.SetCargo();
            g.agent.SetDestination(g.point.position);
            bots.Add(g);
            max++;
            if (max >= maxTrucksCount) break;
        }
        if (ots.Count <= 2)
        {
            var c = FindObjectOfType<CarsManager>();
            var tr = truckManager;
            for (int i = 0; i < Random.Range(2, 5); i++)
            {
                var b = Instantiate(c.cars[Random.Range(0, c.cars.Count)].gameObject, c.opponents[Random.Range(0, c.opponents.Count)].transform.position, Quaternion.identity);
                b.GetComponent<Bot>().inMach = true;
                var town = WorldManager.w.townBases.Find(x => x.townName == tr.cargo.endTown);
                b.GetComponent<Bot>().point = town.points[Random.Range(0, town.points.Count)];
                b.GetComponent<Bot>().agent.SetDestination(b.GetComponent<Bot>().point.position);
                b.GetComponent<Bot>().SetCargo();
                c.spawnedCars.Add(b);
                bots.Add(b.GetComponent<Bot>());
            }
        }
    }
    private void Update()
    {
        if (licenses.Count == 0)
        {
            licensesHolder.SetActive(false);
            return;
        }
        else
        {
            licensesHolder.SetActive(true);
            licensesT.text = "";
        }
        for (int i = 0; i < licenses.Count; i++)
        {

            licensesT.text += "  License #" + (i + 1) + $" [{(int)licenses[i].seconds} s]  ";
            licenses[i].seconds -= Time.deltaTime;
            if (licenses[i].seconds <= 0)
            {
                licenses.RemoveAt(i);
                break;
            }
        }
    }
    public void StopMatch()
    {
        if (countEnd == 0)
        {
            licenses.Add(new License() {seconds = Random.Range(240, 600) });
        }
        print(countEnd);
        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].inMach = false;
        }
        bots = new List<Bot>();
    }






}

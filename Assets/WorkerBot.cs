using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerBot : MonoBehaviour
{
    public Transform point;
    public NavMeshAgent agent;
    public GameObject[] trailers;
    public int userid;
    // Start is called before the first frame update
    void Start()
    {
        agent.speed = Random.Range(18f, 25f);
        agent.isStopped = true;
        for (int i = 0; i < trailers.Length; i++)
        {
            trailers[i].SetActive(false);
        }
    }

    public void Buy()
    {
        transform.parent = null;
        float min = 999999f;
        int id = -1;

        for (int i = 0; i < WorldManager.w.townBases.Count; i++)
        {
            float dist = Vector3.Distance(WorldManager.w.townBases[i].transform.position, transform.position);
            if (dist < min)
            {
                min = dist;
                id = i;
            }
        }
        agent.isStopped = false;
        point = WorldManager.w.townBases[id].points[0];
        agent.SetDestination(point.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Buy();
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5f))
        {
            if (hit.transform.tag == "Bot" || hit.transform.tag == "Player" || hit.transform.tag == "Truck")
                agent.isStopped = true;
            else
            {
                agent.isStopped = false;
            }
        }
        else
        {
            agent.isStopped = false;
        }
        if (point == null) return;
        if (Vector3.Distance(point.transform.position, transform.position) <= 3f)
        {
            var g = FindObjectOfType<CarsManager>();
            var mn = FindObjectOfType<PlayerBotsManager>();
            if (mn.persons[userid].cargo != null)
            {
                Player.p.money += mn.persons[userid].cargo.tax * 0.6f;
                TruckSounds.Pay();
                mn.persons[userid].pribl += (int)(mn.persons[userid].cargo.tax * 0.6f);
            }
            var tenders = WorldManager.w.tenders.FindAll(x => x.startTown != point.transform.parent.GetComponentInChildren<TownBase>().townName);

            var tender = tenders[Random.Range(0, tenders.Count)];

            mn.persons[userid].cargo = tender;



            var town = WorldManager.w.townBases.Find(x => x.townName == tender.endTown);

            point = town.points[Random.Range(0, town.points.Count)];


            for (int i = 0; i < trailers.Length; i++)
            {
                trailers[i].SetActive(false);
            }
            if (trailers.Length != 0)
                trailers[tender.cargoType].active = true;
            agent.SetDestination(point.position);
        }
    }
}

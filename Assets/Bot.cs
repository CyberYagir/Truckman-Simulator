using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Bot : MonoBehaviour
{
    public Transform point;
    public float speed;
    public NavMeshAgent agent;
    public bool inMach;
    public GameObject[] trailers;
    public TruckManager player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<TruckManager>();
        for (int i = 0; i < trailers.Length; i++)
        {
            trailers[i].SetActive(false);
        }
        agent.SetDestination(point.position);
        speed = Random.Range(17f, 22f);
        agent.speed = speed;
        int r = Random.Range(-1, trailers.Length);
        if (r != -1)
        {
            trailers[r].active = true;
        }
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0,1f,0), transform.forward, out hit, 7f))
        {
            if (hit.collider.transform.tag == "Truck" || hit.collider.transform.tag == "Player")
            {
                agent.velocity = Vector3.zero;
                agent.isStopped = true;
            }
            else if (hit.collider.transform.tag == "Bot" && Vector3.Distance(player.transform.position, transform.position) <= 15f)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
        }
        else
        {
            agent.isStopped = false;
        }

        if (Vector3.Distance(point.transform.position, transform.position) <= 2f)
        {
            for (int i = 0; i < trailers.Length; i++)
            {
                trailers[i].GetComponentInChildren<BoxCollider>().enabled = false;
            }
            agent.speed = 5f;
            var g = FindObjectOfType<CarsManager>();
            if (inMach)
            {
                MatchManager.matchManager.countEnd++;
                MatchManager.matchManager.bots.Remove(this);
                inMach = false;
            }
            point = g.points[Random.Range(0, g.points.Count)];
            agent.SetDestination(point.position);
            int r = Random.Range(-1, trailers.Length);
            for (int i = 0; i < trailers.Length; i++)
            {
                trailers[i].SetActive(false);
            }
            if (r != -1)
            {
                trailers[r].active = true;
            }
        }
        else
        {
            if (Vector3.Distance(player.transform.position, point.transform.position) <= 5f)
            {
                if (Vector3.Distance(point.transform.position, transform.position) < 8f)
                {
                    agent.velocity = Vector3.zero;
                }
            }
            if (Vector3.Distance(player.transform.position, transform.position) <= 15f)
            {
                if (agent.velocity.magnitude > 5f) agent.velocity = agent.velocity / 1.5f;
                if (Vector3.Distance(player.transform.position, transform.position) < 10f)
                {
                    agent.speed = 5f;
                    
                }
                else
                {
                    for (int i = 0; i < trailers.Length; i++)
                    {
                        trailers[i].GetComponentInChildren<BoxCollider>().enabled = true;
                    }
                    agent.speed = speed;
                }
            }
            else
            {
                agent.speed = speed;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        print(agent.velocity.magnitude);
    }
    public void SetCargo()
    {
        for (int i = 0; i < trailers.Length; i++)
        {
            trailers[i].SetActive(false);
        }
        if (trailers.Length != 0)
        {
            int r = Random.Range(0, trailers.Length);
            trailers[r].active = true;
        }
    }
    public void SetCargo(int type)
    {
        for (int i = 0; i < trailers.Length; i++)
        {
            trailers[i].SetActive(false);
        }
        if (trailers.Length != 0)
        {
            trailers[type].active = true;
        }
    }
}

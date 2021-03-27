using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotCollisions : MonoBehaviour
{

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.isTrigger) return;
        if (collision.transform.tag == "Truck" || collision.transform.tag == "Player")
        {
            if (collision.transform.tag == "Player")
            {
                if (FindObjectOfType<TruckManager>().isIn)
                {
                    Player.p.money -= Random.Range(200, 500);
                }
            }
            if (GetComponent<Bot>() != null)
                GetComponent<Bot>().enabled = false;

            if (GetComponent<WorkerBot>() != null)
            {
                var u = WorldManager.w.GetComponent<PlayerBotsManager>().persons[GetComponent<WorkerBot>().userid];
                if (u.cargo != null)
                {
                    u.cargo.tax -= Random.Range(200, 500);
                }
                GetComponent<WorkerBot>().enabled = false;
            }
            GetComponent<NavMeshAgent>().isStopped = true;
            GetComponent<Rigidbody>().isKinematic = false;
            StopAllCoroutines();
            StartCoroutine(wait());
        }
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(Random.Range(10, 30f));

        if (GetComponent<Bot>() != null)
            GetComponent<Bot>().enabled = true;

        if (GetComponent<WorkerBot>() != null)
            GetComponent<WorkerBot>().enabled = true;

        GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(2f);
        GetComponent<NavMeshAgent>().isStopped = false;
    }
}

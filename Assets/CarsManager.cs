using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarsManager : MonoBehaviour
{
    public Transform player;
    public WorldManager w;
    public List<GameObject> cars;
    public List<GameObject> spawnedCars;
    public List<Transform> points;
    public float maxPointDistance, minPointDistance, minOpponentsDistance;
    public List<Transform> opponents;
    public int traffic;

    public void Start()
    {
        StartCoroutine(wait());
    }

    IEnumerator wait(){
        while (true)
        {
            bool isDestroyed = true;
            while (isDestroyed)
            {
                isDestroyed = false;
                for (int i = 0; i < spawnedCars.Count; i++)
                {
                    if (spawnedCars[i].GetComponent<Bot>().inMach) continue;
                    if (Vector3.Distance(player.transform.position, spawnedCars[i].transform.position) > maxPointDistance)
                    {
                        Destroy(spawnedCars[i].gameObject);
                        spawnedCars.RemoveAt(i);
                        isDestroyed = true;
                        break;
                    }
                }
            }
            var list = new List<Transform>();
            var listTargets = new List<Transform>();
            opponents = new List<Transform>();
            for (int i = 0; i < points.Count; i++)
            {
                var dist = Vector3.Distance(player.transform.position, points[i].position);
                if (dist >= minPointDistance && dist <= maxPointDistance)
                {
                    list.Add(points[i]);
                }
                if (dist >= minPointDistance && dist <= minOpponentsDistance)
                {
                    opponents.Add(points[i]);
                }
                if (dist <= maxPointDistance)
                {
                    listTargets.Add(points[i]);
                }
            }
            var p = GameObject.FindGameObjectsWithTag("CargoPoint");
            while (spawnedCars.Count < traffic)
            {
                var pos = list[Random.Range(0, list.Count)].transform.position;
                if (Vector3.Distance(player.transform.position, pos) < minPointDistance) break;
                yield return new WaitForSeconds(1.2f);
                var b = Instantiate(cars[Random.Range(0, cars.Count)], pos, Quaternion.identity);
                spawnedCars.Add(b);
                b.GetComponent<Bot>().point = p[Random.Range(0, p.Length)].transform;            
            }
            yield return new WaitForSeconds(5f);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(player.transform.position, maxPointDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(player.transform.position, minPointDistance);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(player.transform.position, minOpponentsDistance);
    }
}

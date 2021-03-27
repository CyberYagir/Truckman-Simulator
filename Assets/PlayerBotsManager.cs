using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class PlayerBotsManager : MonoBehaviour
{
    public Person[] persons;
    public List<GameObject> bot;
    public Park[] parks;

    private void Start()
    {
        parks = FindObjectsOfType<Park>();

        while (persons.ToList().Find(x=>x.spawnedTruck == null) != null)
        {
            var g = persons.ToList().Find(x => x.spawnedTruck == null);
            var park = parks[Random.Range(0, parks.Length)];
            var point = park.points[Random.Range(0, park.points.Length)];
            while (point.childCount != 0)
            {
                park = parks[Random.Range(0, parks.Length)];
                point = park.points[Random.Range(0, park.points.Length)];
            }

            g.spawnedTruck = Instantiate(g.truck, point.transform.position, point.transform.rotation);
            g.spawnedTruck.GetComponent<WorkerBot>().userid = persons.ToList().FindIndex(x => x == g);
            g.spawnedTruck.transform.parent = point.transform;
        }

    }



    [System.Serializable]
    public class Person
    {
        public string name;
        public int years;
        public int expir;
        public int cost;
        public Sprite photo;
        public GameObject truck, spawnedTruck;
        public Cargo cargo;
        public bool buyed;
        public int pribl;
    }
}

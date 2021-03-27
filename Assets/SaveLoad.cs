using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    [System.Serializable]
    public class Vector
    {
        public float x, y, z;

        public Vector(Vector3 vec)
        {
            this.x = vec.x;
            this.y = vec.y;
            this.z = vec.z;
        }
        public Vector3 Get()
        {
            return new Vector3(x, y, z);
        }
    }
    [System.Serializable]
    public class World {
        public float time;
        public List<Worker> workers = new List<Worker>();
        public Player player;
        public Match match;

        [System.Serializable]
        public class Player {
            public float fuel, truckhp, sleep, hungry, money, hp;
            public Cargo cargo;
            public Vector pos, rot;
            public List<MatchManager.License> licenses = new List<MatchManager.License>();
        }
        [System.Serializable]
        public class Worker {
            public Vector pos;
            public Vector rot;
            public int userid;
            public Cargo cargo;
            public float pribl;
        }
        
        [System.Serializable]
        public class Match
        {
            public List<Vector> botsPoses = new List<Vector>();
            public int count;
        }
        
    }

    public void Update()
    {
    }

    public void Save(string name)
    {
        string path = Path.GetFullPath(Path.Combine(Application.dataPath, @"..\"));
        var world = new World();
        var tr = FindObjectOfType<TruckManager>();
        world.time = WorldManager.w.time;
        world.player = new World.Player() { fuel = tr.fuel, truckhp = tr.health, hungry = Player.p.hungry, money = Player.p.money, sleep = Player.p.sleep, pos = new Vector(tr.transform.position), rot = new Vector(tr.transform.localEulerAngles), licenses = MatchManager.matchManager.licenses, cargo = tr.cargo, hp = Player.p.hp};
        world.match = new World.Match() { count = MatchManager.matchManager.countEnd };
        for (int i = 0; i < MatchManager.matchManager.bots.Count; i++)
        {
            world.match.botsPoses.Add(new Vector(MatchManager.matchManager.bots[i].transform.position));
        }
        var h = FindObjectOfType<PlayerBotsManager>();
        world.workers = new List<World.Worker>();
        for (int i = 0; i < h.persons.Length; i++)
        {
            if (h.persons[i].buyed)
            {
                world.workers.Add(new World.Worker() { pos = new Vector(h.persons[i].spawnedTruck.transform.position), rot = new Vector(h.persons[i].spawnedTruck.transform.localEulerAngles), userid = i, pribl = h.persons[i].pribl, cargo = h.persons[i].cargo });
            }
        }

        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fs = new FileStream(path + @"Files\Saves\" + $"{name}.dat", FileMode.OpenOrCreate))
        {
            // сериализуем весь массив people
            formatter.Serialize(fs, world);

        }
    }

    public void Load(string name)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Path.GetFullPath(Path.Combine(Application.dataPath, @"..\"));
        using (FileStream fs = new FileStream(path + @"Files\Saves\" + $"{name}.dat", FileMode.OpenOrCreate))
        {


            World w = (World)formatter.Deserialize(fs);
            var tr = FindObjectOfType<TruckManager>();

            tr.cargo = w.player.cargo;
            tr.fuel = w.player.fuel;
            tr.health = w.player.truckhp;
            tr.transform.position = w.player.pos.Get();
            tr.transform.localEulerAngles = w.player.rot.Get();

            Player.p.hungry = w.player.hungry;
            Player.p.money = w.player.money;
            Player.p.sleep = w.player.sleep;
            Player.p.hp = w.player.hp;
            MatchManager.matchManager.licenses = w.player.licenses;
            if (tr.cargo.cargoName != "")
            {
                FindObjectOfType<TrailerManager>().StartCargo();
            }
            WorldManager.w.time = w.time;
            var c = FindObjectOfType<CarsManager>();
            for (int i = 0; i < c.spawnedCars.Count; i++)
            {
                Destroy(c.spawnedCars[i].gameObject);
            }
            c.spawnedCars = new List<GameObject>();
            MatchManager.matchManager.bots = new List<Bot>();

            for (int i = 0; i <w.match.botsPoses.Count; i++)
            {
                var b = Instantiate(c.cars[Random.Range(0, c.cars.Count)].gameObject, w.match.botsPoses[i].Get(), Quaternion.identity);
                b.GetComponent<Bot>().inMach = true;
                var town = WorldManager.w.townBases.Find(x => x.townName == tr.cargo.endTown);
                b.GetComponent<Bot>().point = town.points[Random.Range(0, town.points.Count)];
                b.GetComponent<Bot>().agent.SetDestination(b.GetComponent<Bot>().point.position);

                b.GetComponent<Bot>().SetCargo(tr.cargo.cargoType);
                c.spawnedCars.Add(b);
                MatchManager.matchManager.bots.Add(b.GetComponent<Bot>());
            }
            MatchManager.matchManager.countEnd = w.match.count;

            var h = FindObjectOfType<PlayerBotsManager>();
            for (int i = 0; i < w.workers.Count; i++)
            {
                h.persons[w.workers[i].userid].buyed = true;
                h.persons[w.workers[i].userid].spawnedTruck.transform.position = w.workers[i].pos.Get();
                h.persons[w.workers[i].userid].spawnedTruck.transform.localEulerAngles = w.workers[i].rot.Get();
                h.persons[w.workers[i].userid].cargo = w.workers[i].cargo;
                h.persons[w.workers[i].userid].pribl = (int)w.workers[i].pribl;
                var wbot = h.persons[w.workers[i].userid].spawnedTruck.GetComponent<WorkerBot>();

                var town = WorldManager.w.townBases.Find(x => x.townName == w.workers[i].cargo.endTown);

                wbot.point = town.points[Random.Range(0, town.points.Count)];
                wbot.agent.SetDestination(wbot.point.position);
            }
        }
    }
}

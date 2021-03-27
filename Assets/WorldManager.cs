using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public List<string> companies;
    public List<Cargo> cargos = new List<Cargo>();
    public List<Cargo> tenders = new List<Cargo>();
    public List<TownBase> townBases = new List<TownBase>();
    public GameObject trees;
    public static WorldManager w;

    public float time;
    public int day, mounth, year;
    public int minutes, hours;
    public int viewMounth, viewDay, viewMinutes, viewHours;


    private void Start()
    {
        w = this; trees.SetActive(true);
        InputManager.m.cfg.LoadGraphic();
        InputManager.m.cfg.LoadSound();
    }


    private void Update()
    {
        for (int i = 0; i < tenders.Count; i++)
        {
            tenders[i].timer += Time.deltaTime;
            if (tenders[i].timer > tenders[i].seconds)
            {
                tenders.RemoveAt(i);
                if (Player.p.cList.gameObject.active)
                {
                    Player.p.cList.UpdateList();
                }
                break;
            }
        }
        while (tenders.Count < 32)
        {
            if (townBases.Count <= 1) return;
            var c = cargos[Random.Range(0, cargos.Count)];
            int startTown = Random.Range(0, townBases.Count);
            int endTown = Random.Range(0, townBases.Count);
            while (endTown == startTown)
            {
                endTown = Random.Range(0, townBases.Count);
            }

            var newCargo = new Cargo() { cargoName = c.cargoName, tons = Random.Range(10, 25), cargoType = c.cargoType, startTown = townBases[startTown].townName, endTown = townBases[endTown].townName, health = 100, seconds = Random.Range(3600, 3600 * 3), company = companies[Random.Range(0, companies.Count)], tax = Random.Range(900f, 3500f)};
            tenders.Add(newCargo);
        }
        time += Time.deltaTime * 30f;
        minutes = (int)(time / 60f);
        hours = (int)(minutes / 60f);
        day = (int)(hours / 24f);
        mounth = (int)(day / 31f);
        year = (int)(mounth / 12f);
        viewMounth = (int)(mounth - (year * 12f)) + 1;
        viewDay = (int)(day - (mounth * 31f)) + 1;
        viewHours = (int)(hours - (day * 24f)) + 1;
        viewMinutes = (int)(minutes - (hours * 60f)) + 1;

    }
}

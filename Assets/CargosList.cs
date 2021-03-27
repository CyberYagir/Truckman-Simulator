using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class CargosList : MonoBehaviour
{
    public int currPoint;
    public string currentTown;
    public Sprite[] trailers;
    public Transform holder;
    public Transform item;
    public List<CargoItem> cargoItems;
    public int selected;
    public GameObject _get, _lock, _truck, _drop;
    public TruckManager truckManager;
    float time;
    private void Start()
    {
        truckManager = FindObjectOfType<TruckManager>();
    }
    private void Update()
    {
        _get.SetActive(false);
        _lock.SetActive(false);
        _truck.SetActive(false);
        _drop.SetActive(false);
        if (InputManager.GetKey("Close").isDown)
        {
            foreach (Transform item in holder)
            {
                Destroy(item.gameObject);
            }
            gameObject.SetActive(false);
            FindObjectOfType<FirstPersonController>().enabled = true;
        }
        if (truckManager.cargo.cargoName == "" && currPoint != -1)
        {
            _get.SetActive(true);
        }
        else if (truckManager.cargo.cargoName != "" && currPoint != -1)
        {
            if (truckManager.cargo.endTown == currentTown)
            {
                _drop.SetActive(true);
                if (InputManager.GetKey("BuyGet").isDown)
                {
                    Player.p.money += (truckManager.cargo.tax * Vector3.Distance(WorldManager.w.townBases.Find(x => x.townName == truckManager.cargo.startTown).transform.position, WorldManager.w.townBases.Find(x => x.townName == truckManager.cargo.endTown).transform.position) / 2000f) * (truckManager.cargo.health/100f);
                    TruckSounds.Pay(); 
                    truckManager.cargo = new Cargo();
                    truckManager.cargo.cargoName = "";
                    FindObjectOfType<TrailerManager>().StopCargo();
                    MatchManager.matchManager.StopMatch();
                    gameObject.SetActive(false);
                    FindObjectOfType<FirstPersonController>().enabled = true;
                }
                return;
            }
            else
            {
                _lock.SetActive(true);
                if (InputManager.GetKey("BuyGet").isDown)
                { 
                    Player.p.money -= 5000;
                    TruckSounds.Pay();
                    truckManager.cargo = new Cargo();
                    truckManager.cargo.cargoName = "";
                    FindObjectOfType<TrailerManager>().StopCargo();
                    MatchManager.matchManager.StopMatch();
                    gameObject.SetActive(false);
                    FindObjectOfType<FirstPersonController>().enabled = true;
                }
                return;
            }
            return;
        }
        else if (currPoint == -1)
        {
            _truck.SetActive(true);
            return;
        }
        
        time += Time.deltaTime;
        if (time > 0.4f && cargoItems.Count > 1)
        {
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                selected -= 1 * Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
                time = 0;
                if (selected >= cargoItems.Count) selected = 0;
                if (selected <= -1) selected = cargoItems.Count-1;
            }
        }
        if (cargoItems.Count == 1)
        {
            selected = 0;
        }
        if (cargoItems.Count == 0)
        {
            selected = -1;
        }
        if (selected != -1)
        {
            if (InputManager.GetKey("BuyGet").isDown)
            {
                truckManager.cargo = WorldManager.w.tenders[cargoItems[selected].inGlobal];
                WorldManager.w.tenders.RemoveAt(cargoItems[selected].inGlobal);
                FindObjectOfType<TrailerManager>().StartCargo();
                FindObjectOfType<MatchManager>().StartMatch();
                gameObject.SetActive(false);
                FindObjectOfType<FirstPersonController>().enabled = true;
            }
            holder.GetComponent<RectTransform>().localPosition = Vector3.Lerp(holder.GetComponent<RectTransform>().localPosition, new Vector3(0, selected * item.GetComponent<RectTransform>().rect.height, 0), 5f * Time.deltaTime);
            for (int i = 0; i < cargoItems.Count; i++)
            {
                if(i == selected)
                {
                    cargoItems[i].Enter();
                }
                else
                {
                    cargoItems[i].Exit();
                }
            }
        }
        
    }


    public void UpdateList()
    {
        foreach (Transform item in holder)
        {
            Destroy(item.gameObject);
        }
        var l = WorldManager.w.tenders;
        cargoItems = new List<CargoItem>();
        for (int i = 0; i < l.Count; i++)
        {
            if (l[i].startTown == currentTown)
            {
                var g = Instantiate(item, holder).GetComponent<CargoItem>();
                g._name.text = l[i].cargoName + "("+l[i].tons+" t)";
                g.company.text = "Comp.: " + l[i].company;
                g.target.text = "Town: " + l[i].endTown;
                g.inGlobal = i;
                g.image.sprite = trailers[l[i].cargoType];
                g.money.text = (l[i].tax * Vector3.Distance(WorldManager.w.townBases.Find(x => x.townName == l[i].startTown).transform.position, WorldManager.w.townBases.Find(x => x.townName == l[i].endTown).transform.position) / 2000f) + " $";
                g.gameObject.SetActive(true);
                cargoItems.Add(g);
            }
        }
    }
}

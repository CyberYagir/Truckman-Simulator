using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkersList : MonoBehaviour
{
    public List<GameObject> items;
    public Transform holder, item;
    float time;
    public int selected = 0;
    public TMP_Text stats;
    float money;
    public GameObject backMenu;
    private void Start()
    {
        UpdateList();
    }
    private void Update()
    {
        if (InputManager.GetKey("Close").isDown)
        {
            backMenu.SetActive(true);
            gameObject.SetActive(false);
        }
        time += Time.deltaTime;
        if (items.Count != 0)
            holder.transform.localPosition = Vector3.Lerp(holder.transform.localPosition, new Vector3(0f, -(items[selected].transform.localPosition.y), 0), 2f * Time.deltaTime);
        if (time > 0.2f && items.Count != 0)
        {
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                selected += Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
                if (selected < 0) selected = 0;
                if (selected >= items.Count) selected = items.Count - 1;
                time = 0;
                UpdateList();
            }
        }

        stats.text = "Statistics: \nWorkers: " + items.Count + "\nMoney: " + money + " $\nScroll - [Vertical]\nClose - [Close]";

    }
    public void UpdateList()
    {
        items = new List<GameObject>();
        foreach (Transform _item in holder)
        {
            Destroy(_item.gameObject);
        }
        var bots = FindObjectOfType<WorldManager>().GetComponent<PlayerBotsManager>();
        money = 0;
        for (int i = 0; i < bots.persons.Length; i++)
        {
            if (bots.persons[i].buyed == false) continue;

            money += bots.persons[i].pribl; 
            var it = Instantiate(item, holder);
            it.GetChild(0).GetComponent<Image>().sprite = bots.persons[i].photo;
            it.GetChild(1).GetComponent<TMP_Text>().text = bots.persons[i].name;
            if (bots.persons[i].cargo.cargoName != "") {
                it.GetChild(2).GetComponent<TMP_Text>().text = bots.persons[i].cargo.cargoName + $" ({bots.persons[i].cargo.tons} t)";
                it.GetChild(2).GetComponent<TMP_Text>().text += "\n>" + bots.persons[i].cargo.endTown;
                it.GetChild(2).GetComponent<TMP_Text>().text += "\n+" + bots.persons[i].pribl + " $";
            }
            else
            {
                it.GetChild(2).GetComponent<TMP_Text>().text = "Empty";
            }
            items.Add(it.gameObject);
            it.gameObject.SetActive(true);
        }
        for (int i = 0; i < items.Count; i++)
        {
            items[i].GetComponent<Outline>().enabled = i == selected;
        }
    }
}

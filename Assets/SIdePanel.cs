using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SIdePanel : MonoBehaviour
{
    public Button[] buttons;
    public int select;
    public float t;



    private void Update()
    {
        t += Time.deltaTime;
        if (t > 0.4f) {
            if (Input.GetAxis("Vertical") != 0)
            {
                select += Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
                if (select >= buttons.Length)
                {
                    select = 0;
                }
                if (select <= -1)
                {
                    select = buttons.Length - 1;
                }
                t = 0;
            }
        }
        if (InputManager.GetKey("BuyGet").isDown)
        {
            buttons[select].onClick.Invoke();
            EventSystem.current.SetSelectedGameObject(null);
        }
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].transform.GetChild(0).gameObject.SetActive(i == select);
        }
    }

    public void ToRepair()
    {
        var f = GameObject.FindGameObjectsWithTag("RepairPoint");
        float min = 9999999f; int id = 0; ;
        var tr = FindObjectOfType<TruckManager>();
        for (int i = 0; i < f.Length; i++)
        {
            var dist = Vector3.Distance(f[i].transform.position, tr.transform.position);
            if (dist < min)
            {
                min = dist;
                id = i;
            }
        }

        tr.transform.position = f[id].transform.position;
        tr.transform.eulerAngles = f[id].transform.eulerAngles;

        Player.p.money -= 700;
        TruckSounds.Pay();
        Repair.Fix();

    }
    public void ToEvacuate()
    {
        var points = FindObjectOfType<CarsManager>();
        float min = 9999999f; int id = 0;
        var tr = FindObjectOfType<TruckManager>();
        for (int i = 0; i < points.points.Count; i++)
        {
            var dist = Vector3.Distance(points.points[i].transform.position, tr.transform.position);
            if (dist < min)
            {
                min = dist;
                id = i;
            }
        }

        RaycastHit hit;
        Physics.Raycast(points.points[id].transform.position, Vector3.down, out hit);


        tr.transform.position = hit.point + new Vector3(0, 1f, 0);
        tr.transform.eulerAngles = points.points[id].transform.eulerAngles;


        Player.p.money -= 250;
        TruckSounds.Pay();
    }
}

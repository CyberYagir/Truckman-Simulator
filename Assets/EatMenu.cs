using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EatMenu : MonoBehaviour
{
    public List<Eat> menu;
    public Transform item, holder;
    public List<Transform> spawned;
    float time;
    public int selected;
    public Animator animator;

    public void Start()
    {
        for (int i = 0; i < menu.Count; i++)
        {
            var f = Instantiate(item.gameObject, holder).transform;
            f.GetChild(0).GetComponent<Image>().sprite = menu[i].sprite;
            f.GetChild(2).GetComponent<TMP_Text>().text = menu[i].cost + " $";
            spawned.Add(f);
            f.gameObject.SetActive(true);
        }

        for (int i = 0; i < spawned.Count; i++)
        {
            if (i == selected)
            {
                spawned[i].GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                spawned[i].GetChild(1).gameObject.SetActive(false);
            }
        }
    }

    [System.Serializable]
    public class Eat {
        public Sprite sprite;
        public int cost;
        public int add;
    }
    
    void Update()
    {
        if (InputManager.GetKey("Close").isDown)
        {
            Player.p.controller.enabled = true;
            gameObject.SetActive(false);
        }
        time += Time.deltaTime;
        holder.transform.localPosition = Vector3.Lerp(holder.transform.localPosition, new Vector3(-selected * 241.393f, holder.transform.localPosition.y, 0), 2f * Time.deltaTime);
        if (time > 0.3f)
        {
            selected += Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
            if (Mathf.RoundToInt(Input.GetAxisRaw("Vertical")) != 0)
            {
                if (selected < 0)
                {
                    selected = menu.Count - 1;
                }
                if (selected >= menu.Count)
                {
                    selected = 0;
                }

                for (int i = 0; i < spawned.Count; i++)
                {
                    if (i == selected)
                    {
                        spawned[i].GetChild(1).gameObject.SetActive(true);
                    }
                    else
                    {
                        spawned[i].GetChild(1).gameObject.SetActive(false);
                    }
                }
                time = 0;
            }
        }
        if (InputManager.GetKey("BuyGet").isDown)
        {
            if (menu[selected].cost <= Player.p.money)
            {
                Player.p.money -= menu[selected].cost;
                TruckSounds.Pay();
                Player.p.hungry += menu[selected].add;
                animator.Play("HumanEat");
                if (Player.p.hungry > 100) Player.p.hungry = 100;
                Player.p.controller.enabled = true;
                gameObject.SetActive(false);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoadSelect : MonoBehaviour, IPointerClickHandler
{
    public bool selected = false;
    public void Select()
    {
        var all = FindObjectsOfType<LoadSelect>();
        for (int i = 0; i < all.Length; i++)
        {
            all[i].Deselect();
        }
        selected = true;
        transform.GetChild(0).GetComponent<TMP_Text>().color = new Color(1, 1, 1, 0.3f);
        transform.GetChild(1).GetComponent<TMP_Text>().color = new Color(1, 1, 1, 0.3f);
    }

    public void Deselect()
    {
        selected = false;
        transform.GetChild(0).GetComponent<TMP_Text>().color = new Color(1, 1, 1, 1f);
        transform.GetChild(1).GetComponent<TMP_Text>().color = new Color(1, 1, 1, 1f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Select();
    }
}

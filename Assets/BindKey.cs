using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BindKey : MonoBehaviour, IPointerClickHandler
{
    public string keyName;
    public void Init()
    {
        GetComponentInChildren<TMP_Text>().text = FindObjectOfType<InputManager>().GetKeyLocal(keyName).key.ToString();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (FindObjectOfType<Binder>().last == null)
        {
            FindObjectOfType<Binder>().last = this;
        }
    }
}

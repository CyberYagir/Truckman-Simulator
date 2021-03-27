using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CargoItem : MonoBehaviour
{
    public Image image, sel;
    public int inGlobal;
    public TMP_Text _name, company, target, money; 
    public void Enter()
    {
        sel.gameObject.SetActive(true);
    }

    public void Exit()
    {
        sel.gameObject.SetActive(false);
    }
}

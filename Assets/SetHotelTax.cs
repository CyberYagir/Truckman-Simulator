using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHotelTax : MonoBehaviour
{
    public float tax;
    public void SetTax()
    {
        Player.p.playerUI.nightMenu.tax = tax;
        Player.p.playerUI.nightMenu.backpoint = gameObject;
    }
}

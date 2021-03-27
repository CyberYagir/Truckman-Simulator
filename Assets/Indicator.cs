using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public Transform arrow;
    public TruckManager m;
    public int type;


    public void Update(){
        if (type == 0)
            arrow.localEulerAngles = new Vector3(0, (m.fuel/m.maxFuel) * 212.62f, 0);
        
        if (type == 1)
            arrow.localEulerAngles = new Vector3(0, (Mathf.Abs(m.truckController.localForwardVelocity)/50f) * 212.62f, 0);
        
        if (type == 2)
            arrow.localEulerAngles = new Vector3(0, (Mathf.Clamp(Mathf.Abs(m.truckController.axleInfos[0].leftWheel.rpm),0,400000f)/400000f) * 212.62f, 0);
            //-232088.2f
    }
}

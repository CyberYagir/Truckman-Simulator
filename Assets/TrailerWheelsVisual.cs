using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerWheelsVisual : MonoBehaviour
{
    public WheelCollider[] colliders;
    public WheelCollider leftWheel;
    public TruckManager truckManager;
    void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }


    private void Update()
    {
        if (truckManager.isEngine)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].motorTorque = leftWheel.motorTorque;
                ApplyLocalPositionToVisuals(colliders[i]);
            }
        }
        else
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].motorTorque = 0;
                ApplyLocalPositionToVisuals(colliders[i]);
            }
        }
    }
}

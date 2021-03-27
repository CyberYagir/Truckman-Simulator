using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckManager : MonoBehaviour
{
    public float axisX, rotSpeed;
    public float health = 100;
    public GameObject wheel;
    public float fuel, maxFuel, fuelInTime;
    public bool isEngine, isLights;
    public bool isIn;
    public ProTruckController truckController;
    public GameObject fpsPlayer;
    public Transform exitPoint;
    public GameObject camera, lights;
    [HideInInspector]
    public Cargo cargo = null;
    public Camera[] mirrors;
    void Update()
    {

        if (health < 0) health = 0;
        if (cargo.cargoName != "")
        {
            if (cargo.health < 0)
            {
                cargo.health = 0;
            }
        }
        wheel.transform.localEulerAngles = new Vector3(0, 0, 90f * axisX);
        axisX = (Input.GetAxis("Horizontal") * rotSpeed);
        if (isEngine)
        {
            lights.SetActive(isLights);
            fuel -= fuelInTime * Time.deltaTime * (Input.GetAxis("Vertical") + 0.01f);
        }
        if (InputManager.GetKey("Motor").isDown && isIn)
        {
            isEngine = !isEngine;
            if (isEngine)
            {
                TruckSounds.StartEngine();
            }
            else
            {
                TruckSounds.StopEngine();
            }
        }
        if (!isEngine)
            lights.SetActive(false);
        if (InputManager.GetKey("Lights").isDown && isIn)
        {
            isLights = !isLights;
        }
        if (GetComponent<TruckUI>().isOpenSide()) isEngine = false;
        if (fuel <= 0)
        {
            fuel = 0;
            isEngine = false;
        }

        if (InputManager.GetKey("TruckerMode").isDown && isIn && !isEngine)
        {
            if (GetComponent<TruckUI>().isOpenSide()) return;
            RaycastHit hit;
            if (!Physics.Raycast(exitPoint.transform.position, exitPoint.forward, out hit, 1.5f))
            {
                if (GetComponentInChildren<TruckCamera>().Can())
                {
                    if (hit.collider == null)
                    {
                        TruckSounds.OutIn();
                        fpsPlayer.transform.position = exitPoint.transform.position;
                        fpsPlayer.active = true;
                        truckController.enabled = false;
                        isIn = false;
                        camera.SetActive(false);
                        for (int i = 0; i < mirrors.Length; i++)
                        {
                            mirrors[i].gameObject.SetActive(false);
                        }
                    }
                }
                else
                {
                    GetComponentInChildren<TruckCamera>().Back();
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProTruckController : MonoBehaviour
{
    [System.Serializable]
    public class AxleInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor;
        public bool steering;
    }
    public TruckManager truckManager;
    public Rigidbody rb;
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float autobrake;
    public float fullStart;
    public float vertical, localForwardVelocity, OldlocalForwardVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetBrake(float brake)
    {
        foreach (AxleInfo axleInfo in axleInfos)
        {
            axleInfo.leftWheel.motorTorque = 0;
            axleInfo.rightWheel.motorTorque = 0; 
            axleInfo.leftWheel.brakeTorque = brake;
            axleInfo.rightWheel.brakeTorque = brake;
        }
    }
    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
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
    float checkTime = 0.5f;
    private void Update()
    {
        checkTime += Time.deltaTime;
        if (checkTime >= 1f)
        {
            OldlocalForwardVelocity = localForwardVelocity;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(rb.worldCenterOfMass, 0.2f);
    }
    public void FixedUpdate()
    {
        rb.centerOfMass = new Vector3(0, 0.7f, -1f);
        if (Input.GetAxis("Vertical") > 0){
            if (vertical < 0) vertical = 0;
            vertical += (1f/25f) * Time.deltaTime;
        }else if (Input.GetAxis("Vertical") < 0){
            if (vertical > 0) vertical = 0;
            vertical -= (1f/25f) * Time.deltaTime;
        }else{
            if (vertical < -0.01 && vertical > 0.01f){
                if (vertical > 0)
                    vertical -= Time.deltaTime;
                if (vertical < 0)
                    vertical += Time.deltaTime;
            }else
                vertical = 0;
        }
        float motor = maxMotorTorque * vertical;
        float steering = maxSteeringAngle * truckManager.axisX;
        if (InputManager.GetKey("Brake").isPressed) 
        {
            SetBrake(maxMotorTorque*10f);
        }
        else
        {
            if (Input.GetAxis("Vertical") == 0)
            {
                SetBrake(axleInfos[0].leftWheel.motorTorque + autobrake);
            }
            else
                SetBrake(0);
        }

        if (GetComponent<TruckUI>().isOpenSide()) return;
        //info.text = "Motor: " + axleInfos[0].leftWheel.motorTorque + "\n" + "Brake: " + axleInfos[0].leftWheel.brakeTorque;
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (truckManager.isEngine)
            {
                if (axleInfo.motor)
                {
                    if (!InputManager.GetKey("Brake").isPressed)
                    {
                        if (Input.GetAxis("Vertical") != 0)
                        {
                            if (localForwardVelocity < 0 && Input.GetAxis("Vertical") > 0)
                            {
                                SetBrake(maxMotorTorque * maxMotorTorque);
                            }
                            else if (localForwardVelocity > 0 && Input.GetAxis("Vertical") < 0)
                            {
                                SetBrake(maxMotorTorque * maxMotorTorque);
                            }
                            else
                            {
                                axleInfo.leftWheel.motorTorque = motor * (truckManager.health / 100f);
                                axleInfo.rightWheel.motorTorque = motor * (truckManager.health / 100f);
                                if (axleInfo.leftWheel.motorTorque > maxMotorTorque)
                                {
                                    axleInfo.leftWheel.motorTorque = maxMotorTorque;
                                    axleInfo.rightWheel.motorTorque = maxMotorTorque;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                axleInfo.leftWheel.motorTorque = 0;
                axleInfo.rightWheel.motorTorque = 0;

                if (localForwardVelocity < 0 && Input.GetAxis("Vertical") > 0)
                {
                    SetBrake(maxMotorTorque * maxMotorTorque);
                }
                else if (localForwardVelocity > 0 && Input.GetAxis("Vertical") < 0)
                {
                    SetBrake(maxMotorTorque * maxMotorTorque);
                }
            }

            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
        if (-Mathf.Abs((localForwardVelocity - OldlocalForwardVelocity) * 1000f) < -1500)
        {
            if (truckManager.cargo != null)
            {
                SetBrake(maxMotorTorque * 10f);
                truckManager.cargo.health += (-Mathf.Abs((localForwardVelocity - OldlocalForwardVelocity) * 1000f)/2000f);
                truckManager.health += (-Mathf.Abs((localForwardVelocity - OldlocalForwardVelocity) * 1000f) / 2500f);
            }
        }
        localForwardVelocity = Vector3.Dot(rb.velocity, transform.forward);
    }
}
    

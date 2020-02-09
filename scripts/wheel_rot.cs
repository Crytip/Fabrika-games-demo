using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheel_rot : MonoBehaviour
{
    private WheelCollider WC;
    private int speed;
    private void Start()
    {
        speed = 350;
        WC = GetComponent<WheelCollider>();
        WC.steerAngle = 90;
    }

    void Update()
    {
        WC.motorTorque = 1000;
        transform.Rotate(Vector3.forward*speed*Time.deltaTime);

    }
}

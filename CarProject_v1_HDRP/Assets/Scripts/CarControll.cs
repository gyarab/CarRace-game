using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControll : MonoBehaviour
{
    // Ovladani auta 
    private float horizontalInput;
    private float verticalInput;
    private float SteerAngleVar;

    

  
    public float maxSteerAndgle = 30;
    private float MotorForce = 500;

    //hodnoty pred zmenou vychyleni kol
    public WheelCollider LeftFront, RightFront;
    public WheelCollider LeftBack, RightBack;

    //hodnoty po zmene vychyleni kol
    public Transform leftFrontT, RightFrontT;
    public Transform LeftBackT, RightBackT;

    public void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }
    public void Steer()
    {
        SteerAngleVar = maxSteerAndgle * horizontalInput;
        LeftFront.steerAngle = SteerAngleVar;
        RightFront.steerAngle = SteerAngleVar;
            
    }

    public void Accelerate()
    {
        LeftFront.motorTorque = verticalInput * MotorForce;
        RightFront.motorTorque = verticalInput * MotorForce;

    }
    private void UpdateWheelPositions()
    {
        UpdateWheelPosition(LeftFront, leftFrontT);
        UpdateWheelPosition(RightFront, RightFrontT);
        UpdateWheelPosition(LeftBack, LeftBackT);
        UpdateWheelPosition(RightBack, RightBackT);


    }

    private void UpdateWheelPosition(WheelCollider collider, Transform transform)
    {
        Vector3 position = transform.position;
        Quaternion quaternion = transform.rotation;

        // pozice ve svete
        collider.GetWorldPose(out position, out quaternion);

        transform.position = position;
        transform.rotation = quaternion;

    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPositions();
    }


}

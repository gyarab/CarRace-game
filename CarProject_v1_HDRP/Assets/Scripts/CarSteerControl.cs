using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSteerControl : MonoBehaviour
{
    public Wheel[] wheels;

    [Header("Car Specs")]
    public float wheelBase; //in meters
    public float rearTrack; //in meters
    public float turnRadius; //in meters

    [Header("Inputs")]
    public float steerInput;
    public float angleForLeftWh;
    public float angleForRightWh;
 

    
    void Update()
    {
        steerInput = Input.GetAxis("Horizontal");

        if (steerInput > 0 ){    //otoceni do prava
        angleForLeftWh = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack/2)))* steerInput;
        angleForRightWh = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack/2)))* steerInput;  
        }else if (steerInput < 0){  //otoceni do leva
        angleForLeftWh = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius - (rearTrack/2)))* steerInput;
        angleForRightWh = Mathf.Rad2Deg * Mathf.Atan(wheelBase / (turnRadius + (rearTrack/2)))* steerInput;

        }else{                      //rovne
        angleForLeftWh = 0;
        angleForRightWh = 0;
        }

        foreach (Wheel w in wheels){
            if(w.WheelFrontLeft)
               w.steerAngle = angleForLeftWh; 
             //  Debug.Log(w.steerAngle);
            
            
            if(w.WheelFrontRight)
               w.steerAngle = angleForRightWh; 
            


        }

    }
        
}

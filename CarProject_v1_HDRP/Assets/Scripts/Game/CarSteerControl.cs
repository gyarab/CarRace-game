using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSteerControl : MonoBehaviour
{
     
     
    [Header("Select Car")] 
    public bool Porsche_911_GT2;
    public bool Skoda_Superb_2018;
    public bool Chevrolet_Chevelle_SS_1966;
    public bool Opel_Vectra_2002;


    public Wheel[] wheels;
    [Header("Car Specs")]
    public float wheelBase; //in meters
    public float rearTrack; //in meters
    public float turnRadius; //in meters

    [Header("Inputs")]
    public float steerInput;
    public float angleForLeftWh;
    public float angleForRightWh;
 

    public AnimationCurve SpeedCurve;
    public AnimationCurve InverseSpeedCurve;

    public float timer2;
    public static float CurveEvaluation = 0;
    [Header("Odometer")]   
    public GameObject pointer;
    private float startPosition = 225;
    private float endPosition = -47;
    private float positionX;
    
    [Header("BackLights")]  
   public GameObject BackLights;
   
    void Start() {

 if(Porsche_911_GT2 == true){
SpeedCurve.AddKey(0,0);
SpeedCurve.AddKey(14,50);
SpeedCurve.AddKey(36,100);
SpeedCurve.AddKey(52,130);
SpeedCurve.AddKey(109,200);
SpeedCurve.AddKey(187,250);
SpeedCurve.AddKey(330,300);
SpeedCurve.AddKey(1000,320);
//---------------------------------------------//
InverseSpeedCurve.AddKey(0,0);
InverseSpeedCurve.AddKey(50,14);
InverseSpeedCurve.AddKey(100,36);
InverseSpeedCurve.AddKey(130,52);
InverseSpeedCurve.AddKey(200,109);
InverseSpeedCurve.AddKey(250,187);
InverseSpeedCurve.AddKey(300,330);
//InverseSpeedCurve.AddKey(500,330);
//InverseSpeedCurve.AddKey(320,1000);
//----------------------------------------------/
} 
 if(Skoda_Superb_2018 == true){
SpeedCurve.AddKey(0,0);
SpeedCurve.AddKey(33,50);
SpeedCurve.AddKey(77,100);
SpeedCurve.AddKey(114,130);
SpeedCurve.AddKey(300,200);
SpeedCurve.AddKey(1000,190);
//---------------------------------------------//
InverseSpeedCurve.AddKey(0,0);
InverseSpeedCurve.AddKey(50,33);
InverseSpeedCurve.AddKey(100,77);
InverseSpeedCurve.AddKey(130,114);
InverseSpeedCurve.AddKey(200,300);
//InverseSpeedCurve.AddKey(500,330);
//InverseSpeedCurve.AddKey(320,1000);
//----------------------------------------------/
} 
 if(Chevrolet_Chevelle_SS_1966 == true){
SpeedCurve.AddKey(0,0);
SpeedCurve.AddKey(25,50);
SpeedCurve.AddKey(65,100);
SpeedCurve.AddKey(90,130);
SpeedCurve.AddKey(250,200);
SpeedCurve.AddKey(300,260);
SpeedCurve.AddKey(1000,240);
//---------------------------------------------//
InverseSpeedCurve.AddKey(0,0);
InverseSpeedCurve.AddKey(50,25);
InverseSpeedCurve.AddKey(100,65);
InverseSpeedCurve.AddKey(130,90);
InverseSpeedCurve.AddKey(200,250);
InverseSpeedCurve.AddKey(260,300);
//InverseSpeedCurve.AddKey(500,330);
//InverseSpeedCurve.AddKey(320,1000);
//----------------------------------------------/
} 
 if(Opel_Vectra_2002 == true){
SpeedCurve.AddKey(0,0);
SpeedCurve.AddKey(25,50);
SpeedCurve.AddKey(65,100);
SpeedCurve.AddKey(90,130);
SpeedCurve.AddKey(250,200);
SpeedCurve.AddKey(300,260);
SpeedCurve.AddKey(1000,240);
//---------------------------------------------//
InverseSpeedCurve.AddKey(0,0);
InverseSpeedCurve.AddKey(50,25);
InverseSpeedCurve.AddKey(100,65);
InverseSpeedCurve.AddKey(130,90);
InverseSpeedCurve.AddKey(200,250);
InverseSpeedCurve.AddKey(260,300);
//InverseSpeedCurve.AddKey(500,330);
//InverseSpeedCurve.AddKey(320,1000);
//----------------------------------------------/
} 

    }
    void FixedUpdate() {
       updatePointer(); 
       
    }
    void Update()
    {
     AccelerationCruveEval();
     lightController();

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
     void AccelerationCruveEval()
     {
        if (Input.GetKey("w"))
        {
             timer2 += Time.deltaTime;
        //Debug.Log("currenTimePressedButton" + timer2); 
         CurveEvaluation = this.SpeedCurve.Evaluate((timer2*10));
      
        }else{
        Debug.Log("IMPORTANTE!!!!!!!!! = " + (float)Wheel.Speed);      
        timer2 = InverseSpeedCurve.Evaluate((float)Wheel.Speed)*0.1f;            
        }


     }
      
      //updateju rucicku tachometru
      public void updatePointer()
      {
        positionX = startPosition - endPosition;
        float temp = this.SpeedCurve.Evaluate((timer2*10))/360;//(float)Wheel.Speed / 360; 
        //float xx = this.SpeedCurve.Evaluate((timer2*10));
        pointer.transform.eulerAngles = new Vector3(0,0,(startPosition - temp * positionX));
      //  pointer.transform.eulerAngles = new Vector3(0,0,(startPosition - xx));
    }

      public void lightController(){
          if (Input.GetKey("s"))
         {
               BackLights.SetActive(true);
         } else{
                BackLights.SetActive(false);
         }

      }




}

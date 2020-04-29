using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{

    //rozdelení jednotlivých kol
    [Header("WHEELS")]
    public bool WheelFrontLeft;
    public bool WheelFrontRight;
    public bool WheelBackLeft;
    public bool WheelBackRight;

    // odometer
    private float TotalDistance;
    public static double Speed = 0;
    private Vector3 startingPosition, speedvector;

    //



    public float steerAngle;
    public float SteerTime = 8;

    private Rigidbody rg;


    [Header("Suspention")]
    public float restLenght = 0.5f; //nestlacena pruzina 0.5f
    public float springTravel = 0.2f; //jak moc se muze pruzina pohybovat 
    public float springStiffness = 30000;
    public float damperStiffness = 4000;

    private float minLenght; //pruziny
    private float maxLenght; //pruziny
    private float springLenght; //aktualni delka pruziny
    private float springForce; //sila pruziny vektor*up ktera talci auto nahoru
    private float motorForce = 6000; //6000 

    private float lastLenght; //posledni vypocet minuleho framu
    private float damperForce; // sila tlumice 
    private float springVelocity; //rychlost pruziny 

    private float ForceX;
    private float ForceY;


    private Vector3 wheelVelocityLS;


    private Vector3 suspentionForce;
    private float wheelAngle; //plinuly prechod pri zataceni

    [Header("WheelSetup")]
    public float wheelRadius = 0.33f; //sirka kola 0.33f

    //slipCalc Pacejka
    private float slipAngle; // in degrees (for lateral force)
    private float slipRatio;
    private float B = 10;    // Stiffness | Dry tarmac
    private float C1 = 1.65f;  // Shape   | Longitudinal 
    private float C2 = 1.3f;  // Shape    | Lateral 
    private float D = 1;     // Peak      | Dry tarmac
    private float E = 0.97f; // Curvature | Dry tarmac
    private float Fz = 4000f; // nahodna hodnota
    private float lateralForce;

    // Slip ratio
    private float forwardVelocity;


    //výkonová křivka
    [Header("Curve")]

    float FltSpeed;
    public float startTime;
    public float timer;
    public float maxSpeed;

    //start motoru po odpocitani
    
    void Start()
    {

        startingPosition = transform.position;
        rg = transform.root.GetComponent<Rigidbody>(); // neni stejny objekt musi se vyhledat // root je nadrazeny objekt v hierarchii

        minLenght = restLenght - springTravel;
        maxLenght = restLenght + springTravel;
    }
    void Update()
    {
        timer += Time.deltaTime;
        //  Debug.Log("currenTime" + timer);                             //--------------------------------------------------------//

        wheelAngle = Mathf.Lerp(wheelAngle, steerAngle, SteerTime * Time.deltaTime); //plinule zataceni
        transform.localRotation = Quaternion.Euler(Vector3.up * wheelAngle);

        Debug.DrawRay(transform.position, -transform.up * (springLenght + wheelRadius), Color.red);

    }

    void FixedUpdate() //pouzivase pri praci s fyzikou / frame
    {

        //   maxSpeed = gameObject.GetComponent<CarSteerControl>().CurveEvaluation;
        maxSpeed = CarSteerControl.CurveEvaluation;
        GetTimeButtonPressed();
        GetCarVelocity();
        odometer();
        slipAngleCalc();
        LateralForceCalc();


        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLenght + wheelRadius))
        {
            lastLenght = springLenght;


            springLenght = hit.distance - wheelRadius;
            springLenght = Mathf.Clamp(springLenght, minLenght, maxLenght); //overeni aby pruzina nebyla delsi/kratsi nez max a min vzdalenost
            springVelocity = (lastLenght - springLenght) / Time.fixedDeltaTime; //prumer vypoctu fyziky [velocity(rychlost) = vzdalenost za cas]
            springForce = springStiffness * (restLenght - springLenght);
            damperForce = damperStiffness * springVelocity;
            //   Debug.Log(springForce);

            // wheelVelocityLS = rg.GetRelativePointVelocity(hit.point);
            wheelVelocityLS = transform.InverseTransformDirection(rg.GetPointVelocity(hit.point));



            //  Debug.Log("veluef from speedCruve" + speedCurve);
            if(GameController.startEngine == true){
            ForceX = Input.GetAxis("Vertical") * -motorForce;
            }
           

            //    Debug.Log(Input.GetAxis("Vertical"));

            FltSpeed = (float)Speed;
            //float ConvertedSpeed = FltSpeed;
            // Debug.Log(FltSpeed);
            ForceY = wheelVelocityLS.x * 1500; // (motorForce * (Speed * 0.1f)

            if (maxSpeed <= Speed)
            {
                if (ForceX < 0)
                    ForceX = 0;
            }


            suspentionForce = (springForce + damperForce) * transform.up;


            rg.AddForceAtPosition(suspentionForce + (ForceX * transform.forward) + (/*lateralForce*/ForceY * -transform.right), hit.point);

            //     Debug.Log(suspentionForce + (ForceX * transform.forward) + (ForceY * -transform.right));    


        }


    }

    void odometer()
    {
        // speed
        speedvector = ((transform.position - startingPosition) / Time.deltaTime);
        Speed = (int)(speedvector.magnitude) * 3.6;
        /*
        if(ForceX > 0){

            Speed = Speed*-1;
        }
        */
        startingPosition = transform.position;
        Debug.Log("speed  " + Speed);

        // total distance
        TotalDistance += rg.velocity.magnitude * Time.deltaTime;
        // Debug.Log("distanceTraveled  " + TotalDistance);                      //--------------------------------------------------------//
    }


    void slipAngleCalc()
    {

        var groundVelocity = wheelVelocityLS;
        //        Debug.Log(groundVelocity);                                         //--------------------------------------------------------//
        float denom = Mathf.Max(Mathf.Abs(groundVelocity.z), 3.0f);
        slipAngle = Mathf.Atan(groundVelocity.x / denom) * Mathf.Rad2Deg;
        //     Debug.Log("slipAngle" + slipAngle + "°");


    }

    void slipRatioCalc()
    {
        float V = forwardVelocity; // V is the forward velocity of the vehicle.
        float Rc = 0.33f; //effective radius of corresponding free-roling tire
        float angularVelocity = (FltSpeed);
        slipRatio = ((angularVelocity * Rc) / V - 1) * 100;



    }


    void LateralForceCalc()
    {

        // F = Fz · D · sin(C · arctan(B·slip – E · (B·slip – arctan(B·slip))))

        float F = Fz * D * Mathf.Sin(C2 * Mathf.Atan(B * slipAngle - E * (B * slipAngle - Mathf.Atan(B * slipAngle))));
        lateralForce = F;
        //  Debug.Log("LateralForce " + lateralForce);
    }

    void longitudinalForce()
    {

        float F = Fz * D * Mathf.Sin(C1 * Mathf.Atan(B * slipRatio - E * (B * slipRatio - Mathf.Atan(B * slipRatio))));

    }

    void GetCarVelocity()
    {
        Vector3 carVelocity = rg.velocity; // momentali rychlost pusobici na auto       
                                           //  Debug.Log("carVelocity " + carVelocity);                             //--------------------------------------------------------//

        forwardVelocity = carVelocity.z;

    }

    void GetTimeButtonPressed()
    {
        if (Input.GetKeyDown("w"))
        {
            startTime = Time.time;
        }
        if (Input.GetKey("w") && Time.time - startTime < 1f)
        {
            //       Debug.Log((Time.time - startTime).ToString("00:00.00") + "time pressed");      //--------------------------------------------------------//
            float lastTime = (Time.time - startTime);
        }

    }




}

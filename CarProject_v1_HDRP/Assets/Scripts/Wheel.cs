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


public float steerAngle;
public float SteerTime = 8;

private Rigidbody rg;


[Header("Suspention")] 
public float restLenght = 0.5f; //nestlacena pruzina 
public float springTravel = 0.2f; //jak moc se muze pruzina pohybovat 
public float springStiffness = 30000;
public float damperStiffness = 4000;

private float minLenght; //pruziny
private float maxLenght; //pruziny
private float springLenght; //aktualni delka pruziny
private float springForce; //sila pruziny vektor*up ktera talci auto nahoru

private float lastLenght; //posledni vypocet minuleho framu
private float damperForce; // sila tlumice 
private float springVelocity; //rychlost pruziny 




private Vector3 suspentionForce;
private float wheelAngle; //plinuly prechod pri zataceni

[Header("WheelSetup")]
public float wheelRadius = 0.33f; //sirka kola



    void Start()
    {
        rg = transform.root.GetComponent<Rigidbody>(); // neni stejny objekt musi se vyhledat // root je nadrazeny objekt v hierarchii

        minLenght = restLenght - springTravel;
        maxLenght = restLenght + springTravel;
    }
    void Update()
    {
       wheelAngle = Mathf.Lerp(wheelAngle, steerAngle, SteerTime * Time.deltaTime); //plinule zataceni
        transform.localRotation = Quaternion.Euler(Vector3.up * wheelAngle);



      
    }

    void FixedUpdate() //pouzivase pri praci s fyzikou / frame
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLenght + wheelRadius)){
                lastLenght = springLenght;
           
               
                springLenght = hit.distance - wheelRadius; 
                springLenght = Mathf.Clamp(springLenght, minLenght, maxLenght); //overeni aby pruzina nebyla delsi/kratsi nez max a min vzdalenost
                springVelocity = (lastLenght - springLenght) / Time.fixedDeltaTime; //prumer vypoctu fyziky [velocity(rychlost) = vzdalenost za cas]
                springForce = springStiffness * (restLenght - springLenght);
                damperForce = damperStiffness * springVelocity;


                suspentionForce = (springForce + damperForce) * transform.up;

                rg.AddForceAtPosition(suspentionForce, hit.point);


        }

      
      
    }
}

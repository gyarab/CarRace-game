using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CarEngineSound : MonoBehaviour
{
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;
    public AudioClip clip6;
    AudioSource audioS;
    //public GameObject Car;
  
    [SerializeField] float modifier;

    void Start()
    {
    audioS = GetComponent<AudioSource>();
   
    }

    // Update is called once per frame
    void Update()
    {
        float soundDiff = 1;
        float soundPitch;

        if((float)Wheel.Speed > 20){
            soundPitch = 1.5f;
        }
          if((float)Wheel.Speed > 40){
            soundPitch = 1.8f;
        }
          if((float)Wheel.Speed > 70){
            soundPitch = 2f;
        }  
        if((float)Wheel.Speed > 100){
            soundPitch = 2.5f;
        }
         float xPos = (float)Wheel.Speed *3 / 100 + 0.3f;
        audioS.pitch = xPos;
        
        
    }
}

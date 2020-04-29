using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour
{
  
  
   public bool checkpointReached = false;

    private void OnTriggerEnter(Collider col) {
        
        if (col.gameObject.name == "FinishObject"){
            if(checkpointReached  == true){
                GameController.finished = true;
                
                
             
             //   Time.timeScale = 0f;
                //scena zmena (raceAgain + exit)
                //timer stop 
                //map reset
               
            }
        }

        if (col.gameObject.name == "CheckpointObject"){
            checkpointReached  = true;
        }

  }



}

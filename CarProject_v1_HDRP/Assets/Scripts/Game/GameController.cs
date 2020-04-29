using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
   public int CoundDownNumber; 
   public Text CoundDownDisplay;
   public Text RaceTimeDisplay;
   public Text EndGameTime;
   public GameObject EndgameCanvas;
   private float StartTime;
   public static bool startEngine = false;
   public static bool finished = false;
 
   private void Start() {
      
       StartCoroutine(CounterToStart());
    
   }

   IEnumerator CounterToStart()
   {
       while(CoundDownNumber > 0)
       {
           CoundDownDisplay.text = CoundDownNumber.ToString();

            yield return new WaitForSeconds(1f);
       
            CoundDownNumber--;
       }
      
    CoundDownDisplay.text = "GO!";  
    startEngine = true;
    StartTime = Time.time;

    //begin game;


    yield return new WaitForSeconds(1f);
    CoundDownDisplay.gameObject.SetActive(false);

   }
   public void ResetGame()
   {
      finished = false;
   EndgameCanvas.SetActive(false);
   SceneManager.LoadScene("Game");
   startEngine = false;
   }
   public void BackToMenu()
   {
      EndgameCanvas.SetActive(false);
   SceneManager.LoadScene("Menu");  
    startEngine = false;
     finished = false;
   }

   void Update()
   {
      if (finished)
      {
         startEngine = false; 
         EndgameCanvas.SetActive(true);
          return; 
      }else{
      float t = Time.time - StartTime;
  
      string minutes = ((int) t/60).ToString();
      string seconds = (t % 60).ToString("f1");
      if (startEngine == true){
      RaceTimeDisplay.text = "0" + minutes + ":" + seconds;
      EndGameTime.text =  minutes + ":" + seconds;
      }
 
      }
   }
     
}

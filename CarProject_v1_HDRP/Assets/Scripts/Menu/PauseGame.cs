using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseGame : MonoBehaviour
{
   
    
    
    public GameObject PauseMenuUI;
    public GameObject ENDMenuUI;
    public static bool PausedGame = false;
    //public GameObject MainCamera1;
    //public GameObject EndGameCamera1;


    int Cameras;

    private void Start()
    { 

    }

    void Update()
    //if dojel si do cile .......
    {
      /*
        if (hpNumber <= 0)
        {
            
            SwitchOnEndGameCamera();
            ENDMenuUI.SetActive(true);        
            FindObjectOfType<AudioManager>().PlaySound("endGame");

        }
         
*/


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            if (PausedGame)
            {
                ResumeGame();
            //    GameObject.FindGameObjectWithTag("PauseMenu").SetActive(true);
               
            }
            else
            {
               PausingGame();
          //      GameObject.FindGameObjectWithTag("PauseMenu").SetActive(false);
            }
        }
    }
    
      public void PausingGame()
        {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;     
        PausedGame = true;


      //  GameObject camerabox = GameObject.Find("CamerBox");
    //    CameraMovement CameraMovement = camerabox.GetComponent<CameraMovement>();
     //   CameraMovement.CameraSpeed = 0f;


    }

    
    public void ResumeGame() {
        //FindObjectOfType<AudioManager>().PlaySound("ButtonClick");
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        PausedGame = false;

    

    }

    public void LoadMenu()
    {

    }
    public void QuitInGame()
    {
        Debug.Log("jdu do menu");
        SceneManager.LoadScene(0);

    }

    public void TurnOffGame()
    {
        // loadne dalsi level
        Debug.Log("quit");
        Application.Quit();


    }
    /*
    public void SwitchOnMainCamera()
    {
        EndGameCamera1.SetActive(false);
        MainCamera1.SetActive(true);
    }
    */
    /*
    public void SwitchOnEndGameCamera()
    {
        MainCamera1.SetActive(false);
        EndGameCamera1.SetActive(true);
    }

    */

}

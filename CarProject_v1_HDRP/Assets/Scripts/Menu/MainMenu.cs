using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour

{
    public static int mapNumber;
    public static int carNumber;
    public static bool yesNo = false;
    bool x = true;
    
    
    public void NewGameStart()
    {
        // loadne dalsi level

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;      
    }


    public void QuitGameNow()
{
    // loadne dalsi level
    Debug.Log("quit");
    Application.Quit();
}

    public void MapNum1()
    {
    mapNumber = 0;
    Debug.Log("0");
    }

     public void MapNum2()
    {
    mapNumber = 1;
    Debug.Log("1");
    }

     public void MapNum3()
    {
    mapNumber = 2;
    Debug.Log("2");
    }

     public void MapNum4()
    {
    mapNumber = 3;
    Debug.Log("3");
    }

        public void CarNum1()
    {
    carNumber = 0;
  //  yesNo = true;
    Debug.Log("0");
    }

     public void CarNum2()
    {
    carNumber = 1;
    Debug.Log("1");
 //   yesNo = true;
    }

     public void CarNum3()
    {
    carNumber = 2;
 //   yesNo = true;
    }
    
     public void CarNum4()
    {
    carNumber = 3;
 //   yesNo = true;
    }

}


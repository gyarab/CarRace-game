using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCarChoose : MonoBehaviour
{
    
    
    [Header("MAP")]
    public GameObject[] maps;
     int getMapNum;

    [Header("Car Type")]
    public GameObject[] CarType;
    public static int getCarNum;

    [Header("Car Start Position")]
    public GameObject[] carStartPos;
    int ahoj = 2;
    
    void Start() {
       // if (SceneManager.GetActiveScene().buildIndex + ahoj == 3){
        ChooseMap();
        ChooseCar();
        SetCarPos();
      //  ahoj = 5;
  //      MainMenu.yesNo = false;
    //    }
  //      }
     
    }

    
    void ChooseMap()
    {
        getMapNum = MainMenu.mapNumber;
        Debug.Log(getMapNum + "mapNum");
        
        maps[getMapNum].SetActive(true);
   

    }

    void ChooseCar()
    {
         getCarNum = MainMenu.carNumber;
        Debug.Log(getCarNum + "carNum");
        CarType[getCarNum].SetActive(true);
   

    }
    void SetCarPos()
    {
         CarType[getCarNum].transform.SetPositionAndRotation(carStartPos[getMapNum].transform.position, carStartPos[getMapNum].transform.rotation);
    }
}

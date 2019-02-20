
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

//    public GameObject DataPort;
//    DataManagerScript GetData;

    public static GameManagerScript instance = null;

    public delegate void BoolEventManager();

    public int money;

    private void Awake()
    {
//        GetData = DataPort.GetComponent<DataManagerScript>();



        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }


    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrewManager : MonoBehaviour {

    public GameObject DataHub;
    private DataManagerScript GetData;

    public GameObject CrewUIObject;
    public GameObject UIParent;

    public float SpawnOffset;
    Vector3 internalOffset = new Vector3(0,0,0);

    Vector3 Spawn;

    


    
	// Use this for initialization
	void Start () {
        GetData = DataHub.GetComponent<DataManagerScript>();
        Spawn = new Vector3(UIParent.transform.position.x + SpawnOffset, UIParent.transform.position.y, UIParent.transform.position.z);

        DataManagerScript.dataCreated += GenerateCrew;
    }

    public void GenerateCrew() {
        for (int i = 1; i <= GetData.CrewMaxCapacity; i++)
        {
            Instantiate(CrewUIObject, Spawn+internalOffset, Quaternion.identity);
            internalOffset.x += -20;
        }
    }
}

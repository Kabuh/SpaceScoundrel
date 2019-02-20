using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetsBehaviour : MonoBehaviour {

    public Text planetStats;
    public string planetName;
    public float smuggleDifficulty;
    

    public float onMouseOverChange;

    Vector3 change;
    bool zoomed = false;

    private DataManagerScript DataClass;
    public GameObject GetData;

    public void Start()
    {
        DataClass = GetData.GetComponent<DataManagerScript>();
        change = new Vector3(onMouseOverChange, onMouseOverChange, 0);
        planetStats.text += "Name: " + planetName + "\n" + "CustomsLVL: " + smuggleDifficulty + "\n";
        planetStats.transform.parent.gameObject.SetActive(false);
    }

    private void OnMouseOver()
    {
        if (!zoomed) {
            transform.localScale += change;
            zoomed = true;
            planetStats.transform.parent.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (DataClass.NotMoving == true) {
                DataClass.ShipDestination = this.transform.position;
                if (DataClass.MarkerInstalled) {
                    DestroyMarker();
                }
                InstallMarker();
            }
            
        }
    }

    private void OnMouseExit()
    {
        if (zoomed)
        {
            transform.localScale -= change;
            zoomed = false;
            planetStats.transform.parent.gameObject.SetActive(false);
        }
    }

    void InstallMarker() {
        GameObject MarkerClone;
        MarkerClone = Instantiate(DataClass.Marker, transform.position, transform.rotation);
        MarkerClone.transform.position += new Vector3(0, DataClass.MarkerOffset, 0);
        DataClass.MarkerInstalled = true;
        DataClass.MarkerCurrent = MarkerClone;
    }

    void DestroyMarker() {
        Destroy(DataClass.MarkerCurrent);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DockedButtonFucntions : MonoBehaviour
{

    public GameObject DataHub;
    private DataManagerScript GetData;
    public GameObject[] ButtonList;
    public enum ButtonListHandler {PickUp, Drop, Refuel};

    public GameObject lootOrigin;


    void Start()
    {
        GetData = DataHub.GetComponent<DataManagerScript>();

        //subscribing listeners
        ShipMovement.OnDockEnter += RunButtonCheck;
        ShipMovement.OnDockLeave += RunButtonDisable;

        RunButtonDisable();
    }

    //listeners

    void RunButtonDisable()
    {
        foreach (GameObject button in ButtonList)
        {
            DisableButton(button);
        }
    }

    void RunButtonCheck()
    {
        if (GetData.CargoSpaceTaken == GetData.CargoSpace)
        {
            DisableButton(ButtonList[(int)ButtonListHandler.PickUp]);
        }
        else
        {
            EnableButton(ButtonList[(int)ButtonListHandler.PickUp]);
        }

        if (lootOrigin == GetData.LandedOn || GetData.CargoSpaceTaken == 0)
        {
            DisableButton(ButtonList[(int)ButtonListHandler.Drop]);
        }
        else
        {
            EnableButton(ButtonList[(int)ButtonListHandler.Drop]);
        }

        if (GetData.FuelCurrent == GetData.FuelMax || GetData.Money == 0)
        {
            DisableButton(ButtonList[(int)ButtonListHandler.Refuel]);
        }
        else
        {
            EnableButton(ButtonList[(int)ButtonListHandler.Refuel]);
        }
    }

    //what buttons do
    public void GetShit()
    {
        GetData.CargoSpaceTaken = GetData.CargoSpace;
        lootOrigin = GetData.LandedOn;
        DisableButton(ButtonList[(int)ButtonListHandler.PickUp]);
    }

    public void UnloadShit()
    {
        GetData.Money += GetData.CargoSpaceTaken;
        GetData.CargoSpaceTaken = 0;
        TurnOFFtoON(ButtonList[(int)ButtonListHandler.Refuel]);
        TurnOFFtoON(ButtonList[(int)ButtonListHandler.PickUp]);
        DisableButton(ButtonList[(int)ButtonListHandler.Drop]);    
    }

    public void GetFuel()
    {
        if (GetData.FuelMax - GetData.FuelCurrent < GetData.Money)
        {
            GetData.Money += GetData.FuelCurrent - GetData.FuelMax;
            GetData.FuelCurrent = GetData.FuelMax;
        }
        else {
            GetData.FuelCurrent += GetData.Money;
            GetData.Money = 0;
        }
        TurnONtoOFF(ButtonList[(int)ButtonListHandler.Refuel]);
    }

    //button power switch
    void EnableButton(GameObject x) {
        x.GetComponent<Button>().interactable = true;
    }

    void DisableButton(GameObject x) {
        x.GetComponent<Button>().interactable = false;
    }

    void TurnOFFtoON(GameObject x) {
        if (x.GetComponent<Button>().interactable == false)
        {
            x.GetComponent<Button>().interactable = true;
        }
    }

    void TurnONtoOFF(GameObject x) {
        if (x.GetComponent<Button>().interactable == true)
        {
            x.GetComponent<Button>().interactable = false;
        }
    }
}



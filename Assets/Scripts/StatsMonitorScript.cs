using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsMonitorScript : MonoBehaviour {

    private ChaikaStatData currentScript;

    public Text ShipStats;

    public int TotalHP {
        get { return currentScript.totalHP; }
        set { currentScript.totalHP = value; } }
    public int CurrentHP {
        get { return currentScript.currentHP ; }
        set { currentScript.currentHP = value; } }
    public string ShipClassName {
        get { return currentScript.shipClassName ; }
        set { currentScript.shipClassName = value; } }
    public int CrewMaxCapacity {
        get { return currentScript.crewMaxCapacity ; }
        set { currentScript.crewMaxCapacity = value; } }
    public int CrewCurrentCapacity {
        get { return currentScript.crewCurrentCapacity ; }
        set { currentScript.crewCurrentCapacity = value; } }
    public int CargoSpace {
        get { return currentScript.cargoSpace ; }
        set { currentScript.cargoSpace = value; } }
    public int CargoSpaceTaken {
        get { return currentScript.cargoSpaceTaken ; }
        set { currentScript.cargoSpaceTaken = value; } }
    public int BootlegSpace {
        get { return currentScript.bootlegSpace ; }
        set { currentScript.bootlegSpace = value; } }
    public int BootlegSpaceTaken {
        get { return currentScript.bootlegSpaceTaken ; }
        set { currentScript.bootlegSpaceTaken = value; } }
    public int FuelMax {
        get { return currentScript.fuelMax ; }
        set { currentScript.fuelMax = value; } }
    public int FuelCurrent {
        get { return currentScript.fuelCurrent ; }
        set { currentScript.fuelCurrent = value; } }
    public float ShipSmuggleStat {
        get { return currentScript.shipSmuggleStat ; }
        set { currentScript.shipSmuggleStat = value; } }

    


    // Use this for initialization
    void Start () {

        currentScript = this.gameObject.GetComponent<ChaikaStatData>();

    }
	
	// Update is called once per frame
	void Update () {
        ShipStats.text = "Ship Stats:" + "\n"
            + "+HP: " + TotalHP + "/" + CurrentHP + "\n"
            + "+Class: " + ShipClassName + "\n"
            + "+Crew: " + CrewMaxCapacity + "/" + CrewCurrentCapacity + "\n"
            + "+CargoSpace: " + CargoSpace + "/" + CargoSpaceTaken + "\n"
            + "+SmuggleSpace: " + BootlegSpace + "/" + BootlegSpaceTaken + "\n"
            + "+SmuggleStat: " + ShipSmuggleStat + "\n"
            + "+Fuel: " + FuelMax + "/" + FuelCurrent + "\n";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManagerScript : MonoBehaviour {

    public delegate void EventHolder();
    public static event EventHolder dataCreated;

    public static DataManagerScript instance = null;

    public GameObject ShipGO;
    private ShipMovement shipMovementScript;
    private StatsMonitorScript shipStatsScript;
    public GameObject GeneralGameData;
    private GameManagerScript gameScript;
    public Text PlayerResourses;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
    }

    void Start()
    {
        shipMovementScript = ShipGO.GetComponent<ShipMovement>();
        shipStatsScript = ShipGO.GetComponent<StatsMonitorScript>();
        gameScript = GeneralGameData.GetComponent<GameManagerScript>();

        Money = 0;

        dataCreated();
    }

    //shipMovement
    public bool NotMoving     {
        get{
            return shipMovementScript.notMoving;}
        set{
            shipMovementScript.notMoving = value; } }
    public Vector2 ShipDestination {
        get {
            return shipMovementScript.ShipDestination; }
        set {
            shipMovementScript.ShipDestination = value; } }

    //ship location
    public GameObject LandedOn {
        get { return shipMovementScript.chosenPlanet; }
        set { shipMovementScript.chosenPlanet = value; }
    }


    //shipStats
    public int TotalHP {
        get { return shipStatsScript.TotalHP; }
        set { shipStatsScript.TotalHP = value; } }
    public int CurrentHP {
        get { return shipStatsScript.CurrentHP; }
        set { shipStatsScript.CurrentHP = value; } }
    public string ShipClassName {
        get { return shipStatsScript.ShipClassName; }
        set { shipStatsScript.ShipClassName = value; } }
    public int CrewMaxCapacity {
        get { return shipStatsScript.CrewMaxCapacity; }
        set { shipStatsScript.CrewMaxCapacity = value; } }
    public int CrewCurrentCapacity {
        get { return shipStatsScript.CrewCurrentCapacity; }
        set { shipStatsScript.CrewCurrentCapacity = value; } }
    public int CargoSpace {
        get { return shipStatsScript.CargoSpace; }
        set { shipStatsScript.CargoSpace = value; } }
    public int CargoSpaceTaken {
        get { return shipStatsScript.CargoSpaceTaken; }
        set { shipStatsScript.CargoSpaceTaken = value; } }
    public int BootlegSpace {
        get { return shipStatsScript.BootlegSpace; }
        set { shipStatsScript.BootlegSpace = value; } }
    public int BootlegSpaceTaken {
        get { return shipStatsScript.BootlegSpaceTaken; }
        set { shipStatsScript.BootlegSpaceTaken = value; } }
    public int FuelMax {
        get { return shipStatsScript.FuelMax; }
        set { shipStatsScript.FuelMax = value; } }
    public int FuelCurrent {
        get { return shipStatsScript.FuelCurrent; }
        set { shipStatsScript.FuelCurrent = value; } }
    public float ShipSmuggleStat {
        get { return shipStatsScript.ShipSmuggleStat; }
        set { shipStatsScript.ShipSmuggleStat = value; } }

    //marker
    public GameObject Marker;
    public float MarkerOffset;
    public GameObject MarkerCurrent { get; set; }
    public bool MarkerInstalled { get; set; }

    //cash
    public int Money {
        get { return gameScript.money; }
        set {
            gameScript.money = value;
            PlayerResourses.text = "$" + value;
        }
    }
}

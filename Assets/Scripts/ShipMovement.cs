using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShipMovement : MonoBehaviour
{
    //data exchange
    public GameObject chosenPlanet;
    private PlanetsBehaviour planetStats;
    public GameObject DataHub;
    private DataManagerScript GetData;

    //ship Vectors
    public Vector2 ShipDestination;
    public Vector3 ShipCurrentLocation;

    //ship UI
    public Text ShipLog;
    public GameObject LaunchButton;

    //ship stats
    public float speed;
    float timePassed;
    float smuggleSuccessThreshold;

    //ship states
    public bool notMoving = true;   //in full-stop state anywhere
    bool docked = false;            //in full-stop state AND on the planet
    bool movementAllowed = false;   //trigger for ship movement function
    
    bool overThePlanet = false;
    

    //pulseMaker
    public float timeCumulative = 0;
    public float pulse = 0;
    public float pulseIncrement;
    public int systemRandomEncounterChance;

    //event
    public delegate void EventHolder();
    public static event EventHolder OnDockEnter;
    public static event EventHolder OnDockLeave;
    public static event EventHolder EventPopup;

    void Start()
    {
        ShipDestination = this.transform.position;
        pulse = pulseIncrement;
        GetData = DataHub.GetComponent<DataManagerScript>();
    }

    void Update()
    {
        ShipCurrentLocation = this.transform.position;
        timePassed = Time.deltaTime * speed;

        
        //getting docked
        if (SameLocationChecker(ref ShipCurrentLocation, ref ShipDestination) && !docked && overThePlanet)
        {
            docked = true; 
            notMoving = true;
            movementAllowed = false;
            pulse = pulseIncrement;
            timeCumulative = 0;    
            if (GetData.MarkerInstalled) {
                Destroy(GetData.MarkerCurrent);
            }
            OnDockEnter();
        }

        //destination set
        if (!SameLocationChecker(ref ShipCurrentLocation, ref ShipDestination) && docked)
        {
            LaunchButton.GetComponent<Button>().interactable = true;
        }

        //flight
        if (movementAllowed)
        {
            Movement();
            timeCumulative += Time.deltaTime;
            if (timeCumulative > pulse)
            {
                pulse += pulseIncrement;
                if (GetData.FuelCurrent > 0) {
                    GetData.FuelCurrent--;
                    if (Mathf.FloorToInt(Random.value * 100) <= systemRandomEncounterChance)
                    {
                        EventPopup();
                    }
                }
            }
        }
    }

    public bool SameLocationChecker(ref Vector3 a, ref Vector2 b)
    {
        if (a.x == b.x && a.y == b.y) { return true; } else { return false; }
    }

    public void LaunchButtonEvent()
    {

        if (ShipDestination.x != this.transform.position.x && ShipDestination.y != this.transform.position.y)
        {
            movementAllowed = true;
            docked = false;
            OnDockLeave();
            notMoving = false;
            RotateAtDestination(ref ShipDestination);
            ShipLog.text += "- departing" + "\n";
            LaunchButton.GetComponent<Button>().interactable = false;
        }
        else {
            ShipLog.text += "- please choose planetary destination" + "\n";
        }
       
    }

    void Movement()
    {
        this.transform.position = Vector2.MoveTowards(ShipCurrentLocation, ShipDestination, timePassed);  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        overThePlanet = true;
        chosenPlanet = collision.gameObject;
        ShipLog.text += "- orbiting a planet " + chosenPlanet.name + "\n";
        planetStats = chosenPlanet.GetComponent<PlanetsBehaviour>();

        smuggleSuccessThreshold = 1 - Mathf.Pow(planetStats.smuggleDifficulty + 2, -1 * GetData.ShipSmuggleStat) * Mathf.Pow(planetStats.smuggleDifficulty, GetData.ShipSmuggleStat);
        ShipLog.text += "- if you carry a bootleg you need " + smuggleSuccessThreshold * 100 + "% throw to pass customs succesfully" + "\n";
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        overThePlanet = false;
    }

    void RotateAtDestination(ref Vector2 Destination) {
        Vector3 CurrentRotation = this.transform.localEulerAngles;
        Vector3 AngleToDestination = new Vector3(0, 0, 0)
        {
            z = (Mathf.Rad2Deg * Mathf.Atan((Destination.y - this.transform.position.y) / (Destination.x - this.transform.position.x)))
        };
        if (Destination.x < this.transform.position.x) {
            AngleToDestination.z += 180;
        }
        transform.eulerAngles = AngleToDestination;
    }
}

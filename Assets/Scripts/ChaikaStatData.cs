using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaikaStatData : MonoBehaviour {

    public int totalHP = 20;
    public int currentHP = 20;
    public string shipClassName;
    public int crewMaxCapacity = 4;
    public int crewCurrentCapacity = 4;
    public int cargoSpace = 1000;
    public int cargoSpaceTaken = 0;
    public int bootlegSpace = 250;
    public int bootlegSpaceTaken = 0;
    public int fuelMax = 100;
    public int fuelCurrent = 100;
    public float shipSmuggleStat;

    // Use this for initialization
    void Start () {
        
	}
}

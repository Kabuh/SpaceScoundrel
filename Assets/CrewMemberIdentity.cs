using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrewMemberIdentity : MonoBehaviour {
    public string chosenProffession;
    public int skillLevel;

    public GameObject crewmanStats;

    public class Crew
    {
        public static List<string> Professions = new List<string>();
        public static List<Crew> CurrentCrew = new List<Crew>();

        public string profession = Professions[Mathf.FloorToInt(Random.value * Professions.Count)];
        public int skillLevel = Mathf.FloorToInt(Random.value * 10 + 1);
    }

    

    // Use this for initialization
    void Start()
    {
        crewmanStats = GameObject.Find("crewmanText");
        

        Crew.Professions.Add("pilot");
        Crew.Professions.Add("gunnery chief");
        Crew.Professions.Add("captain");
        Crew.Professions.Add("doctor");
        Crew.Professions.Add("engineer");
        Crew.Professions.Add("diplomat");
        Crew.Professions.Add("shepard");

        Crew myStats = new Crew();
        chosenProffession = myStats.profession;
        skillLevel = myStats.skillLevel;
        Crew.CurrentCrew.Add(myStats);
    }

    private void OnMouseOver()
    {
        if (crewmanStats != null)
        {
            Text theText = crewmanStats.transform.GetComponent<Text>();
            theText.text = "Profession: " + chosenProffession + "\n" + "Skill Level: " + skillLevel;
        }
        else {
            Debug.Log("TextObject isn't Found");
        }
        
    }

    private void OnMouseExit()
    {
        Text theText = crewmanStats.transform.GetComponent<Text>();
        theText.text = "";
    }

}

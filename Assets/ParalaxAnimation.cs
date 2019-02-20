using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxAnimation : MonoBehaviour {

    public int direction = 1;
    public float Speed;
    float Step;
    float Cumulative;
    


    // Use this for initialization
    void Awake () {
        
        //Animation();
    }
	
	// Update is called once per frame
	void Update () {
        Step = Time.deltaTime * direction * Speed;
        Cumulative += Step;
        this.transform.rotation = Quaternion.Euler(0, 0, Cumulative);
    }
}

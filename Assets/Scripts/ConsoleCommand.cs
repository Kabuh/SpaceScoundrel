using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleCommand : MonoBehaviour {

    public Text console;
    private bool open = false;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("`"))
        {
            if (!open)
            {
                console.transform.parent.gameObject.SetActive(true);
                open = true;
            } else
            {
                console.transform.parent.gameObject.SetActive(false);
                open = false;
            }
        }
        


	}
}

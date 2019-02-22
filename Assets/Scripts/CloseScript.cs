using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseScript : MonoBehaviour {

    private void Start()
    {
        QuestManager.closeTheQuestWindow += CloseThisMessage;
        QuestManager.closeChoiseBluttons += CloseButtons;
    }

    public GameObject[] childChoiseButtons;
    public GameObject closeButton;
    

    public void CloseThisMessage (){
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void CloseButtons() {
        foreach(GameObject GO in childChoiseButtons){
            GO.SetActive(false);
        }
        closeButton.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseScript : MonoBehaviour {

    private void Start()
    {
        QuestManager.closeTheQuestWindow += CloseThisMessage;
    }
    

    public void CloseThisMessage (){
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}

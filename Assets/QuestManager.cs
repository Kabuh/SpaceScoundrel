using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {

    public GameObject DataHub;
    private DataManagerScript GetData;

    public delegate void EventHolder();
    public static event EventHolder closeTheQuestWindow;
    public static event EventHolder closeChoiseBluttons;

    public GameObject EventMessage;
    public Text eventMessage;

    QuestClass CurrentQuestHolder;

    int StatHolder;             //delete later;
    int StatModifierHolder;
    bool ChoiseButtonOnePressed = false;
    bool ChoiseButtonTwoPressed = false;
    bool CloseButtonButtonPressed = false;
    public GameObject closeButton;

    bool CommandDone = false;

    bool activatableButton = false;

    [System.Serializable]
    public class QuestClass
    {
        public int index;
        public string name;
        public string description;
        public string choise1;
        public string choise2;
        public string outcomeAction1;
        public string outcomeAction2;
        public string outcomeStat1;
        public string outcomeStat2;
        public bool outcomeBool1;
        public bool outcomeBool2;
        public int outcomeAmmount1;
        public int outcomeAmmount2;
        public string outcomeText1;
        public string outcomeText2;
    }

    public class ListMaker
    {
        public List<QuestClass> QuestList = new List<QuestClass>();
    }



    string fileName = "QuestData.json";
    string path = "";
    string contents = "";

    //QuestClass test1 = new QuestClass();
    //QuestClass test2 = new QuestClass();

    List<QuestClass> QuestListHolder = new List<QuestClass>();


    private void Start()
    {
        GetData = DataHub.GetComponent<DataManagerScript>();
        ShipMovement.eventPopup += RandomEventWritter;

        path = Application.persistentDataPath + "/" + fileName;
        ReadAFile();
        QuestClass[] QuestArray = QuestListHolder.ToArray();


        QuestClass TestQuest = new QuestClass
        {
            index = 1,
            name = "Test is a Test",
            description = "Quest started and options are layed out",
            choise1 = "",
            choise2 = "",
            outcomeAction1 = "Take risk 1",
            outcomeAction2 = "Take risk 2",
            outcomeStat1 = "",
            outcomeStat2 = "",
            outcomeBool1 = true,
            outcomeBool2 = true,
            outcomeAmmount1 = 50,
            outcomeAmmount2 = 50
        };

        QuestClass ConnectorQuest = new QuestClass
        {
            index = 2,
            name = "Connector is another Test",
            description = "Quest started and options are layed out again",
            choise1 = "",
            choise2 = "",
            outcomeAction1 = "Take risk 1",
            outcomeAction2 = "Take risk 2",
            outcomeStat1 = "",
            outcomeStat2 = "",
            outcomeBool1 = true,
            outcomeBool2 = true,
            outcomeAmmount1 = 50,
            outcomeAmmount2 = 50
        };

        //contents = JsonUtility.ToJson(TestQuest);
        //MakeAFile(contents);
    }

    private void Update()
    {
        if ((Input.GetKeyDown("1") && activatableButton) || ChoiseButtonOnePressed)
        {
            activatableButton = false;
            ChoiseButtonOnePressed = false;
            closeChoiseBluttons();
            Interpreter(ref CurrentQuestHolder.outcomeAction1, ref CurrentQuestHolder.outcomeStat1, ref CurrentQuestHolder.outcomeBool1, ref CurrentQuestHolder.outcomeAmmount1);
            eventMessage.text = CurrentQuestHolder.outcomeText1;
            eventMessage.text += string.Format("\n\n Your  [{0}] stat will receive modifier of [{1}] for the ammount of: [{2}]", CurrentQuestHolder.outcomeStat1, CurrentQuestHolder.outcomeAction1, CurrentQuestHolder.outcomeAmmount1);
            CommandDone = true;
        }
        if ((Input.GetKeyDown("2") && activatableButton) || ChoiseButtonTwoPressed)
        {
            activatableButton = false;
            ChoiseButtonTwoPressed = false;
            closeChoiseBluttons();
            Interpreter(ref CurrentQuestHolder.outcomeAction2, ref CurrentQuestHolder.outcomeStat2, ref CurrentQuestHolder.outcomeBool2, ref CurrentQuestHolder.outcomeAmmount2);
            eventMessage.text = CurrentQuestHolder.outcomeText2;
            eventMessage.text += string.Format("\n\n Your [{0}] stat will receive modifier of  [{1}] for the ammount of: [{2}]", CurrentQuestHolder.outcomeStat2, CurrentQuestHolder.outcomeAction2, CurrentQuestHolder.outcomeAmmount2);
            CommandDone = true;
        }
        if ((Input.GetKeyDown("space")|| CloseButtonButtonPressed) && CommandDone)
        {
            CloseButtonButtonPressed = false;
            closeTheQuestWindow();
            CommandDone = false;
        }
    }

    void MakeAFile(string x) { //delete this later
        File.AppendAllText(path, x);
        Debug.Log("File created.... Maybe");
    }

    void ReadAFile() {
        try
        {
            if (File.Exists(path))
            {
                contents = File.ReadAllText(path);
                ListMaker listMakerRead = new ListMaker();
                listMakerRead = JsonUtility.FromJson<ListMaker>(contents);
                QuestListHolder = listMakerRead.QuestList;
            }
            else {
                Debug.Log("File not found in the location" + path);
            }
        }
        catch (System.Exception ex) {
            Debug.Log(ex);
        }
    }

    void RandomEventWritter()
    {
        activatableButton = true;
        closeButton.SetActive(false);
        CurrentQuestHolder = QuestListHolder[Mathf.FloorToInt(Random.value * QuestListHolder.Count)];
        EventMessage.SetActive(true);
        eventMessage.text = "\t[ " + CurrentQuestHolder.name + " ]" + "\n\n" + CurrentQuestHolder.description + "\n\n\t" + "1. " + CurrentQuestHolder.choise1 + "\n\n\t" + "2. " + CurrentQuestHolder.choise2;
        Time.timeScale = 0;
    }

    void Interpreter(ref string command, ref string statName, ref bool swttch,  ref int ammount)
    {
        int value = -1;
        bool subBool;

        switch (statName)
        {
            case "HP":
                value = GetData.CurrentHP;
                break;
            case "Money":
                value = GetData.Money;
                break;
            case "Cargo":
                value = GetData.CargoSpaceTaken;
                break;
            case "Fuel":
                value = GetData.FuelCurrent;
                break;
            case "Nothing":
                break;
            default:
                Debug.Log("StatInterpreter  unrecognized Data");
                break;
        }

        if (value != -1)
        {
            switch (command)
            {
                case "Add":
                    value = value + ammount;
                    break;
                case "Reduce":
                    value = value - ammount;
                    if (value >= 0)
                    {
                        break;
                    }
                    else
                    {
                        value = 0;
                        break;
                    }
                case "SwitchOn":
                    subBool = true;
                    break;
                case "SwitchOff":
                    subBool = false;
                    break;
                default:
                    Debug.Log("Quest interpreter reads unrecognized data");
                    break;
            }

            switch (statName)
            {
                case "HP":
                    GetData.CurrentHP = value;
                    break;
                case "Money":
                    GetData.Money = value;
                    break;
                case "Cargo":
                    GetData.CargoSpaceTaken = value;
                    break;
                case "Fuel":
                    GetData.FuelCurrent = value;
                    break;
                case "Nothing":
                    break;
                default:
                    Debug.Log("StatInterpreter  unrecognized Data");
                    break;
            }
        }
        else {
            Debug.Log("Error of writting value");
        }
    }

    public void ButtonSwitchOne() {
        ChoiseButtonOnePressed = true;
    }

    public void ButtonSwitchTwo()
    {
        ChoiseButtonTwoPressed = true;
    }

    public void ButtonClosePress() {
        CloseButtonButtonPressed = true;
    }
}

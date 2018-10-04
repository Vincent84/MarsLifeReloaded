using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum InteractableType
{
    EQUIPMENT,
    PANELS,
    MINERALS,
    TUBES,
    PIECES,
    PICTURE,
    BLACKBOX,
    COMPUTER,
    WALL,
    ROCK,
    COORDINATES,
    ANTENNA,
    FLAG,
    AREASCANNER,
    AREAGEIGER
}

public static class Database
{

    /*public static List<DataEquipment> equipments = new List<DataEquipment>();
    public static List<DataPanel> panels = new List<DataPanel>();
    public static List<DataMineral> minerals = new List<DataMineral>();
    public static List<DataTube> tubes = new List<DataTube>();
    public static List<DataProbe> probes = new List<DataProbe>();*/

    public static List<InteractableObject> interactableObjects = new List<InteractableObject>();

    public static List<DataGadget> gadgets = new List<DataGadget>();
    public static List<DataScene> scenes = new List<DataScene>();
    public static List<DataQuest> quests = new List<DataQuest>();

	//Save
    public static DataQuest currentQuest;
    public static string currentScene;
    public static Vector3 playerPosition;
	public static string activeCheckpoint;
    public static bool playerIsOutside;
	//end save


    [Serializable]
    public class InteractableObject
    {

        public InteractableObject(InteractableType pInteractableType, string pInteractableName, bool pIsInteractable, string pScene, bool pIsDestroyable)
        {
            this.type = pInteractableType;
            this.interactableName = pInteractableName;
            this.isInteractable = pIsInteractable;
            this.sceneContainer = pScene;
            this.isDestroyable = pIsDestroyable;
        }

		//Save
        public InteractableType type;
        public string interactableName;
        public bool isInteractable;
        public bool isDestroyable;
        public string sceneContainer;
		//Save
    }


    /*[Serializable]
    public class DataEquipment: InteractableObject
    {
        
    }

    [Serializable]
    public class DataPanel : InteractableObject
    {
        
    }

    [Serializable]
    public class DataMineral : InteractableObject
    {
        
    }

    [Serializable]
    public class DataTube : InteractableObject
    {
        
    }

    [Serializable]
    public class DataProbe : InteractableObject
    {
        
    }*/

    [Serializable]
    public class DataGadget
    {
        public DataGadget(string pGadgetName, bool pIsActive)
        {
            gadgetName = pGadgetName;
            isActive = pIsActive;
        }

		//Save
        public string gadgetName;
        public bool isActive;
		//end save
    }

    [Serializable]
    public class DataScene
    {

        public DataScene(string pSceneName, bool pIsUnlocked)
        {
            this.sceneName = pSceneName;
            this.isUnlocked = pIsUnlocked;
        }

		//Save
        public string sceneName;
        public bool isUnlocked;
		//end save
    }

    [Serializable]
    public class DataQuest
    {

        public DataQuest(Quest.QuestState pState, string pName, int pPriority)
        {
            this.currentState = pState;
            this.questName = pName;
            this.questPriority = pPriority;
        }

		//Save
        public Quest.QuestState currentState;
        public string questName;
        public List<DataTask> tasks = new List<DataTask>();
        public DataTask activedTask; 
        public int questPriority;
		//end save
    }

    [Serializable]
    public class DataTask
    {
        public DataTask(Task.TaskState pState, string pName, int pPriority)
        {
            this.currentState = pState;
            this.taskName = pName;
            this.taskPriority = pPriority;
        }

		//Save
        public Task.TaskState currentState;
        public string taskName;
        public int taskPriority;
		//end save
    }

    public static void PrintInteractable()
    {
        foreach(InteractableObject o in interactableObjects)
        {
            Debug.Log(o.interactableName + ": " + o.isInteractable);
        }
    }

    public static void ResetDatabase()
    {
        interactableObjects.Clear();
        gadgets.Clear();
        quests.Clear();
        scenes.Clear();
        playerIsOutside = false;
        playerPosition = new Vector3(0, 0, 0);
    }

}

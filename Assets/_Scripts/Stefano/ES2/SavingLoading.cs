using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Invector.CharacterController;

public class SavingLoading : MonoBehaviour 
{

	#region Private 

	private int number = 0;

	#endregion

	#region Save

	/// <summary>
	/// Metodo che esegue tutti i salvataggi
	/// </summary>
	public void Save()
	{

		SaveCurrentScene ();
		SavePlayer ();
		//SaveCurrentQuest ();
		SaveData ();

		//Salviamo il numero di quest per il caricamento
		ES2.Save (Database.quests.Count, PlayerPrefs.GetString ("Slot") + ".txt?tag=questNumberQuests"); 

		for (int i = 0; i < Database.quests.Count; i++) 
		{

			SaveQuest (Database.quests [i]);

			number++;

		}

		number = 0;

		SaveGadget ();
		SaveScenes ();
		SaveInteractableObjects ();

	}

	private void SaveData()
	{

		ES2.Save (System.DateTime.Now.ToString (), PlayerPrefs.GetString ("Slot") + ".txt?tag=SaveTime");

	}

	/// <summary>
	/// Metodo che salva il nome della scnea corrente
	/// </summary>
	private void SaveCurrentScene()
	{

		//String
		ES2.Save (Database.currentScene, PlayerPrefs.GetString ("Slot") + ".txt?tag=currentScene");

	}

	/// <summary>
	/// Metodo che salava le informazioni del player
	/// </summary>
	private void SavePlayer()
	{
        ES2.Save (vThirdPersonController.instance.transform.position, PlayerPrefs.GetString ("Slot") + ".txt?tag=player");
    
        Database.playerPosition = vThirdPersonController.instance.transform.position;

        ES2.Save(Database.playerIsOutside, PlayerPrefs.GetString("Slot") + ".txt?tag=playerIsOutside");
    }

    /// <summary>
    /// Metodo che permette il salvataggio di una quest
    /// </summary>
    private void SaveQuest(Database.DataQuest q)
	{

		//Enum string
		ES2.Save (q.currentState, PlayerPrefs.GetString ("Slot") + ".txt?tag="+number+"CurrentState");
		//string
		ES2.Save (q.questName, PlayerPrefs.GetString ("Slot") + ".txt?tag="+number+"Name");

		//Mi slalvo il numero di task che ha la quest per il caricamento
		ES2.Save (q.tasks.Count, PlayerPrefs.GetString ("Slot") + ".txt?tag=" + number + "NumberTasks");

		for (int i = 0; i < q.tasks.Count; i++) 
		{

			//bool
			ES2.Save (q.tasks [i].currentState, PlayerPrefs.GetString ("Slot") + ".txt?tag="+number+"TaskCurrentState" + i);
			//string
			ES2.Save (q.tasks [i].taskName, PlayerPrefs.GetString ("Slot") + ".txt?tag="+number+"TaskTaskName" + i);
			//int
			ES2.Save (q.tasks[i].taskPriority, PlayerPrefs.GetString ("Slot") + ".txt?tag="+number+"TaskTaskPriority" + i);

		}

		//int 
		ES2.Save (q.questPriority, PlayerPrefs.GetString ("Slot") + ".txt?tag="+number+"QuestPriority");

	}

	/// <summary>
	/// Metodo che permette il salvataggio della quest corrente
	/// </summary>
	private void SaveCurrentQuest()
	{

		//Enum string
		ES2.Save (Database.currentQuest.currentState, PlayerPrefs.GetString ("Slot") + ".txt?tag=currentQuestCurrentState");
		//ES2.Save (Database.currentQuest.currentState, PlayerPrefs.GetString ("Slot") + ".txt?tag="+Database.currentQuest.questName+"CurrentState");
		//string
		ES2.Save (Database.currentQuest.questName, PlayerPrefs.GetString ("Slot") + ".txt?tag=currentQuestName");

		/*for (int i = 0; i < Database.currentQuest.tasks.Count; i++) 
		{

			//bool
			ES2.Save (Database.currentQuest.tasks [i].currentState, PlayerPrefs.GetString ("Slot") + ".txt?tag=currentQuestTaskCurrentState" + i);
			//string
			ES2.Save (Database.currentQuest.tasks [i].taskName, PlayerPrefs.GetString ("Slot") + ".txt?tag=currentQuestTaskTaskName" + i);
			//int
			ES2.Save (Database.currentQuest.tasks[i].taskPriority, PlayerPrefs.GetString ("Slot") + ".txt?tag=currentQuestTaskTaskPriority" + i);

		}*/

		//bool
		ES2.Save (Database.currentQuest.activedTask.currentState, PlayerPrefs.GetString ("Slot") + ".txt?tag=currentQuestActivedTaskCurrentState");
		//string
		ES2.Save (Database.currentQuest.activedTask.taskName, PlayerPrefs.GetString ("Slot") + ".txt?tag=currentQuestActivedTaskTaskName");
		//int
		ES2.Save (Database.currentQuest.activedTask.taskPriority, PlayerPrefs.GetString ("Slot") + ".txt?tag=currentQuestActivedTaskTaskPriority");

		//int 
		ES2.Save (Database.currentQuest.questPriority, PlayerPrefs.GetString ("Slot") + ".txt?tag=currentQuestQuestPriority");

		Debug.Log ("Quest corrente salvata");

	}

	/// <summary>
	/// Metodo che salva i gadgets
	/// </summary>
	private void SaveGadget()
	{

		//Salvimao il numero di gadget per il caricamento
		ES2.Save (Database.gadgets.Count, PlayerPrefs.GetString ("Slot") + ".txt?tag=gadgetNumberGadgets");

		for (int i = 0; i < Database.gadgets.Count; i++) 
		{

			//string
			ES2.Save (Database.gadgets[i].gadgetName, PlayerPrefs.GetString ("Slot") + ".txt?tag=gadgetGadgetName"+i);
			//bool
			ES2.Save (Database.gadgets[i].isActive, PlayerPrefs.GetString ("Slot") + ".txt?tag=gadgetIsActive"+i);

		}

		Debug.Log ("Gadegts salvati");

	}

	/// <summary>
	/// Salvataggio delle scene
	/// </summary>
	private void SaveScenes()
	{

		//Salvo il numero delle scene per il caricamento
		ES2.Save (Database.scenes.Count, PlayerPrefs.GetString ("Slot") + ".txt?tag=scenesNumberScenes");

		for (int i = 0; i < Database.scenes.Count; i++) 
		{

			//String
			ES2.Save (Database.scenes[i].sceneName, PlayerPrefs.GetString ("Slot") + ".txt?tag=scenesSceneName"+i);
			//bool
			ES2.Save (Database.scenes[i].isUnlocked, PlayerPrefs.GetString ("Slot") + ".txt?tag=scenesIsActive"+i);

		}

		Debug.Log ("Scene salvate");

	}
		
	/// <summary>
	/// Salavtaggio degli interactable Objects
	/// </summary>
	private void SaveInteractableObjects()
	{

		//Salvo il numero degli interactableObject per il caricamento
		ES2.Save(Database.interactableObjects.Count, PlayerPrefs.GetString ("Slot") + ".txt?tag=interactableObjectNumberInteractableObjects");

		for (int i = 0; i < Database.interactableObjects.Count; i++) 
		{

			//Enum String
			ES2.Save (Database.interactableObjects[i].type, PlayerPrefs.GetString ("Slot") + ".txt?tag=interactableObjectType"+i);
			//String
			ES2.Save (Database.interactableObjects[i].interactableName, PlayerPrefs.GetString ("Slot") + ".txt?tag=interactableObjectIntercatableName"+i);
			//bool
			ES2.Save (Database.interactableObjects[i].isInteractable, PlayerPrefs.GetString ("Slot") + ".txt?tag=interactableObjectIsinteractable"+i);
			//string
			ES2.Save (Database.interactableObjects[i].sceneContainer, PlayerPrefs.GetString ("Slot") + ".txt?tag=interactableObjectSceneContainer"+i);
            //bool
            ES2.Save(Database.interactableObjects[i].isDestroyable, PlayerPrefs.GetString("Slot") + ".txt?tag=interactableObjectIsdestroyable" + i);

        }

		Debug.Log ("Interctable Objects salvati");

	}

	#endregion

	#region Load

	/// <summary>
	/// Metodo che avvia il caricamento con coroutine
	/// </summary>
	public void Load()
	{
        Database.ResetDatabase();

        StartCoroutine (LoadCoroutine ());

    }

	/// <summary>
	/// Metodo che esegue tutti i metodi di caricamento
	/// </summary>
	public IEnumerator LoadCoroutine()
	{

		if (ES2.Exists (PlayerPrefs.GetString ("Slot") + ".txt")) 
		{

			LoadCurrentScene ();
			LoadPlayer ();
			//LoadCurrentQuest ();

			//Carichiamo il numero di qust che abbiamo salvato
			int numberQuest = ES2.Load<int> (PlayerPrefs.GetString ("Slot") + ".txt?tag=questNumberQuests");

			for (int i = 0; i < numberQuest; i++) 
			{

				LoadQuest ();

				number++;

			}

			number = 0;

			LoadGadgets ();
			LoadScene ();
			LoadInteractableObjects ();

			GameManager.newGame = false;

			yield return null;

		} 
		else 
		{
			
			Debug.Log ("Il file non esiste");
			GameManager.newGame = true;

		}

		yield return null;

	}

	/// <summary>
	/// Metodo che carica il nome della scena corrente
	/// </summary>
	private void LoadCurrentScene()
	{

		Database.currentScene =  ES2.Load<string> (PlayerPrefs.GetString ("Slot") + ".txt?tag=currentScene");

		Debug.Log ("Scena corrente caricata");

	}

	/// <summary>
	/// Metodo che carica le informazioni del player
	/// </summary>
	private void LoadPlayer()
	{

        Database.playerPosition = ES2.Load<Vector3> (PlayerPrefs.GetString ("Slot") + ".txt?tag=player");
        Database.playerIsOutside = ES2.Load<bool>(PlayerPrefs.GetString("Slot") + ".txt?tag=playerIsOutside");


    }

    /// <summary>
    /// Metodo che carica una quest
    /// </summary>
    private void LoadQuest()
	{

		//Costruttore della quest
		Database.DataQuest dataQuest = new Database.DataQuest (
			ES2.Load<Quest.QuestState> (PlayerPrefs.GetString ("Slot") + ".txt?tag=" + number + "CurrentState"),
			ES2.Load<string> (PlayerPrefs.GetString ("Slot") + ".txt?tag=" + number + "Name"),
			ES2.Load<int> (PlayerPrefs.GetString ("Slot") + ".txt?tag=" + number + "QuestPriority"));

        

		//Inizilizzo la variabile Task
		Database.DataTask task;

		int numberTasks = ES2.Load<int> (PlayerPrefs.GetString ("Slot") + ".txt?tag=" + number + "NumberTasks");
		
		for (int i = 0; i < numberTasks; i++) 
		{

			task = new Database.DataTask(
				ES2.Load<Task.TaskState> (PlayerPrefs.GetString ("Slot") + ".txt?tag="+ number +"TaskCurrentState" + i),
				ES2.Load<string> (PlayerPrefs.GetString ("Slot") + ".txt?tag="+ number +"TaskTaskName" + i),
				ES2.Load<int> (PlayerPrefs.GetString ("Slot") + ".txt?tag="+ number +"TaskTaskPriority" + i)
			);

            if (dataQuest.currentState == Quest.QuestState.ENABLED &&
                task.currentState == Task.TaskState.ENABLED || task.currentState == Task.TaskState.READY || task.currentState == Task.TaskState.ACTIVED)
            {
                Database.currentQuest = dataQuest;
                dataQuest.activedTask = task;
            }

            //Aggiungo la task alla quest
            dataQuest.tasks.Add (task);

		}
			
		//popolo la lista di quest 
		Database.quests.Add (dataQuest);

	}

	/// <summary>
	/// Metodo che carica la quest corrente
	/// </summary>
	private void LoadCurrentQuest()
	{

		Database.currentQuest.currentState = ES2.Load<Quest.QuestState> (PlayerPrefs.GetString ("Slot") + ".txt?tag=currentQuestCurrentState");
		Database.currentQuest.questName = ES2.Load<string> (PlayerPrefs.GetString ("Slot") + ".txt?tag=currentQuestName");

		/*for (int i = 0; i < Database.currentQuest.tasks.Count; i++) 
		{

			Database.currentQuest.tasks [i].currentState = ES2.Load<Task.TaskState> (PlayerPrefs.GetString ("Slot") + ".txt?tag=currentQuestTaskCurrentState" + i);
			Database.currentQuest.tasks [i].taskName = ES2.Load<string> (PlayerPrefs.GetString ("Slot") + ".txt?tag=currentQuestTaskTaskName" + i);
			Database.currentQuest.tasks [i].taskPriority = ES2.Load<int> (PlayerPrefs.GetString ("Slot") + ".txt?tag=currentQuestTaskTaskPriority" + i);

		}*/

		Database.currentQuest.activedTask.currentState = ES2.Load<Task.TaskState> (PlayerPrefs.GetString ("Slot") + ".txt?tag=currentQuestActivedTaskCurrentState");
		Database.currentQuest.activedTask.taskName = ES2.Load<string> (PlayerPrefs.GetString ("Slot") + ".txt?tag=currentQuestActivedTaskTaskName");
		Database.currentQuest.activedTask.taskPriority = ES2.Load<int> (PlayerPrefs.GetString ("Slot") + ".txt?tag=currentQuestActivedTaskTaskPriority");
		Database.currentQuest.questPriority = ES2.Load<int> (PlayerPrefs.GetString ("Slot") + ".txt?tag=currentQuestQuestPriority");

		Debug.Log ("Quest corrente caricata");

	}

	/// <summary>
	/// Metodo che carica i gadgets
	/// </summary>
	private void LoadGadgets()
	{

		Database.DataGadget gadget;

		int numberGadgets = ES2.Load<int> (PlayerPrefs.GetString ("Slot") + ".txt?tag=gadgetNumberGadgets");

		for (int i = 0; i < numberGadgets; i++) 
		{
			
			gadget = new Database.DataGadget (
				ES2.Load<string> (PlayerPrefs.GetString ("Slot") + ".txt?tag=gadgetGadgetName" + i),
				ES2.Load<bool> (PlayerPrefs.GetString ("Slot") + ".txt?tag=gadgetIsActive" + i));


			Database.gadgets.Add (gadget);

		}

		Debug.Log ("Gadegts caricati");

	}

	/// <summary>
	/// Metodo che carica i dati delle scene
	/// </summary>
	private void LoadScene()
	{

		Database.DataScene scena;

		int numberScenes = ES2.Load<int> (PlayerPrefs.GetString ("Slot") + ".txt?tag=scenesNumberScenes");

		for (int i = 0; i < numberScenes; i++) 
		{
			scena = new Database.DataScene (
				ES2.Load<string> (PlayerPrefs.GetString ("Slot") + ".txt?tag=scenesSceneName" + i),
				ES2.Load<bool> (PlayerPrefs.GetString ("Slot") + ".txt?tag=scenesIsActive" + i));

			Database.scenes.Add (scena);

		}

		Debug.Log ("Scene caricate");

	}

	/// <summary>
	/// Metodo che carica gli interactableObjects
	/// </summary>
	private void LoadInteractableObjects()
	{

		Database.InteractableObject obj;

		int numberInteractableObjects = ES2.Load<int> (PlayerPrefs.GetString ("Slot") + ".txt?tag=interactableObjectNumberInteractableObjects");

		for (int i = 0; i < numberInteractableObjects; i++) 
		{

			obj = new Database.InteractableObject (
				ES2.Load<InteractableType> (PlayerPrefs.GetString ("Slot") + ".txt?tag=interactableObjectType" + i),
				ES2.Load<string> (PlayerPrefs.GetString ("Slot") + ".txt?tag=interactableObjectIntercatableName" + i),
				ES2.Load<bool> (PlayerPrefs.GetString ("Slot") + ".txt?tag=interactableObjectIsinteractable" + i),
                ES2.Load<string> (PlayerPrefs.GetString ("Slot") + ".txt?tag=interactableObjectSceneContainer" + i),
                ES2.Load<bool>(PlayerPrefs.GetString("Slot") + ".txt?tag=interactableObjectIsdestroyable" + i));

			Database.interactableObjects.Add (obj);

		}

		Debug.Log ("Interctable Objects caricati");

	}

    #endregion

    #region Function

    public void StartNewGame()
    {
        GameManager.newGame = true;
    }

    #endregion

}

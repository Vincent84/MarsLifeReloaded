using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Invector.CharacterController;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public static bool newGame = true;
    public string activeCheckpoint;
    [HideInInspector]
    public GadgetManager gadgetManager;

    public Musica2 music;

    List<string> scenes;
    QuestManager questManager;
    vThirdPersonController player;
    //Invector.vGameController gameController;

    private void Awake()
    {

        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(this);

        //questManager = QuestManager.instance;
        questManager = FindObjectOfType<QuestManager>();
        gadgetManager = FindObjectOfType<GadgetManager>();
        player = FindObjectOfType<vThirdPersonController>();

        scenes = new List<string>();

        if (newGame)
        {

            Database.ResetDatabase();

            questManager.InitQuests();
            gadgetManager.InitGadgets();
            this.SetScenes();

        }
        else
        {

            player.GetComponent<GenericSettings>().SetPlayer();
            questManager.SetQuests();
            gadgetManager.SetGadgets();
            

        }
            
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        questManager.CheckQuest();
        Database.currentScene = scene.name;
        SetObjectScene(scene);
    }

    void SetObjectScene(Scene scene)
    {
        if(Database.interactableObjects.Count > 0 && Database.interactableObjects.Exists(x => x.sceneContainer == scene.name))
        {
            foreach(Database.InteractableObject interactable in Database.interactableObjects)
            {
                if(interactable.sceneContainer == scene.name && !interactable.isInteractable)
                {
                    GameObject interactableToDestroy = GameObject.Find(interactable.interactableName);
                    if (interactable.isDestroyable)
                        Destroy(interactableToDestroy);
                    else
                    {
                        interactableToDestroy.transform.GetChild(1).gameObject.SetActive(true);
                        Destroy(interactableToDestroy.transform.GetChild(0).gameObject);
                    }  
                }
            }
        }
    }

    void SetScenes()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string[] containerSceneName = SceneUtility.GetScenePathByBuildIndex(i).Split('/');
            string[] sceneName = containerSceneName[2].Split('.');
            scenes.Add(sceneName[0]);
            bool isUnlocked = false;
            if (i == 0 || i == 1)
            {
                isUnlocked = true;
            }
            Database.DataScene dataScene = new Database.DataScene(sceneName[0], isUnlocked);
            Database.scenes.Add(dataScene);
        }

    }




    /*public void PrintData()
    {

        print("Ciao");

        foreach (Database.DataQuest quest in Database.quests)
        {
            print(quest.questName);
            print(quest.currentState);
            foreach (Database.DataTask task in quest.tasks)
            {

                print(task.taskName);
                print(task.currentState);
            }

            print("\n");
        }

        foreach(Database.DataScene scene in Database.scenes)
        {
            print(scene.sceneName + ": " + scene.isUnlocked);
        }
    }*/
}

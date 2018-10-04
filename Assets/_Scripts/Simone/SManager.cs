using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;
using Sirenix.OdinInspector;
using DG.Tweening;

public class SManager : MonoBehaviour {

    private PostProcessingBehaviour behaviour;                                                              // Profilo del Post-Processing (vThirdController)
    private GameObject quest;                                                                               // Cerca il Gameobject "Quest"

    [BoxGroup("PostProcessing Profiles for Gale")] public PostProcessingProfile insideBase;                 // Post-Processing esclusivi per il Cratere Gale
    [BoxGroup("PostProcessing Profiles for Gale")] public PostProcessingProfile outsideBase;                // Post-Processing esclusivi per il Cratere Gale

    [BoxGroup("PostProcessing Profiles for Other Scenes")] public PostProcessingProfile crater;             // Post-Processing delle altre scene
    [BoxGroup("PostProcessing Profiles for Other Scenes")] public PostProcessingProfile labyrinthus;        // Post-Processing delle altre scene
    [BoxGroup("PostProcessing Profiles for Other Scenes")] public PostProcessingProfile marineris;          // Post-Processing delle altre scene
    [BoxGroup("PostProcessing Profiles for Other Scenes")] public PostProcessingProfile olympus;            // Post-Processing delle altre scene

    [BoxGroup("Directional Light")] public GameObject lightGroup;                                           // LightGroup Gameobject

    [BoxGroup("Trigger Collider (Gale Crater)")] public BoxCollider outsideTrigger; // Posizione Cratere (X 62, Y 1, Z -280) Rotation (Y -50)
    [BoxGroup("Trigger Collider (Gale Crater)")] public BoxCollider insideTrigger; // Posizione Cratere (x 60, Y 1, Z -278,56) Rotation (Y -50)

    // Scena

    private Text sceneText;
    private Text temperatureText;
    private Text dayText;

    // Sol

    private Image fadeSol;
    private Text solLeft;
    private Text solCenter;
    private bool quest1, quest2, quest3, quest4, quest5, quest6, quest7, quest8;

    // Missions

    private Text missionTitleText;
    private Text descriptionText;
    private bool mission1, mission2, mission3;

    private int temperatureValue;                                                                   // Random delle Temperature 
    private int dialogueValue;                                                                      // Random dei Dialoghi 

    private bool isGale;                                                                            // Bool per le Animazioni delle Icone
    private bool isValles;                                                                          // Bool per le Animazioni delle Icone
    private bool isNoctis;                                                                          // Bool per le Animazioni delle Icone                                                                
    private bool isOlympus;                                                                         // Bool per le Animazioni delle Icone

    private bool isGaleLoaded;                                                                      // Bool per il cambio Post-Processing

    // Ingame Dialogues

    private RectTransform dialogueBox;
    private Text dialogueText;

    void Start()
    {
        // Attiva tutti i figli di SManager Canvas

        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        behaviour = FindObjectOfType<vThirdPersonCamera>().GetComponent<PostProcessingBehaviour>();
        quest = GameObject.Find("Quests");

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        temperatureValue = Random.Range(-60, 20);
        dialogueValue = Random.Range(0, 10);

        // GET SCENE INFO

        sceneText = transform.GetChild(0).transform.GetChild(0).GetComponent<Text>();
        temperatureText = transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();
        dayText = transform.GetChild(0).transform.GetChild(2).GetComponent<Text>();

        // GET SOL INFO

        fadeSol = transform.GetChild(1).transform.GetChild(0).GetComponent<Image>();
        solLeft = transform.GetChild(1).transform.GetChild(1).GetComponent<Text>();
        solCenter = transform.GetChild(1).transform.GetChild(2).GetComponent<Text>();

        // GET MISSION INFO

        missionTitleText = transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
        descriptionText = transform.GetChild(2).transform.GetChild(1).GetComponent<Text>();

        // GET DIALOGUES INFO

        dialogueBox = transform.GetChild(4).transform.GetChild(0).GetComponent<RectTransform>();
        dialogueText = transform.GetChild(4).transform.GetChild(0).GetChild(3).GetComponent<Text>();

        // FADE COMPONENTS

        sceneText.DOFade(0, 0);
        temperatureText.DOFade(0, 0);
        dayText.DOFade(0, 0);

        solLeft.DOFade(0, 0);
        solCenter.DOFade(0, 0);

        missionTitleText.DOFade(0, 0);
        descriptionText.DOFade(0, 0);

        // CRATERE GALE

        if (sceneName == "Gale Crater")
        {
            isGale = true;
            isGaleLoaded = true;
            StartCoroutine(LandInfo("Gale Crater"));
        }
        else
        {
            isGaleLoaded = false;
        }

        // ALTRE SCENE

        if (sceneName == "Noctis Labyrinthus")
        {
            behaviour.profile = labyrinthus;
            isNoctis = true;
            StartCoroutine(LandInfo("Noctis Labyrinthus"));
            StartCoroutine(DialoguesInfo());
        }

        else if (sceneName == "Olympus Mons")
        {
            behaviour.profile = olympus;
            isOlympus = true;
            StartCoroutine(LandInfo("Olympus Mons"));
            StartCoroutine(DialoguesInfo());
        }

        else if (sceneName == "Valles Marineris")
        {
            behaviour.profile = marineris;
            isValles = true;
            StartCoroutine(LandInfo("Valles Marineris"));
            StartCoroutine(DialoguesInfo());
        }
    }

    private void Update()
    {
        // INFO MISSION

        // QUEST1 (QUEST1)
        if (quest.transform.GetChild(0).GetComponent<Quest>().currentState == Quest.QuestState.ENABLED && !mission1)
        {
            StartCoroutine(MissionInfo("Welcome on board", "Move the player using 'WASD' or your left analogic stick", 16));
            mission1 = true;
        }

        // USA LA PALA SUI PANNELLI (QUEST1)
        else if (quest.transform.GetChild(0).transform.GetChild(2).GetComponent<TaskInteract>().currentState == Task.TaskState.READY && !mission2)
        {
            StartCoroutine(MissionInfo("Clear the Solar Panels", "Use the Action Button 'E' or 'X' to use you shovel", 5));
            mission2 = true;
        }

        // VAI ALLA VALLE MARINERIS (QUEST2)
        else if (quest.transform.GetChild(1).transform.GetChild(1).GetComponent<TaskTravel>().currentState == Task.TaskState.ENABLED && !mission3)
        {
            StartCoroutine(MissionInfo("exploring mars", "use the land rover to reach different locations on mars", 5));
            mission3 = true;
        }

        #region SOL
        if (quest.transform.GetChild(0).GetComponent<Quest>().currentState == Quest.QuestState.ENABLED && !quest1)
        {
            quest1 = true;
            StartCoroutine(SolLeftInfo());
        }
        #endregion
    }

    // START SCENE

    public IEnumerator LandInfo(string name)
    {
        #region Temperature/Light/Post-Processing
        if (isGaleLoaded == true)
        {
            if (FindObjectOfType<GenericSettings>().isOutside == false)
            {
                dayText.text = "Time of Day: Inside the base";
                behaviour.profile = crater;
            }
            else if (FindObjectOfType<GenericSettings>().isOutside == true)
            {
                dayText.text = "Time of Day: Outside the base";
                behaviour.profile = crater;
            }

            if (temperatureValue >= -60 && temperatureValue < -30)
            {
                dayText.text = "Time of Day: Sunset";
                lightGroup.transform.GetChild(0).gameObject.SetActive(true);
                RenderSettings.sun = lightGroup.transform.GetChild(0).GetComponent<Light>();

                lightGroup.transform.GetChild(1).gameObject.SetActive(false);
                lightGroup.transform.GetChild(2).gameObject.SetActive(false);
            }
            else if (temperatureValue >= -30 && temperatureValue < 0)
            {
                dayText.text = "Time of Day: Midday";
                lightGroup.transform.GetChild(1).gameObject.SetActive(true);
                RenderSettings.sun = lightGroup.transform.GetChild(1).GetComponent<Light>();

                lightGroup.transform.GetChild(0).gameObject.SetActive(false);
                lightGroup.transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                dayText.text = "Time of Day: Twilight";
                lightGroup.transform.GetChild(2).gameObject.SetActive(true);
                RenderSettings.sun = lightGroup.transform.GetChild(2).GetComponent<Light>();

                lightGroup.transform.GetChild(0).gameObject.SetActive(false);
                lightGroup.transform.GetChild(1).gameObject.SetActive(false);
            }
        }

        if(isGaleLoaded == false)
        {
            if (temperatureValue >= -60 && temperatureValue < -30)
            {
                dayText.text = "Time of Day: Sunset";
                lightGroup.transform.GetChild(0).gameObject.SetActive(true);
                RenderSettings.sun = lightGroup.transform.GetChild(0).GetComponent<Light>();

                lightGroup.transform.GetChild(1).gameObject.SetActive(false);
                lightGroup.transform.GetChild(2).gameObject.SetActive(false);
            }
            else if(temperatureValue >= -30 && temperatureValue < 0)
            {
                dayText.text = "Time of Day: Midday";
                lightGroup.transform.GetChild(1).gameObject.SetActive(true);
                RenderSettings.sun = lightGroup.transform.GetChild(1).GetComponent<Light>();

                lightGroup.transform.GetChild(0).gameObject.SetActive(false);
                lightGroup.transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                dayText.text = "Time of Day: Twilight";
                lightGroup.transform.GetChild(2).gameObject.SetActive(true);
                RenderSettings.sun = lightGroup.transform.GetChild(2).GetComponent<Light>();

                lightGroup.transform.GetChild(0).gameObject.SetActive(false);
                lightGroup.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        #endregion

        yield return new WaitForSeconds(2.5f);
        sceneText.text = name;
        sceneText.DOFade(1, 4);

        yield return new WaitForSeconds(2);
        temperatureText.text = "Temperature: " + temperatureValue + "°";
        temperatureText.DOFade(1, 2);

        yield return new WaitForSeconds(1);
        dayText.DOFade(1, 2);

        #region Icon Animations  // SONO STATE DISABILITATE LE "IMAGES" DI TUTTI GLI OGGETTI TRANNE I "COLLECTABLES"
        yield return new WaitForSeconds(0.5f);
        if (isGale)
        {
            transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(true);
            foreach(Transform child in transform.GetChild(3).transform.GetChild(0))
            {
                child.GetComponent<Animation>().Play("RotateY");
            }
        }
        else if (isValles)
        {
            transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(true);
            foreach (Transform child in transform.GetChild(3).transform.GetChild(1))
            {
                child.GetComponent<Animation>().Play("RotateY");
            }
        }
        else if (isNoctis)
        {
            transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(true);
            foreach (Transform child in transform.GetChild(3).transform.GetChild(2))
            {
                child.GetComponent<Animation>().Play("RotateY");
            }
        }
        else if (isOlympus)
        {
            transform.GetChild(3).transform.GetChild(3).gameObject.SetActive(true);
            foreach (Transform child in transform.GetChild(3).transform.GetChild(3))
            {
                child.GetComponent<Animation>().Play("RotateY");
            }
        }

        yield return new WaitForSeconds(6);
        if (isGale)
        {
            foreach (Transform child in transform.GetChild(3).transform.GetChild(0))
            {
                child.GetComponent<Animation>().Play("RotateYBack");
            }
            yield return new WaitForSeconds(2);
            transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(false);
            isGale = false;
        }
        else if (isValles)
        {
            foreach (Transform child in transform.GetChild(3).transform.GetChild(1))
            {
                child.GetComponent<Animation>().Play("RotateYBack");
            }
            yield return new WaitForSeconds(2);
            transform.GetChild(3).transform.GetChild(1).gameObject.SetActive(false);
            isValles = false;
        }
        else if (isNoctis)
        {
            foreach (Transform child in transform.GetChild(3).transform.GetChild(2))
            {
                child.GetComponent<Animation>().Play("RotateYBack");
            }
            yield return new WaitForSeconds(2);
            transform.GetChild(3).transform.GetChild(2).gameObject.SetActive(false);
            isNoctis = false;
        }
        else if (isOlympus)
        {
            foreach (Transform child in transform.GetChild(3).transform.GetChild(3))
            {
                child.GetComponent<Animation>().Play("RotateYBack");
            }
            yield return new WaitForSeconds(2);
            transform.GetChild(3).transform.GetChild(3).gameObject.SetActive(false);
            isOlympus = false;
        }
        #endregion

        sceneText.DOFade(0, 4);
        temperatureText.DOFade(0, 4);
        dayText.DOFade(0, 4);
    }

    #region Mission Coroutine
    public IEnumerator MissionInfo(string missionEnabled, string descriptionInfo, int seconds)
    {
        missionTitleText.text = missionEnabled;
        descriptionText.text = descriptionInfo;

        yield return new WaitForSeconds(seconds);
        missionTitleText.GetComponent<Animation>().Play("MoveLeft");
        missionTitleText.DOFade(1, 0.5f);

        yield return new WaitForSeconds(1f);
        descriptionText.GetComponent<Animation>().Play("MoveUp");
        descriptionText.DOFade(1, 1);

        yield return new WaitForSeconds(10);
        missionTitleText.GetComponent<Animation>().Play("MoveRight");
        descriptionText.GetComponent<Animation>().Play("MoveDown");
        missionTitleText.DOFade(0, 1);
        descriptionText.DOFade(0, 0.5f);
    }
    #endregion

    #region Dialogues Coroutine
    public IEnumerator DialoguesInfo()
    {
        yield return new WaitForSeconds(25);
        dialogueBox.GetComponent<Animation>().Play("D_MoveUp");

        if (dialogueValue == 0)
            dialogueText.text = "Marika: *ktztzt * Ehy John, did you know that first recordings of Mars date back to the Ancient Egyptians? They called the planet Her Desher, meaning The Red One. Ancient Greeks nominated it Ares, Romans Mars. In the Eastern Asian cultures it was referred to as the Fire Star while in the Hindu religion it is known as the deity Mangala ";

        else if (dialogueValue == 1)
            dialogueText.text = "Marika: *ktztzt * Ehy John, did you know that Mars was first observed via telescope by Galileo Galilei in 1609? However, to discover its two moons, Phobos and Deimos, nominated after the sons of the Greek God of War Ares, we had to wait until 1877 when the astronomer Asaph Hall found them. ";

        else if (dialogueValue == 2)
            dialogueText.text = "Marika: *ktztzt * Ehy John, did you know that the Mars symbol, is the circle with a small arrow pointing out from behind heading to the top right? It is a stylized representation of a shield and spear used by the Roman God Mars. ";

        else if (dialogueValue == 3)
            dialogueText.text = "Alexander: *ktztzt * Hello my friend, I just wanted to talk to you about Mars, for I think you have noticed it's quite a dust ball. Within hours, due to the wind and other phenomenons, a storm can form and within day it can become a massive, planet-spanning cell that leaves the air fogged with dust for weeks! ";

        else if (dialogueValue == 4)
            dialogueText.text = "Alexander: *ktztzt * Hello my friend, just wanted to share a thought. On Earth there are areas of Mars called 'Spiders', they are terrain formations created every Martian Spring as the terrain erodes during the change of ice caps from ice to vapor. So mind your steps as you might, well, step, on a small spider ahah! ";

        else if (dialogueValue == 5)
            dialogueText.text = "Alexander: *ktztzt * Hello my friend,  there's a thing I'd like to talk about. Did you know that Mars has seasons as well as the Earth? However, each season on the Red Planet lasts twice as long as that on Earth. Here it's almost always very cold for humans, basically just like my hometown. ";

        else if (dialogueValue == 6)
            dialogueText.text = "Alexander: *ktztzt * Hello my friend, I just wanted to tell you how lucky we are. Did you know that until 2016 the Mars missions success rate were no better than 66%? Now we are here! Thinking of the progress we have made in the last decades makes Alexander wander what will come next. ";

        else if (dialogueValue == 7)
            dialogueText.text = "Emma: *ktztzt * Hello darling! Mind a bit of chatter, it gets pretty lonely while working. Have you ever heard of Solar Conjunctions? A Solar conjunction is the period when Earth and Mars are obscured from each other by the Sun itself. When this happens, the communication between spacecraft on Mars and the Earth drastically reduces and a lot of data is lost. This happens regularly every two years for about two weeks. ";

        else if (dialogueValue == 8)
            dialogueText.text = "Emma: *ktztzt * Hello dear! Just a thought that I wanted to share. Did you know that the first spacecraft to Mars was sent from Earth back in 1960? Unfortunately, the mission failed, but the attempt allowed us to learn a lot and from our mistakes we made what we are doing today possible! ";

        else if (dialogueValue == 9)
            dialogueText.text = "Emma: *ktztzt * Hello my dear, mind a second? I was just wondering how incredible is the fact that how even just the prototyping of technology for a Mars mission has influenced the life on Earth. Think of the development of satellites that has aided the life on Earth greatly. That's just an example! ";

        yield return new WaitForSeconds(15);
        dialogueBox.GetComponent<Animation>().Play("D_MoveDown");
    }
    #endregion

    #region Sol Coroutines
    public IEnumerator SolLeftInfo()
    {
        solLeft.text = "Sol 1";

        yield return new WaitForSeconds(8f);
        solLeft.DOFade(1, 4);

        yield return new WaitForSeconds(4f);
        solLeft.DOFade(0, 4);
    }

    // CHIAMARE QUESTA NEL MANAGER DELLE QUEST
    public IEnumerator SolInfo(string name)
    {
        FindObjectOfType<GenericSettings>().LockPlayer();
        yield return new WaitForSeconds(1f);

        fadeSol.DOFade(1, 2);
        solCenter.text = name;

        yield return new WaitForSeconds(3f);
        solCenter.DOFade(1, 4);

        yield return new WaitForSeconds(4f);
        FindObjectOfType<GenericSettings>().UnlockPlayer();
        solCenter.DOFade(0, 2);
        fadeSol.DOFade(0, 4);
    }
    #endregion
}

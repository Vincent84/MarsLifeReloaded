using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.CharacterController;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;


public class RoverManager : MonoBehaviour 
{

    /*[Tooltip("Write the name of the level you want to load")]
    public string levelToLoad;
    [Tooltip("True if you need to spawn the character into a transform location on the scene to load")]
    public bool findSpawnPoint = true;
    [Tooltip("Assign here the spawnPoint name of the scene that you will load")]
    public string spawnPointName;
    private GameObject player;*/

	[Header("Variabili di Stefano Mauri")]
	public HUD hud; 
	public EventSystem eSystem; 
	public GameObject selectedButton;
	public GameObject panel;

	[Header("Debug mode, da disattivare in delivery")]
	public bool isDebug = false;

	public static bool enterTrigger = false;

	#region Private

	private bool isInFade = false;

	#endregion

	void Awake()
	{
		/*if (gameObject.name == "LAND ROVER") 
		{
			eSystem = GameObject.FindGameObjectWithTag ("eSystem").GetComponent<EventSystem> ();
            //eSystem.gameObject.SetActive (false);
            //eSystem.GetComponent<StandaloneInputModule>().enabled = false;
		}*/

		hud = GameObject.Find ("PauseMenu").GetComponent<HUD> ();
        eSystem = GameObject.FindGameObjectWithTag ("eSystem").GetComponent<EventSystem> ();

	}

    void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag.Equals("Player"))
        {



			isInFade = false;
            enterTrigger = true;

			//Attivo il menu del rover
			hud.MoveOnMenu ("RoverMenu");
			//Blocchiamo il menu di pausa
			hud.SetRoverMenu();

			if (isDebug == false) 
			{
				//Controlliamo quali bottoni sono attivi
				hud.CheckAtiveScenes ();
			}

			//Imposto l'eventsystem sul bottone exit del menu di pausa
			hud.ChangeFirstSelected (hud.menu [hud.menu.Length - 1].buttonSelect);

            /*vThirdPersonCamera.instance.lockCamera = true;

            //print(vThirdPersonCamera.instance.currentStateName);

            other.GetComponent<vThirdPersonInput> ().lockInput = true;
            vThirdPersonController.instance.lockSpeed = true;
            vThirdPersonController.instance.lockRotation = true;*/

            vThirdPersonController.instance.GetComponent<GenericSettings>().LockPlayer();

			//eSystem.gameObject.SetActive (true);
            /*eSystem.GetComponent<StandaloneInputModule>().enabled = true;
            eSystem.firstSelectedGameObject = selectedButton;
			eSystem.SetSelectedGameObject(selectedButton);*/

			//panel.SetActive (true);

            //UIManager.instance.ShowScenePanel();

        }
    }

    /*void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {

			enterTrigger = false;

			other.GetComponent<vThirdPersonInput> ().lockInput = false;
            vThirdPersonController.instance.lockSpeed = false;
            vThirdPersonController.instance.lockRotation = false;

            eSystem.gameObject.SetActive(false);

            panel.SetActive (false);

            UIManager.instance.HideScenePanel();

            vThirdPersonCamera.instance.lockCamera = false;

        }
    }*/


    /*public void ChangeScene()
    {

		//enterTrigger = false;

        //player = vThirdPersonController.instance.gameObject;
        player.GetComponent<vThirdPersonInput>().lockInput = false;
        vThirdPersonController.instance.lockSpeed = false;
        vThirdPersonController.instance.lockRotation = false;

        //vThirdPersonController.instance.GetComponent<GenericSettings>().UnlockPlayer();

        //var spawnPointFinderObj = new GameObject("spawnPointFinder");
        //var spawnPointFinder = spawnPointFinderObj.AddComponent<vFindSpawnPoint>();
        //Debug.Log(spawnPointName+" "+gameObject.name);

        
        //spawnPointFinder.AlighObjetToSpawnPoint(player, spawnPointName);

		//hud.MoveOnMenu ("FadeDark");
		//isInFade = true;


        #if UNITY_5_3_OR_NEWER
        SceneManager.LoadScene(levelToLoad);
        #else
        		Application.LoadLevel(levelToLoad);
        #endif
   }*/

	private IEnumerator WaitChangeScene()
	{

		while (true) 
		{


			Debug.Log (hud.panelFade.alpha);

			if (hud.panelFade.alpha >= 1) 
			{

				Debug.Log ("Entrato");

				#if UNITY_5_3_OR_NEWER
				//SceneManager.LoadScene(levelToLoad);
				#else
				Application.LoadLevel(levelToLoad);
				#endif

				yield return null;

			}

			yield return null;

		}

		Debug.Log ("uscito");

		yield return null;

	}
		

}

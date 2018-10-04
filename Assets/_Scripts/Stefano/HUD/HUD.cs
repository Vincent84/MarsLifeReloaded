using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.CharacterController;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Audio;

public class HUD : MonoBehaviour {

	#region Public 

	[Space(10)]
	public ScreenMenu[] menu;

	[Space(10)]
	[Header("Lista bottoni del rover")]
	public ButtonsRover[] buttonsRover;

	[Space(10)]
	public EventSystem eSystem;
	public Animator animPauseMenu;

    [Header("Markers Locations")]
    public Image galeMarker;
    public Image noctisMarker;
    public Image olympusMarker;
    public Image vallesMarker;

	[Header("Marker per alzare o diminuire i volumi")]
	public GameObject mainMusicButton;
	public GameObject SFXbutton;
	public GameObject musicButton;

	public Color32 disableColor;
	public Color32 enableColor;

	[Header("Colore dei bottoni disabilitati nel rover menu")]
	public Color32 enableColorRoverMenu;
	public Color32 disableColorRoverMenu;

    [Header("Lista MAIN VOLUME")]
	public List<Image> listMainVolume;
	public AudioMixer mainAudio;
	public AudioMixer invectorAudio;
	[Header("Lista SFX VOLUME")]
	public List<Image> listSFXVolume;
	public AudioMixer SFXaudio;
	[Header("Lista MUSIC VOLUME")]
	public List<Image> listMusicVolume;
	public AudioMixer musicAudio;

	[Header("Oggetto che contiene questo menu")]
	public GameObject pauseMenu;

	[Header("Panel per il fade")]
	public CanvasGroup panelFade;

	[Header("Variabile per il salvataggio simulato")]
	public float saveTime;

	[Header("Variabili per la Radio/MP3")]
	public Text songText;

	#endregion 

	#region Class

	[Serializable]
	public class ScreenMenu
	{

		[Header("Schermata")]
		[Space(5)]

		public GameObject screen;
		public GameObject buttonSelect;
		public bool isActive;

	}

	[Serializable]
	public class ButtonsRover
	{

		[Space(10)]
		public Button button;
		public Text textButton;
		public string targetScene;
		public GameObject warningImage1;
		public GameObject warningImage2;

	}

	#endregion

	#region Private 

	private float timer = 0.2f;
	private GameObject Player;
	private bool isGamepad = false;
	private bool checkIsGamepad = false;
	private bool menuIsOpen = false;
    private bool codexMenuIsOpen = false;
	private bool roverMenuIsOpen  = false;
	private static string newSceneUnlock = "";

	#endregion

	void Start()
	{

		//newSceneUnlock = "Valles Marineris";

		//Impostiamo il primo bottone illuminato 
		ChangeFirstSelected (CheckJoystick());

		checkIsGamepad = isGamepad;

		/*Player = GameObject.FindGameObjectWithTag ("Player");

		if (Player != null) 
		{

			Player.SetActive (false);

		}*/

		//tempESystem = eSystem.firstSelectedGameObject;

		//Avvio la coroutine per il controllo degli input
		StartCoroutine (CheckInput ());


	}

	void Update()
	{  

        if (codexMenuIsOpen == false)
        {

			// ANIMAZIONI LOCATIONS DEL ROVER
			if (roverMenuIsOpen == true && isGamepad == true) {

				if (eSystem.currentSelectedGameObject.name == "Cratere Gale") {
					galeMarker.transform.localScale = new Vector3 (1.6f, 1.6f, 1);
				} else {
					galeMarker.transform.localScale = new Vector3 (1f, 1f, 1);
				}

				if (eSystem.currentSelectedGameObject.name == "Noctis Labyrinthus") {
					noctisMarker.transform.localScale = new Vector3 (1.6f, 1.6f, 1);
				} else {
					noctisMarker.transform.localScale = new Vector3 (1f, 1f, 1);
				}

				if (eSystem.currentSelectedGameObject.name == "Olympus Mons") {
					olympusMarker.transform.localScale = new Vector3 (1.6f, 1.6f, 1);
				} else {
					olympusMarker.transform.localScale = new Vector3 (1f, 1f, 1);
				}

				if (eSystem.currentSelectedGameObject.name == "Valles Marineris") {
					vallesMarker.transform.localScale = new Vector3 (1.6f, 1.6f, 1);
				} else {
					vallesMarker.transform.localScale = new Vector3 (1f, 1f, 1);
				}

			} else {

				//RESETTO I MARKER

				galeMarker.transform.localScale = new Vector3 (1f, 1f, 1);
				noctisMarker.transform.localScale = new Vector3 (1f, 1f, 1);
				olympusMarker.transform.localScale = new Vector3 (1f, 1f, 1);
				vallesMarker.transform.localScale = new Vector3 (1f, 1f, 1);

			}

            if (isGamepad == false)
            {

                /*if (Input.GetKeyDown(KeyCode.Escape) == true && menuIsOpen == true && roverMenuIsOpen == false)
                {

                    bool canIclose = false;

                    for (int i = 0; i < menu.Length; i++)
                    {

                        if (menu[i].isActive == true && menu[i].screen == menu[0].screen)
                        {

                            canIclose = true;

                        }
                        else if (menu[i].isActive == true)
                        {

                            canIclose = false;

                        }

                    }


                    if (canIclose == true)
                    {
                        MoveOnMenu("ExitPauseMenu");
                        SetCloseMenu();
                    }

                }*/


            }
            else
            {

                /*if (InputManager.StartButton() == true && menuIsOpen == true && roverMenuIsOpen == false)
                {

                    bool canIclose = false;

                    for (int i = 0; i < menu.Length; i++)
                    {

                        if (menu[i].isActive == true && menu[i].screen == menu[0].screen)
                        {

                            canIclose = true;

                        }
                        else if (menu[i].isActive == true)
                        {

                            canIclose = false;

                        }

                    }


                    if (canIclose == true)
                    {
                        MoveOnMenu("ExitPauseMenu");
                        SetCloseMenu();
                    }

                }*/

            }

            //Se il tasto start viene premeuto avviamo il menu 
			if (InputManager.StartButton() == true && menuIsOpen == false && roverMenuIsOpen == false && UIManager.instance.dialoguePanel.activeSelf == false && UIManager.instance.infoPanel.activeSelf == false)
            {
				
                MoveOnMenu("PasueMenu_new");
                menuIsOpen = true;
				ChangeFirstSelected(menu[0].buttonSelect);
                vThirdPersonController.instance.GetComponent<GenericSettings>().LockPlayer();

            }

            if (isGamepad == true && menuIsOpen == true && roverMenuIsOpen == false)
            {

                #region SettingAudio

                //Controllo audio per il menu principale 
                if (timer >= 0.08f && isGamepad == true && eSystem.currentSelectedGameObject != null)
                {

                    timer = 0;

                    #region MainAudio

                    if (eSystem.currentSelectedGameObject.GetHashCode() == mainMusicButton.GetHashCode() && InputManager.MainHorizontal() < -0.5)
                    {

                        //Tolgo volume
                        DecreaseVolume(listMainVolume, mainAudio);
                        //this.GetComponent<Musica> ().RiproduciSuono (4);

                    }
                    else if (eSystem.currentSelectedGameObject.GetHashCode() == mainMusicButton.GetHashCode() && InputManager.MainHorizontal() > 0.5)
                    {

                        //Tolgo volume
                        EncreaseVolume(listMainVolume, mainAudio);
                        //this.GetComponent<Musica> ().RiproduciSuono (4);

                    }

                    #endregion

                    #region SFXaudio

                    if (eSystem.currentSelectedGameObject.GetHashCode() == SFXbutton.GetHashCode() && InputManager.MainHorizontal() < -0.5)
                    {

                        //Tolgo volume
                        DecreaseVolume(listSFXVolume, SFXaudio);
                        //this.GetComponent<Musica> ().RiproduciSuono (4);

                    }
                    else if (eSystem.currentSelectedGameObject.GetHashCode() == SFXbutton.GetHashCode() && InputManager.MainHorizontal() > 0.5)
                    {

                        //Tolgo volume
                        EncreaseVolume(listSFXVolume, SFXaudio);
                        //this.GetComponent<Musica> ().RiproduciSuono (4);

                    }

                    #endregion

                    #region MusicAudio

                    if (eSystem.currentSelectedGameObject.GetHashCode() == musicButton.GetHashCode() && InputManager.MainHorizontal() < -0.5)
                    {

                        //Tolgo volume
                        DecreaseVolume(listMusicVolume, musicAudio);
                        //this.GetComponent<Musica> ().RiproduciSuono (4);

                    }
                    else if (eSystem.currentSelectedGameObject.GetHashCode() == musicButton.GetHashCode() && InputManager.MainHorizontal() > 0.5)
                    {

                        //Tolgo volume
                        EncreaseVolume(listMusicVolume, musicAudio);
                        //this.GetComponent<Musica> ().RiproduciSuono (4);

                    }

                    #endregion

                }
                else
                {

                    timer += Time.deltaTime;

                }

                #endregion

            }

           /* //Disabilito i punti esclamativi
            if (newSceneUnlock != "" && roverMenuIsOpen == true)
            {

                for (int i = 0; i < buttonsRover.Length; i++)
                {

                    /*if (buttonsRover [i].targetScene == newSceneUnlock && buttonsRover[i].button.name == eSystem.currentSelectedGameObject.name) 
				    {

					    newSceneUnlock = "";
					    buttonsRover [i].warningImage1.SetActive (false);
					    buttonsRover [i].warningImage2.SetActive (false);

				    }

                }

            }*/
        }
	}

	#region Volume

	/// <summary>
	/// Metodo che muta gli audio
	/// </summary>
	/// <param name="nameList">Name list.</param>
	public void Mute(string nameList)
	{

		if (nameList == "Music") 
		{

			for (int i = 0; i < listMusicVolume.Count; i++)
			{

				listMusicVolume [i].color = disableColor;

			}

			//musicAudio.volume = 0;
			musicAudio.SetFloat("Volume", -80f);

			Debug.Log ("Music mute");

		} 
		else if (nameList == "SFX")
		{

			for (int i = 0; i < listSFXVolume.Count; i++)
			{

				listSFXVolume [i].color = disableColor;

			}


			//SFXaudio.volume = 0;
			SFXaudio.SetFloat("Volume", -80f);

			Debug.Log ("SFX mute");

		} 
		else if (nameList == "Main") 
		{

			for (int i = 0; i < listMainVolume.Count; i++) 
			{

				listMainVolume [i].color = disableColor;

			}

			//mainAudio.volume = 0;
			mainAudio.SetFloat("Volume", -80f);
			invectorAudio.SetFloat ("Volume", -80f);

			Debug.Log("Main mute");

		}


	}

	#region Mouse

	/// <summary>
	/// Metodo che aumenta il volume con il mouse
	/// </summary>
	/// <param name="button">Button.</param>
	public void ChangeVolumeMain(GameObject button)
	{

		string name = button.name;
		float v = 0f;

		for (int i = 0; i < listMainVolume.Count; i++) 
		{

			listMainVolume [i].color = disableColor;

			if (listMainVolume [i].name == name) 
			{

				//Ciclo che attiva i colori
				for (int j = 0; j <= i; j++) 
				{

					v += 4f;
					listMainVolume [j].color = enableColor;

				}

			}

			v -= 4f;

		}

		//Cambio effettio del volume
		//mainAudio.volume = v;
		mainAudio.SetFloat("Volume", v);
		invectorAudio.SetFloat ("Volume", v);

	}

	/// <summary>
	/// Metodo che aumenta il volume con il mouse
	/// </summary>
	/// <param name="button">Button.</param>
	public void ChangeVolumeMusic(GameObject button)
	{

		string name = button.name;

		float v = 0f;

		for (int i = 0; i < listMusicVolume.Count; i++) 
		{

			listMusicVolume [i].color = disableColor;

			if (listMusicVolume [i].name == name) 
			{

				//Ciclo che attiva i colori
				for (int j = 0; j <= i; j++) 
				{

					v += 4f;
					listMusicVolume [j].color = enableColor;

				}

			}

			v -= 4f;

		}

		if (SoundManager.isRadio == true) 
		{
			//musicAudio.volume = v;
			musicAudio.SetFloat ("Volume", v);
		}

	}

	/// <summary>
	/// Metodo che aumenta il volume con il mouse
	/// </summary>
	/// <param name="button">Button.</param>
	public void ChangeVolumeSFX(GameObject button)
	{

		string name = button.name;

		float v = 0f;

		for (int i = 0; i < listSFXVolume.Count; i++) 
		{

			listSFXVolume [i].color = disableColor;

			if (listSFXVolume [i].name == name) 
			{

				//Ciclo che attiva i colori
				for (int j = 0; j <= i; j++) 
				{

					v += 4f;	

					listSFXVolume [j].color = enableColor;

				}

			}

			v -= 4f;

		}

		//SFXaudio.volume = v;
		SFXaudio.SetFloat("Volume", v);

	}

	#endregion

	#region Joystic


	/// <summary>
	/// Metodo che aumenta il volume del gioco
	/// </summary>
	public void EncreaseVolume( List<Image> list, AudioMixer audio)
	{

		float v = -40f;

		for (int i = 0; i < list.Count; i++) 
		{

			if (list [i].color == disableColor) 
			{

				if (i == 0) 
				{

					//audio.volume += 0.1f;
					audio.GetFloat ("Volume", out v);
					v += 44f;
					audio.SetFloat ("Volume", v);

					if (audio.name == "mainVolume") 
					{

						invectorAudio.SetFloat ("Volume", v);

					}

					list [i].color = enableColor;
					return;

				} 
				else {
					//audio.volume += 0.1f;
					audio.GetFloat ("Volume", out v);
					v += 4f;
					audio.SetFloat ("Volume", v);

					if (audio.name == "mainVolume") 
					{

						invectorAudio.SetFloat ("Volume", v);

					}

					list [i].color = enableColor;
					return;

				}

			}

		}

	}

	/// <summary>
	/// Metodo che diminuisce il volume di gioco
	/// </summary>
	public void DecreaseVolume( List<Image> list, AudioMixer audio)
	{

		float v = 0f;

		for (int i = 1; i <= list.Count; i++) 
		{

			if (list [list.Count-i].color == enableColor) 
			{

				//Debug.Log (i);

				if (i == 10) 
				{

					//audio.volume -= 0.1f;
					audio.GetFloat ("Volume", out v);
					v -= 44f;
					audio.SetFloat ("Volume", v);

					if (audio.name == "mainVolume") 
					{

						invectorAudio.SetFloat ("Volume", v);

					}

					list [list.Count - i].color = disableColor;
					return;

				}
				else
				{

					//audio.volume -= 0.1f;
					audio.GetFloat ("Volume", out v);
					v -= 4f;
					audio.SetFloat ("Volume", v);

					if (audio.name == "mainVolume") 
					{

						invectorAudio.SetFloat ("Volume", v);

					}

					list [list.Count - i].color = disableColor;
					return;


				}

			}

		}
	}


	#endregion

	#endregion

	#region Animazioni

	/// <summary>
	/// Metodo che ti muovi nel menu
	/// </summa
	public void MoveOnMenu(string value)
	{

		animPauseMenu.Play (value);

	}

	//Corotuine per emulare il salvataggio
	public void AnimationSavingData()
	{

		StartCoroutine (SavingDataEmulator());
	
	}

	private IEnumerator SavingDataEmulator()
	{

		//Non permetto di premere pulsanti 
		eSystem.SetSelectedGameObject(null);

		MoveOnMenu ("SavingIn");

		float timer = 0f;

		yield return new WaitForSeconds (saveTime);
		
		gameObject.GetComponent<SaveMenu> ().LoadSlotInfo ();
		//Torniamo alla schermata della scelta degli slot
		MoveOnMenu ("GoChose_Return");
		//Rimetto il puntatore al bottone corretto
		ChangeFirstSelected (isGamepad);

		yield return null;
	}

	#endregion

	#region EventSystem

	/// <summary>
	/// Cambiare il first select dell'Event System
	/// </summary>
	/// <param name="button">Button.</param>
	/// <param name="isGamepad">If set to <c>true</c> is gamepad.</param>
	public void ChangeFirstSelected(bool isGamepad)
	{

		if (isGamepad == true) 
		{

			mainMusicButton.GetComponent<Button>().enabled = true;
			SFXbutton.GetComponent<Button>().enabled = true;
			musicButton.GetComponent<Button>().enabled = true;

			for (int i = 0; i < menu.Length; i++) 
			{

				if (menu [i].isActive == true) 
				{

					eSystem.firstSelectedGameObject = menu[i].buttonSelect;
					eSystem.SetSelectedGameObject (menu [i].buttonSelect);
					//return;

				}

			}

		} 
		else 
		{
			
			mainMusicButton.GetComponent<Button>().enabled = false;
			SFXbutton.GetComponent<Button>().enabled = false;
			musicButton.GetComponent<Button>().enabled = false;
			eSystem.firstSelectedGameObject = null;
			eSystem.SetSelectedGameObject (null);

		}



	}

	/// <summary>
	/// Cambiare il first select dell'Event System
	/// </summary>
	/// <param name="button">Button.</param>
	public void ChangeFirstSelected(GameObject button)
	{


		if (isGamepad == true) 
		{

			for (int i = 0; i < menu.Length; i++) {

				if (menu [i].screen.activeSelf == true) {

					eSystem.firstSelectedGameObject = null;
					eSystem.SetSelectedGameObject (null);

					eSystem.firstSelectedGameObject = button;
					eSystem.SetSelectedGameObject (button);
					return;

				}

			}
		}

	}

	/// <summary>
	/// Metodo da eseguire nel trigger del rover per capire se il menu è attivo oppure no
	/// </summary>
	public void SetRoverMenu()
	{
		if (roverMenuIsOpen == false) 
		{
			roverMenuIsOpen = true;
		} 
		else 
		{

			roverMenuIsOpen = false;

		}
	}

	#endregion

	#region MenuFunction

	/// <summary>
	/// Metodo che ritorna al menu principale
	/// </summary>
	public void TurnToMainMenu()
	{

		SceneManager.LoadScene ("_UI_Menu_Stefano");

	}

	/// <summary>
	/// Coroutine che controlla il cambio di input del gioco
	/// </summary>
	/// <returns>The input.</returns>
	public IEnumerator CheckInput()
	{

		while (true) 
		{

			//Controllo ogni quanto di tempo se il Joystick o la tastiera sono stati inseriti
			isGamepad = CheckJoystick ();

			if (isGamepad != checkIsGamepad) 
			{

				//Controllo se stiamo aprendo il menu di pausa
				if (menuIsOpen == true && roverMenuIsOpen == false) 
				{

					//Dico che stiamo aprendo il menu di pausa ordinario
					menu [0].isActive = true;
					menu [menu.Length - 1].isActive = false;

				} 
				else 
				{
					//Dico che stiamo aprendo il menu di rover
					menu [menu.Length - 1].isActive = true;
					menu [0].isActive = false;

				}

				ChangeFirstSelected (isGamepad);
				checkIsGamepad = isGamepad;
			} 
			else 
			{

				if (eSystem.currentSelectedGameObject == false) {

					ChangeFirstSelected (isGamepad);

				}

			}

			yield return new WaitForSeconds (1f);

		}

		yield return null;


	}

	/// <summary>
	/// Controllo se è inserito un Joystick
	/// </summary>
	public bool CheckJoystick()
	{

		bool gamepad = false;

		for (int i = 0; i < Input.GetJoystickNames ().Length; i++) 
		{

			if (Input.GetJoystickNames().GetValue(i) != "") 
			{

				gamepad = true;

			}

		}

		if (gamepad == false) 
		{

			//Debug.Log ("Nessun controller inserito");
			isGamepad = false;

			//Riabilito gli input da Mouse
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;

			return false;

		} 
		else
		{

			//Debug.Log ("Controller inserito");
			isGamepad = true;

			//Disabilitiamo gli input da Mouse
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;


			return true;

		}

	}

	/// <summary>
	/// Metodo che aggiorna la scena attiva corrente nella lista menu
	/// </summary>
	/// <param name="nextScene">Next scene.</param>
	public void CurrentMenu(GameObject nextScene)
	{

		for (int i = 0; i < menu.Length; i++) 
		{

			if (menu [i].screen == nextScene) 
			{

				menu [i].isActive = true;

			} 
			else 
			{

				menu [i].isActive = false;	

			}

		}

	}

	/// <summary>
	/// Metodo che indica al sistema che il menu ora è chiuso
	/// </summary>
	public void SetCloseMenu()
	{

		menuIsOpen = false;
        vThirdPersonController.instance.GetComponent<GenericSettings>().UnlockPlayer();

	}

    public void SetCodexMenuIsOpen(bool value)
    {

        codexMenuIsOpen = value;

    }

    public bool GetCodexMenuIsOpen()
    {

        return codexMenuIsOpen;

    }

    public bool GetMenuIsOpen()
    {

        return menuIsOpen;

    }

    #endregion

    #region MenuRover

    /// <summary>
    /// Controllo le scene che sono raggiungibili
    /// </summary>
    public void CheckAtiveScenes()
	{
		
		for (int i = 0; i < Database.scenes.Count; i++) 
		{

			for (int j = 0; j < buttonsRover.Length; j++) 
			{
						
				//Se trovo la scena di riferimento al bottone lo setto al valore di locking della scena stessa
				if (buttonsRover [j].targetScene == Database.scenes [i].sceneName) 
				{
					
					buttonsRover [j].button.enabled = Database.scenes [i].isUnlocked;

					if (Database.scenes [i].isUnlocked == false) {

						buttonsRover [j].textButton.color = disableColorRoverMenu;

					}
					else 
					{

						buttonsRover [j].textButton.color = enableColorRoverMenu;

					}

					//controllo se è una scena sbloccata nuova
					/*if (newSceneUnlock == buttonsRover [j].targetScene) 
					{
						
						buttonsRover [j].warningImage1.SetActive (true);
						buttonsRover [j].warningImage2.SetActive (true);

                        print(newSceneUnlock);

					}*/

					Debug.Log (buttonsRover [j].targetScene + " = " +Database.scenes [i].isUnlocked);

				}

			}

		}
			
	}

	/// <summary>
	/// Metodo che disabilitano i warning se attivi
	/// </summary>
	public void DisableWarningImage()
	{

		for (int i = 0; i < buttonsRover.Length; i++) 
		{

			if (buttonsRover [i].button.name == gameObject.name && buttonsRover [i].warningImage1.activeSelf == true) 
			{

				buttonsRover [i].warningImage1.SetActive (false);
				buttonsRover [i].warningImage1.SetActive (false);

			}

		}

		//Resetto il valore della scena nuova
		newSceneUnlock = "";

	}

	public void ExitRoverMenu()
	{

		RoverManager.enterTrigger = false;

		/*vThirdPersonController.instance.GetComponent<vThirdPersonInput>().lockInput = false;
        vThirdPersonController.instance.lockSpeed = false;
        vThirdPersonController.instance.lockRotation = false;*/

		//eSystem.gameObject.SetActive(false);

		//Disabilitiamo il menu del rover
		MoveOnMenu ("RoverMenu_Return");

        eSystem.SetSelectedGameObject(null);

		//Diamo la possibilità al giocatore di utilizzare il menu di pausa
		SetRoverMenu ();

		//panel.SetActive(false);

		//UIManager.instance.HideScenePanel();

		//vThirdPersonCamera.instance.lockCamera = false;
		vThirdPersonController.instance.GetComponent<GenericSettings>().UnlockPlayer();


	}

	public static void SetNewSceneUnlock(string nameScene)
	{

		newSceneUnlock = nameScene;

	}
		
	public bool GetMenuRoverIsOpen()
	{

		return roverMenuIsOpen;

	}

	#endregion

	#region Radio

	/// <summary>
	/// Metodo che cambia il nome del testo della canzone corrente 
	/// </summary>
	public void ChangeTextSong(string newSong)
	{

		songText.text = newSong;

	}

	#endregion

	#region Old

	/*

	#region Public 

	public Animator anim;
	public GameObject player;
	public GameObject firstButton;

	public EventSystem eSystem;

	[Header("Marker per alzare o diminuire i volumi")]
	public GameObject mainMuiscButton;
	public GameObject SFXbutton;
	public GameObject musicButton;

	public Color32 disableColor;
	public Color32 enableColor;

	[Header("Lista MAIN VOLUME")]
	public List<Image> listMainVolume;
	public AudioSource mainAudio;
	[Header("Lista SFX VOLUME")]
	public List<Image> listSFXVolume;
	public AudioSource SFXaudio;
	[Header("Lista MUSIC VOLUME")]
	public List<Image> listMusicVolume;
	public AudioSource musicAudio;

	#endregion 

	#region Private

	private bool semaforo = true;
	private float timer = 0.2f;
	private GameObject tempESystem;

	#endregion


	void Awake()
	{

		tempESystem = eSystem.firstSelectedGameObject;

	}

	void Update()
	{

        if(InputManager.StartButton() == true && RoverManager.enterTrigger == false)
        {

            eSystem.SetSelectedGameObject(firstButton);


		    if (player.GetComponent<Invector.CharacterController.vThirdPersonController> ().isGrounded == true && semaforo == true && InputManager.MainHorizontal() == 0 && InputManager.MainVertical() == 0) 
		    {

			    semaforo = false;
			    eSystem.gameObject.SetActive (true);
			    OpenMenu ();
			    player.GetComponent<Invector.CharacterController.vThirdPersonInput> ().enabled = false;

		    } 
		    else if (semaforo == false && InputManager.StartButton () == true) 
		    {

			    semaforo = true;
			    CloseMenu ();
			    player.GetComponent<Invector.CharacterController.vThirdPersonInput> ().enabled = true;
			    eSystem.gameObject.SetActive (false);

		    }

        }

        //Pezza
        if (timer >= 0.2f && semaforo == false) 
		{

			timer = 0;

			if (eSystem.currentSelectedGameObject.GetHashCode () == mainMuiscButton.GetHashCode () && InputManager.MainHorizontal () < 0) {

				//Tolgo volume
				DecreaseVolume (listMainVolume, mainAudio);
				this.GetComponent<Musica> ().RiproduciSuono (4);

			} else if (eSystem.currentSelectedGameObject.GetHashCode () == mainMuiscButton.GetHashCode () && InputManager.MainHorizontal () > 0) {

				//Tolgo volume
				EncreaseVolume (listMainVolume, mainAudio);
				this.GetComponent<Musica> ().RiproduciSuono (4);

			}

			if (eSystem.currentSelectedGameObject.GetHashCode () == SFXbutton.GetHashCode () && InputManager.MainHorizontal () < 0) {

				//Tolgo volume
				DecreaseVolume (listSFXVolume, SFXaudio);
				this.GetComponent<Musica> ().RiproduciSuono (4);

			} else if (eSystem.currentSelectedGameObject.GetHashCode () == SFXbutton.GetHashCode () && InputManager.MainHorizontal () > 0) {

				//Tolgo volume
				EncreaseVolume (listSFXVolume, SFXaudio);
				this.GetComponent<Musica> ().RiproduciSuono (4);

			}

			if (eSystem.currentSelectedGameObject.GetHashCode () == musicButton.GetHashCode () && InputManager.MainHorizontal () < 0) {

				//Tolgo volume
				DecreaseVolume (listMusicVolume, musicAudio);
				this.GetComponent<Musica> ().RiproduciSuono (4);

			} else if (eSystem.currentSelectedGameObject.GetHashCode () == musicButton.GetHashCode () && InputManager.MainHorizontal () > 0) {

				//Tolgo volume
				EncreaseVolume (listMusicVolume, musicAudio);
				this.GetComponent<Musica> ().RiproduciSuono (4);

			}

		} 
		else 
		{

			timer += Time.deltaTime;

		}

		if (semaforo == false) {

			if (eSystem.currentSelectedGameObject.GetHashCode () != tempESystem.GetHashCode () && InputManager.MainVertical () != 0) {

				tempESystem = eSystem.currentSelectedGameObject;
				this.GetComponent<Musica> ().RiproduciSuono (1);

			}
		}
	}

	#region Animazioni

	/// <summary>
	/// OMetodo che apre il menu
	/// </summary>
	public void OpenMenu()
	{

		anim.Play("Move");

	}

	/// <summary>
	/// Metodo che chiude il menu	
	/// </summary>
	public void CloseMenu()
	{

		anim.Play ("MoveReturn");

	}

	/// <summary>
	/// Metodo che ti muovi nel menu
	/// </summa
	public void MoveOnMenu(string value)
	{

		anim.Play (value);

	}

	#endregion

	#region Manager

	public void ResumeGame()
	{

		semaforo = true;
		CloseMenu ();
		player.GetComponent<Invector.CharacterController.vThirdPersonInput> ().enabled = true;
		eSystem.GetComponent<EventSystem> ().SetSelectedGameObject (firstButton);
		eSystem.gameObject.SetActive (false);

	}

	public void TurnToMainMenu()
	{

		SceneManager.LoadScene ("_UI_Menu_Stefano");

	}

	public void Quit()
	{

		Application.Quit ();

	}

	#endregion

	#region Volume

	/// <summary>
	/// Metodo che aumenta il volume del gioco
	/// </summary>
	public void EncreaseVolume( List<Image> list, AudioSource audio)
	{

		for (int i = 0; i < list.Count; i++) 
		{

			if (list [i].color == disableColor) 
			{

				audio.volume += 0.1f;
				list [i].color = enableColor;
				return;

			}

		}

	}

	/// <summary>
	/// Metodo che diminuisce il volume di gioco
	/// </summary>
	public void DecreaseVolume( List<Image> list, AudioSource audio)
	{

		for (int i = 1; i <= list.Count; i++) 
		{

			if (list [list.Count-i].color == enableColor) 
			{

				audio.volume -= 0.1f;
				list [list.Count-i].color = disableColor;
				return;

			}

		}

	}

	#endregion

	*/

	#endregion
	
}

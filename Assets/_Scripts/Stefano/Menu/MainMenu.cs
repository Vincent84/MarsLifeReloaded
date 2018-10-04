using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour 
{

	#region Public 

	[Space(10)]
	public ScreenMenu[] menu;

	public CanvasGroup background;

	public GameObject firstButton;
	public EventSystem eSystem;
	public Animator anim;
	public Animator mainMenu;
	public Animator doorUp;
	public Animator doorDown;

	[Header("Marker per alzare o diminuire i volumi")]
	public GameObject mainMusicButton;
	public GameObject SFXbutton;
	public GameObject musicButton;

	public Color32 disableColor;
	public Color32 enableColor;

	[Header("Lista MAIN VOLUME")]
	public List<Image> listMainVolume;
	public AudioMixer mainAudio;
	[Header("Lista SFX VOLUME")]
	public List<Image> listSFXVolume;
	public AudioMixer SFXaudio;
	[Header("Lista MUSIC VOLUME")]
	public List<Image> listMusicVolume;
	public AudioMixer musicAudio;

	#endregion 

	#region Private 

	//Pezza
	private float timer = 0.2f;
	private GameObject tempESystem;
	private GameObject Player;
	private bool isGamepad = false;
	private bool checkIsGamepad = false;

	#endregion

	[Serializable]
	public class ScreenMenu
	{

		[Header("Schermata")]
		[Space(5)]

		public GameObject screen;
		public GameObject buttonSelect;
		public bool isActive;

	}

	void Awake()
	{

		//Impostiamo il primo bottone illuminato 
		ChangeFirstSelected (CheckJoystick());

		checkIsGamepad = isGamepad;

		Player = GameObject.FindGameObjectWithTag ("Player");
        QuestManager questManager = FindObjectOfType<QuestManager>();
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (Player != null) 
		{
            Destroy(Player);
            Destroy(questManager.gameObject);
            Destroy(gameManager.gameObject);
            Destroy(Invector.vGameController.instance.gameObject);
			//Player.SetActive (false);

		}

		//tempESystem = eSystem.firstSelectedGameObject;

		//Avvio la coroutine per il controllo degli input
		StartCoroutine (CheckInput ());


	}

	void Update()
	{

		if (isGamepad == true) 
		{

			#region SettingAudio

			//Controllo audio per il menu principale 
			if (timer >= 0.2f) {

				timer = 0;

				if(eSystem.currentSelectedGameObject != null)
				{

					#region MainAudio

				

					if (eSystem.currentSelectedGameObject.GetHashCode () == mainMusicButton.GetHashCode () && InputManager.MainHorizontal () < 0) {

						//Tolgo volume
						DecreaseVolume (listMainVolume, mainAudio);
						//this.GetComponent<Musica> ().RiproduciSuono (4);

					} else if (eSystem.currentSelectedGameObject.GetHashCode () == mainMusicButton.GetHashCode () && InputManager.MainHorizontal () > 0) {
						
						//Tolgo volume
						EncreaseVolume (listMainVolume, mainAudio);
						//this.GetComponent<Musica> ().RiproduciSuono (4);

					}
							

					#endregion

					#region SFXaudio

					if (eSystem.currentSelectedGameObject.GetHashCode () == SFXbutton.GetHashCode () && InputManager.MainHorizontal () < 0) {

						//Tolgo volume
						DecreaseVolume (listSFXVolume, SFXaudio);
						//this.GetComponent<Musica> ().RiproduciSuono (4);

					} else if (eSystem.currentSelectedGameObject.GetHashCode () == SFXbutton.GetHashCode () && InputManager.MainHorizontal () > 0) {
						
						//Tolgo volume
						EncreaseVolume (listSFXVolume, SFXaudio);
						//this.GetComponent<Musica> ().RiproduciSuono (4);

					}

					#endregion

					#region MusicAudio

					if (eSystem.currentSelectedGameObject.GetHashCode () == musicButton.GetHashCode () && InputManager.MainHorizontal () < 0) {

						//Tolgo volume
						DecreaseVolume (listMusicVolume, musicAudio);
						//this.GetComponent<Musica> ().RiproduciSuono (4);

					} else if (eSystem.currentSelectedGameObject.GetHashCode () == musicButton.GetHashCode () && InputManager.MainHorizontal () > 0) {
						
						//Tolgo volume
						EncreaseVolume (listMusicVolume, musicAudio);
						//this.GetComponent<Musica> ().RiproduciSuono (4);

					}

					#endregion

				}

			}
			else
			{

				timer += Time.deltaTime;

			}

			#endregion

		}

		/*if (isGamepad == true) 
		{

			#region MusicOnMenu

			if (eSystem.currentSelectedGameObject.GetHashCode () != tempESystem.GetHashCode () && InputManager.MainVertical () != 0) {

				tempESystem = eSystem.currentSelectedGameObject;
				this.GetComponent<Musica> ().RiproduciSuono (1);

			}

			#endregion

		}*/

		if (isGamepad == true) 
		{

			#region SettingAudio

			//Controllo audio per il menu principale 
			if (timer >= 0.08f && isGamepad == true && eSystem.currentSelectedGameObject != null) 
			{

				timer = 0;

				#region MainAudio

				if (eSystem.currentSelectedGameObject.GetHashCode () == mainMusicButton.GetHashCode () && InputManager.MainHorizontal () < -0.5) {

					//Tolgo volume
					DecreaseVolume (listMainVolume, mainAudio);
					//this.GetComponent<Musica> ().RiproduciSuono (4);

				} else if (eSystem.currentSelectedGameObject.GetHashCode () == mainMusicButton.GetHashCode () && InputManager.MainHorizontal () > 0.5) {

					//Tolgo volume
					EncreaseVolume (listMainVolume, mainAudio);
					//this.GetComponent<Musica> ().RiproduciSuono (4);

				}

				#endregion

				#region SFXaudio

				if (eSystem.currentSelectedGameObject.GetHashCode () == SFXbutton.GetHashCode () && InputManager.MainHorizontal () < -0.5) {

					//Tolgo volume
					DecreaseVolume (listSFXVolume, SFXaudio);
					//this.GetComponent<Musica> ().RiproduciSuono (4);

				} else if (eSystem.currentSelectedGameObject.GetHashCode () == SFXbutton.GetHashCode () && InputManager.MainHorizontal () > 0.5) {

					//Tolgo volume
					EncreaseVolume (listSFXVolume, SFXaudio);
					//this.GetComponent<Musica> ().RiproduciSuono (4);

				}

				#endregion

				#region MusicAudio

				if (eSystem.currentSelectedGameObject.GetHashCode () == musicButton.GetHashCode () && InputManager.MainHorizontal () < -0.5) {

					//Tolgo volume
					DecreaseVolume (listMusicVolume, musicAudio);
					//this.GetComponent<Musica> ().RiproduciSuono (4);

				} else if (eSystem.currentSelectedGameObject.GetHashCode () == musicButton.GetHashCode () && InputManager.MainHorizontal () > 0.5) {

					//Tolgo volume
					EncreaseVolume (listMusicVolume, musicAudio);
					//this.GetComponent<Musica> ().RiproduciSuono (4);

				}

				#endregion

				#endregion

			} else {

				timer += Time.deltaTime;

			}

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

			Debug.Log("Main mute");

		}


	}

	//Collegare anche il mixer del di Invector

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

		//musicAudio.volume = v;
		musicAudio.SetFloat("Volume", v);

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
					list [i].color = enableColor;
					return;

				} 
				else {
					//audio.volume += 0.1f;
					audio.GetFloat ("Volume", out v);
					v += 4f;
					audio.SetFloat ("Volume", v);
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
					list [list.Count - i].color = disableColor;
					return;

				}
				else
				{

					//audio.volume -= 0.1f;
					audio.GetFloat ("Volume", out v);
					v -= 4f;
					audio.SetFloat ("Volume", v);
					list [list.Count - i].color = disableColor;
					return;


				}

			}

		}

	}

	#endregion

	#endregion

	#region Joystick

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

	#endregion

	#region Animazioni

	/// <summary>
	/// Metodo che gestisce l'animator della main camera
	/// </summary>
	/// <param name="value">Value.</param>
	public void PlayAnimationCamera(string value)
	{

		anim.Play(value);

	}

	/// <summary>
	/// Metodo che permette di gestire l'animator del main menu
	/// </summary>
	/// <param name="value">Value.</param>
	public void PlayAnimationMainMenu(string value)
	{

		mainMenu.Play (value);

	}

	/// <summary>
	/// Metodo che attiva il movimento della porta
	/// </summary>
	public void PlayAnimationDoorUp()
	{

		doorUp.SetTrigger("UpOpen");

	}

	/// <summary>
	/// Metodo che attiva il movimento della porta
	/// </summary>
	public void PlayAnimationDoorDown()
	{

		doorDown.SetTrigger("DownOpen");

	}

	#endregion

	#region EventSystem

	/// <summary>
	/// Cambiare il first select dell'Event System
	/// </summary>
	/// <param name="button">Button.</param>
	/// <param name="isGamepad">If set to <c>true</c> is gamepad.</param>
	public void ChangeFirstSelected(GameObject button, bool isGamepad)
	{

		if (isGamepad == true) 
		{

			mainMusicButton.GetComponent<Button>().enabled = true;
			SFXbutton.GetComponent<Button>().enabled = true;
			musicButton.GetComponent<Button>().enabled = true;
			eSystem.firstSelectedGameObject = button;

		} 
		else 
		{
			mainMusicButton.GetComponent<Button>().enabled = false;
			SFXbutton.GetComponent<Button>().enabled = false;
			musicButton.GetComponent<Button>().enabled = false;
			eSystem.firstSelectedGameObject = null;
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
					return;

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
	/// Metodo che ad ogni cambio di schemrata seleziona cambia il bottone di riferimento del Joystick
	/// </summary>
	/// <param name="newButton">New button.</param>
	public void ChangeFisrtButton(GameObject newButton)
	{


		firstButton = newButton;

	}



	#endregion

	#region MenuFunction

	/// <summary>
	/// Metodo per uscire dal gioco
	/// </summary>
	public void Exit()
	{

		Application.Quit ();

	}

	/// <summary>
	/// Metodo che avvia la coroutine per cambiare scena
	/// </summary>
	/// <param name="nameScena">Name scena.</param>
	public void ChangeScena(string nameScena)
	{

		//Se il file esiste allora carico la partita 
		if (ES2.Exists (PlayerPrefs.GetString ("Slot") + ".txt")) 
		{
			
			StartCoroutine (FadeScena (ES2.Load<string> (PlayerPrefs.GetString ("Slot") + ".txt?tag=currentScene")));

		} 
		else //vuol dire che si vuole creare una nuova partita
		{

			PlayerPrefs.SetInt ("isFirstTime", 0);
			StartCoroutine (FadeScena (nameScena));

		}

	}

	/// <summary>
	/// Metodo che avvia la coroutine per cambiare scena
	/// </summary>
	/// <param name="nameScena">Name scena.</param>
	public void ChangeScena(int index)
	{
		//Converto il numero di scena in nome
		string nameScena = SceneManager.GetSceneByBuildIndex (index).name;

		//Se il file esiste allora carico la partita 
		if (ES2.Exists (PlayerPrefs.GetString ("Slot") + ".txt")) 
		{

			StartCoroutine (FadeScena (ES2.Load<string> (PlayerPrefs.GetString ("Slot") + ".txt?tag=currentScene")));

		} 
		else //vuol dire che si vuole creare una nuova partita
		{

			PlayerPrefs.SetInt ("isFirstTime", 0);
			StartCoroutine (FadeScena (nameScena));

		}

	}

	/// <summary>
	/// Metodo che avvia la coroutine per cambiare scena
	/// </summary>
	/// <param name="nameScena">Name scena.</param>
	public void ChangeScenaForNewGame(int index)
	{

		//Converto il numero di scena in nome
		string nameScena = SceneManager.GetSceneByBuildIndex (index).name;

		StartCoroutine (FadeScena (nameScena));

	}

	/// <summary>
	/// Metodo che avvia la coroutine per cambiare scena
	/// </summary>
	/// <param name="nameScena">Name scena.</param>
	public void ChangeScenaForNewGame(string nameScena)
	{
        
		PlayerPrefs.SetInt ("isFirstTime", 0);
        StartCoroutine (FadeScena (nameScena));

	}

	/// <summary>
	/// Metodo che avvia i Credits
	/// </summary>
	public void ChangeSceneToCredits()
	{

		SceneManager.LoadScene (5);

	}

	/// <summary>
	/// Coroutine che controlla il cambio di scena 
	/// </summary>
	/// <returns>The scena.</returns>
	public IEnumerator FadeScena(string nameScena)
	{

		Debug.Log ("Avvio scena!");

		while(background.alpha < 1)
		{

			//Aseptto che lo schermo sia totalmente nero

			yield return null;
            

        }

		if (Player != null) 
		{

			Player.SetActive (true);

		}

		Debug.Log ("Avvio scena!");

        //gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        FindObjectOfType<ProgressSceneLoader>().LoadScene(nameScena);
        //SceneManager.LoadScene (nameScena);


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
				ChangeFirstSelected (isGamepad);
				checkIsGamepad = isGamepad;

			} 
			else 
			{

				if (eSystem.currentSelectedGameObject == false) {

					ChangeFirstSelected (isGamepad);

				}
				//ChangeFirstSelected (isGamepad);

			}

			yield return new WaitForSeconds (0.1f);

		}

		yield return null;


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

	#endregion

}

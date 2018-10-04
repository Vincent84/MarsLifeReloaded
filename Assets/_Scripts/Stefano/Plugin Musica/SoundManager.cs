using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

	#region Public 
	public Musica2 music;
	public static bool isRadio = false;

	public int Scena0;
	public int Scena1;
	public int Scena2;
	public int Scena3;
	public int Scena4;

	public AudioMixer mixerMusic;

	[Header("Variabile da utilizzare in case di Debug, togliere in delivery")]
	public bool isDebug = false;

	public float timerChangeMusic = 0.5f;

	#endregion

	#region Private 

	private int nStation = 0;
	private float timerCommand = 0;
	private bool activeTimer = false;

	private int overTwoSong = 0;

	private HUD managerHUD;

	#endregion

	void Awake()
	{

		Scene currentScena = SceneManager.GetActiveScene ();

		if (currentScena.buildIndex != Scena0) 
		{
			managerHUD = GameObject.Find ("PauseMenu").GetComponent<HUD> ();
		}
	}

	void Start()
	{

		if (isDebug == true) 
		{

			PlayerPrefs.SetInt ("isFirstTime", 0);

		}

		Debug.Log (gameObject.name);

		Scene currentScena = SceneManager.GetActiveScene ();


		if (currentScena.buildIndex == Scena0) 
		{

			//Vento
			music.GoStartMusic (0,0);

		}

		if (currentScena.buildIndex == Scena1)
		{

			//Controlliamo se è una nuova partita
			if (PlayerPrefs.GetInt ("isFirstTime") == 0)
			{

				PlayerPrefs.SetInt ("isFirstTime", 1);
				music.GoSnapShotFade (0);

			} 
			else 
			{

				music.GoSnapShotFade (1);

			}

			//music.GoStartMusic (0, 0);
			//music.GoStartMusic (0, 1);
			//Vento
			//music.GoStartMusic (0, 2);
			//music.GoStartMusic (0, 3);
			//music.GoStartMusic (0, 4);
			//music.GoStartMusic (0, 5);

		}

		if (currentScena.buildIndex == Scena2) 
		{

			//Vento
			music.GoStartMusic (0, 2);
			music.GoSnapShotFade (1);

		}

		if (currentScena.buildIndex == Scena3) 
		{

			//Vento
			music.GoStartMusic (0, 2);
			music.GoSnapShotFade (1);

		}

		if (currentScena.buildIndex == Scena4) 
		{

			//Vento
			music.GoStartMusic (0, 2);
			music.GoSnapShotFade (1);

		}


		if (currentScena.buildIndex != Scena0)
		{
			//Eseguiamo la radio
			mixerMusic.SetFloat ("Volume", -80f);
			music.GoRandomClusterFade (0);
			music.GoPauseClusterFade (0);
		}

	}

	void Update()
	{


		if(SceneManager.GetActiveScene().buildIndex != Scena0 && activeTimer == false && managerHUD.GetMenuIsOpen() == false && managerHUD.GetCodexMenuIsOpen() == false && managerHUD.GetMenuRoverIsOpen() == false)
		{

			//Accendo o spengo la radio
			if (Input.GetKeyDown (KeyCode.UpArrow) || InputManager.UPArrow()) 
			{

				activeTimer = true;
				managerHUD.ChangeTextSong (music.InfoClusterFadeNameSongInPlay (0));
				SetRadio ();

			}

			//Andiamo alla canzone successiva 
			if ((InputManager.RIGHTArrow () == true || Input.GetKeyDown(KeyCode.RightArrow) == true) && isRadio == true && activeTimer == false)
			{

				activeTimer = true;
				NextMusic ();
				managerHUD.MoveOnMenu ("MusicNext");
				managerHUD.ChangeTextSong (music.InfoClusterFadeNameNextSong (0));

			}

			//Andiamo alla canzone precedente 
			if ((InputManager.LEFTArrow () == true || Input.GetKeyDown(KeyCode.LeftArrow) == true) && isRadio == true && activeTimer == false) 
			{

				activeTimer = true;
				PreviousMusic ();
				managerHUD.MoveOnMenu ("MusicPrevious");
				managerHUD.ChangeTextSong (music.InfoClsuterFadeNamePreviousSong (0));

			}

		}

		//Diamo dei dealy ai comandi 
		if (activeTimer == true) 
		{

			timerCommand += Time.deltaTime;

			if(timerCommand > timerChangeMusic)
			{

				timerCommand = 0;
				activeTimer = false;

			}

		}

	}

	#region Radio

	/// <summary>
	/// Metodo che si occupa di attivare o disattivare la radio 
	/// </summary>
	private void SetRadio ()
	{

		music.GoPlayOneShot (2);

		if (isRadio == false) 
		{

			isRadio = true;
			//mixerMusic.SetFloat("Volume", ES2.Load<float> ("Setting.txt?tag="+mixerMusic.name));
			mixerMusic.SetFloat("Volume", -20f);

			music.GoPlayClusterFade (0);
			managerHUD.MoveOnMenu ("MusicStart");

			Debug.Log ("Radio attiva");

		} 
		else 
		{

			isRadio = false;
			mixerMusic.SetFloat("Volume", -80f);

			music.GoPauseClusterFade (0);
			managerHUD.MoveOnMenu ("MusicStop");

			Debug.Log ("Radio disattiva");

		}

	}

	/// <summary>
	/// Metodo che passa alla canzone successiva
	/// </summary>
	private void NextMusic()
	{

		music.GoNextMusicClusterFade (0);

	}

	/// <summary>
	/// Metodo che passa alla canzone precedente
	/// </summary>
	private void PreviousMusic()
	{

		music.GoPreviousMusicClusterFade (0);

	}

	#endregion

}

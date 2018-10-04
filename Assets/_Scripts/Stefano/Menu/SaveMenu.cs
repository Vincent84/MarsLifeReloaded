using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Audio;
using Sirenix;
using Sirenix.OdinInspector;

public class SaveMenu : MonoBehaviour {

	#region Public 

	[FoldoutGroup("Salvataggio Audio"), Space(10)]
	[Header("Settaggi audio")]
	public List<SettingAudio> listAudio;

	#region Salvataggio menu

	[FoldoutGroup("Salvataggio menu"), Space(10)]
	[Header("Vettore d'immagine per gli slot di salvataggio")]
	public Sprite[] illustrtions;

	[FoldoutGroup("Salvataggio menu")]
	[Header("Viene caricata questa immagine se non c'è il salvataggio")]
	public Sprite illustrationEmpty;

	[FoldoutGroup("Salvataggio menu")]
	[Header("Immagini da sostituire degli slot")]
	public Image imageSlot1;
	[FoldoutGroup("Salvataggio menu")]
	public Image imageSlot2;
	[FoldoutGroup("Salvataggio menu")]
	public Image imageSlot3;

	[FoldoutGroup("Salvataggio menu")]
	[Header("Riferimenti per il nome degli slot")]
	public Text nameSlot1;
	[FoldoutGroup("Salvataggio menu")]
	public Text nameSlot2;
	[FoldoutGroup("Salvataggio menu")]
	public Text nameSlot3;

	[FoldoutGroup("Salvataggio menu")]
	[Header("Riferimenti per la data degli slot")]
	public Text dateSlot1;
	[FoldoutGroup("Salvataggio menu")]
	public Text dateSlot2;
	[FoldoutGroup("Salvataggio menu")]
	public Text dateSlot3;

	#endregion

	[FoldoutGroup("Variabili no save data")]
	[Header("Variabili in caso di mancanza del file di salvataggio")]
	public Color enableColor; 


	#endregion

	#region Class

	[Serializable]
	public class SettingAudio
	{

		public AudioMixer audioMain;
		public AudioMixer audioSecondary;
		public List<Image> listImage;

	}

	#endregion

	void Start()
	{

		LoadAudio ();
		//LoadSlotInfo ();

	}

	#region Setting save data 

	/// <summary>
	/// Metodo che permette di settare lo slot in cui effettuare il caricamento o il salvataggio
	/// </summary>
	/// <param name="slotName">Slot name.</param>
	public void SetSlot(Text slotName)
	{

		PlayerPrefs.SetString ("Slot", slotName.text);
		Debug.Log (PlayerPrefs.GetString ("Slot"));

	}

	#endregion

	#region Save

	public void SaveSettings()
	{

		SaveAudio ();

	}

	/// <summary>
	/// Metodo che salva i volumi 
	/// </summary>
	public void SaveAudio()
	{

		for (int i = 0; i < listAudio.Count; i++) 
		{

			float volume;
			listAudio [i].audioMain.GetFloat ("Volume", out volume);

			ES2.Save (volume, "Setting.txt?tag=" + listAudio [i].audioMain.name);

			for (int j = 0; j < listAudio [i].listImage.Count; j++) 
			{

				ES2.Save<Color32> (listAudio [i].listImage [j].color, "Setting.txt?tag=" + listAudio [i].audioMain.name + listAudio [i].listImage [j].name);

			}

		}

	}

	#endregion

	#region Load

	/// <summary>
	/// Metodo che carica i volumi	
	/// </summary>
	public void LoadAudio()
	{

		if (ES2.Exists ("Setting.txt")) 
		{

			for (int i = 0; i < listAudio.Count; i++) 
			{

				if (listAudio [i].audioMain.name == "Music" && SoundManager.isRadio == true) 
				{
					listAudio [i].audioMain.SetFloat ("Volume", ES2.Load<float> ("Setting.txt?tag=" + listAudio [i].audioMain.name));
				} 

				if (listAudio [i].audioMain.name != "Music") 
				{
					listAudio [i].audioMain.SetFloat ("Volume", ES2.Load<float> ("Setting.txt?tag=" + listAudio [i].audioMain.name));

					//Controllo se questo mixer ha una dipendenza di un altro mixer 
					if (listAudio [i].audioSecondary != null) 
					{

						listAudio [i].audioSecondary.SetFloat ("Volume", ES2.Load<float> ("Setting.txt?tag=" + listAudio [i].audioMain.name));

					}

				}

				for (int j = 0; j < listAudio [i].listImage.Count; j++) 
				{
					
					listAudio [i].listImage [j].color = ES2.Load<Color32> ("Setting.txt?tag=" + listAudio [i].audioMain.name + listAudio [i].listImage [j].name);

				}

			}

		} else {

			Debug.Log ("Il file non esiste, lo creo");

			SaveAudio ();

			for (int i = 0; i < listAudio.Count; i++) 
			{

				if (listAudio [i].audioMain.name == "Music" && SoundManager.isRadio == true) 
				{
					listAudio [i].audioMain.SetFloat ("Volume", -20f);
				} 

				if (listAudio [i].audioMain.name != "Music") 
				{
					listAudio [i].audioMain.SetFloat ("Volume",-20f);

					//Controllo se questo mixer ha una dipendenza di un altro mixer 
					if (listAudio [i].audioSecondary != null) 
					{

						listAudio [i].audioSecondary.SetFloat ("Volume",-20f);

					}

				}

				//Setto il volume visivo a metà
				for (int j = 0; j < 5; j++) 
				{

					listAudio [i].listImage [j].color = enableColor ;

				}

			}

		}

	}
		
	/// <summary>
	/// Metodo che carica le informazioni degli slot
	/// </summary>
	public void LoadSlotInfo()
	{


		if (ES2.Exists (nameSlot1.text + ".txt") && ES2.Exists(nameSlot1.text + ".txt?tag=SaveTime") == true) 
		{

			DateTime date = DateTime.Parse (ES2.Load<string> ( nameSlot1.text + ".txt?tag=SaveTime"));
			dateSlot1.text = date.ToLocalTime ().ToString();

		} 
		else 
		{

			dateSlot1.text = "";

		}

		if (ES2.Exists (nameSlot2.text + ".txt") && ES2.Exists(nameSlot2.text + ".txt?tag=SaveTime") == true) 
		{

			DateTime date = DateTime.Parse (ES2.Load<string> (nameSlot2.text + ".txt?tag=SaveTime"));
			dateSlot2.text = date.ToLocalTime ().ToString();

		} 
		else 
		{

			dateSlot2.text = "";

		}

		if (ES2.Exists (nameSlot3.text + ".txt") && ES2.Exists(nameSlot3.text + ".txt?tag=SaveTime") == true) 
		{

			DateTime date = DateTime.Parse (ES2.Load<string> ( nameSlot3.text + ".txt?tag=SaveTime"));
			dateSlot3.text = date.ToLocalTime ().ToString();

		} 
		else 
		{

			dateSlot3.text = "";

		}


		LoadSlotIllustrations ();

	}

	/// <summary>
	/// Metodo che carica le illustrazioni degli slot di salvataggio
	/// </summary>
	private void LoadSlotIllustrations()
	{

		string scenaCurrentSlot1;
		string scenaCurrentSlot2;
		string scenaCurrentSlot3;

		bool find1 = false;
		bool find2 = false;
		bool find3 = false;

		if (ES2.Exists (nameSlot1.text + ".txt")) 
		{
			scenaCurrentSlot1 = ES2.Load<string> (nameSlot1.text + ".txt?tag=currentScene");
		} 
		else 
		{

			scenaCurrentSlot1 = "empty";
			imageSlot1.sprite = illustrationEmpty;
			find1 = true;

		}

		if (ES2.Exists (nameSlot2.text + ".txt")) 
		{
			scenaCurrentSlot2 = ES2.Load<string> (nameSlot2.text + ".txt?tag=currentScene");
		}
		else 
		{

			scenaCurrentSlot2 = "empty";
			imageSlot2.sprite = illustrationEmpty;
			find2 = true;

		}

		if (ES2.Exists (nameSlot3.text + ".txt")) 
		{

			scenaCurrentSlot3 = ES2.Load<string> (nameSlot3.text + ".txt?tag=currentScene");
		}
		else 
		{

			scenaCurrentSlot3 = "empty";
			imageSlot3.sprite = illustrationEmpty;
			find3 = true;

		}

        //find1 != true && find2 != true && find3 != true


        if (true) 
		{
			for (int i = 0; i < illustrtions.Length; i++) 
			{

				if (scenaCurrentSlot1 == illustrtions [i].name) 
				{

					imageSlot1.sprite = illustrtions [i];

				}

				if (scenaCurrentSlot2 == illustrtions [i].name) 
				{

					imageSlot2.sprite = illustrtions [i];

				}

				if (scenaCurrentSlot3 == illustrtions [i].name) 
				{

					imageSlot3.sprite = illustrtions [i];

				}

			}
		}

	}

	#endregion


}

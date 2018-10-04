using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// Script che permette di caricare i dati di gioco
/// </summary>
public class LoadOnFile : MonoBehaviour 
{

	#region Public

	public bool autoLoad = true;
	public GameObject objSaveManager;
	[Header("")]
	[Header("Lista di parametri da caricare")]
	public bool objTransform = true;
	public bool questSystem = true;
	public bool playerSystem = true;

	#endregion

	#region Private 

	private SaveOnFile saveManager;

	#endregion

	void Awake()
	{

		saveManager = objSaveManager.GetComponent<SaveOnFile> ();

		if (autoLoad == true) 
		{

			Load ();

		}

	}

	/// <summary>
	/// Metodo che carica i dati di gioco
	/// </summary>
	public void Load()
	{

		if (ES2.Exists (PlayerPrefs.GetString("Slot")+".txt")) 
		{
			
			LoadTransform ();
			loadQuest ();
			loadPlayer ();

		}
		else
			Debug.Log ("Il file non esiste");

	}

	/// <summary>
	/// Metodo che carica i trasnform 
	/// </summary>
	public void LoadTransform()
	{

		for (int i = 0; i < saveManager.listTransform.Count; i++) 
		{

			//Carico i dati da un tag univoco
			ES2.Load<Transform> ( PlayerPrefs.GetString("Slot")+".txt?tag=transform" + saveManager.listTransform [i].name + SceneManager.GetActiveScene ().name, saveManager.listTransform [i]);

		}

	}

	/// <summary>
	/// Metosdo che carica le quest
	/// </summary>
	public void loadQuest()
	{

		if (questSystem == true) 
		{
			for (int i = 0; i < saveManager.listQuest.Count; i++) 
			{

				//Carico i parametri della quest
				saveManager.listQuest [i].name = ES2.Load<string> (PlayerPrefs.GetString("Slot")+".txt?tag=quest" + i + "name");
				saveManager.listQuest [i].isActive = ES2.Load<bool> (PlayerPrefs.GetString("Slot")+".txt?tag=quest" + i + "isActive");

				//Carico la lista di interi
				for (int j = 0; j < saveManager.listQuest [i].listNumeri.Count; j++) 
				{

					saveManager.listQuest [i].listNumeri[j] = ES2.Load<int> (PlayerPrefs.GetString ("Slot") + ".txt?tag=quest" + i + "listNumeri"+j);

				}

			}
		} 
		else 
		{

			Debug.Log ("Non carico le Quest");

		}

	}

	/// <summary>
	/// Metodo che carica i dati del player
	/// </summary>
	public void loadPlayer()
	{

		if (playerSystem == true) 
		{

			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			ES2.Load<Transform> (PlayerPrefs.GetString ("Slot") + ".txt?tag=playerTransform", player.transform );

		} 
		else 
		{

			Debug.Log ("Non carico il player");

		}

	}

}

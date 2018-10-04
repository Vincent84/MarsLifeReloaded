using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

/// <summary>
/// Script che permette di salvare i dati di gioco
/// </summary>
public class SaveOnFile : MonoBehaviour {

	#region Public

	[Header("Lista di parametri da salvare") ]
	public bool objTransform = true;
	public bool questSystem = true;
	public bool playerSystem = true;
	[Header("")]

	[Header("Lista transform oggetti")]
	public List<Transform> listTransform;
	[Header("Lista quest")]
	public List<Quest> listQuest;

	#endregion

	[Serializable]
	public class Quest
	{

		public string name;
		public bool isActive;
		public List<int> listNumeri;

	}


	/// <summary>
	/// Metodo che salva determinati dati
	/// </summary>
	public void Save()
	{


		SaveTransform ();
		SaveQuest ();
		SavePlayer ();


	}

	/// <summary>
	/// Metodo che salva i transform
	/// </summary>
	public void SaveTransform()
	{

		if (objTransform == true) 
		{
			for (int i = 0; i < listTransform.Count; i++) 
			{

				//Salvo il parametro in un tag univoco
				ES2.Save (listTransform [i], PlayerPrefs.GetString("Slot")+".txt?tag=transform" + listTransform [i].name + SceneManager.GetActiveScene ().name);

			}
		} 
		else 
		{
			
			Debug.Log ("Non salvo i Transform");

		}

	}

	/// <summary>
	/// Metodo che salva le Quest
	/// </summary>
	public void SaveQuest()
	{

		if (questSystem == true) 
		{
			for (int i = 0; i < listQuest.Count; i++) 
			{

				//Salvo i parametri della quest
				ES2.Save (listQuest [i].name, PlayerPrefs.GetString("Slot")+".txt?tag=quest" + i + "name");
				ES2.Save (listQuest [i].isActive, PlayerPrefs.GetString("Slot")+".txt?tag=quest" + i + "isActive");

				//Salvo la lista di interi
				for (int j = 0; j < listQuest [i].listNumeri.Count; j++) 
				{
					
					ES2.Save (listQuest [i].listNumeri[j], PlayerPrefs.GetString ("Slot") + ".txt?tag=quest" + i + "listNumeri"+j);

				}

			}
		} 
		else 
		{

			Debug.Log ("Non salvo le Quest");

		}

	}

	/// <summary>
	/// Metodo che salva i dati del player
	/// </summary>
	public void SavePlayer()
	{

		if (playerSystem == true) 
		{

			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			ES2.Save (player.transform, PlayerPrefs.GetString ("Slot") + ".txt?tag=playerTransform");

		} 
		else 
		{

			Debug.Log ("Non salvo il player");

		}

	}

}

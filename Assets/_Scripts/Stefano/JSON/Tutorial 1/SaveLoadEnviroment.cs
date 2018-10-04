using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveLoadEnviroment : MonoBehaviour {

	[Header("Oggetti da salvare nella scena")]
	public List<EnviromentData> EnviromentsObjects = new List<EnviromentData>();
	//private EnviromentsData ListEnviromentsObjects = new EnviromentsData ();
	private string FilePath;
	private string jsonString;

	void Awake()
	{

		//ListEnviromentsObjects.EnviromentObjects = EnviromentsObjects;

		//Path del file di salvataggio
		FilePath = Path.Combine(Application.dataPath, "EnviromentSave.json");

		//Controllo se il file esiste
		if (File.Exists (FilePath) == false) 
		{

			//Il file non esiste allora lo creo

			Debug.Log ("Il file di salvataggio non esiste, lo creo");
			File.Create (FilePath);

		} 
		else 
		{
			//Il file di salvataggio esiste

			Debug.Log ("File salvtaggio OK");
			Load ();

		}

	}

	void Update()
	{

		if (Input.GetKey (KeyCode.S)) {

			Save ();

		}

	}

	//Metodo che permette il salvataggio dei dati dell'enviroment
	void Save()
	{

		Debug.Log ("Salvo i dati");

		for (int i = 0; i < EnviromentsObjects.Count; i++) 
		{
			jsonString += JsonUtility.ToJson (EnviromentsObjects [1]);
		}

		File.WriteAllText (FilePath, jsonString);
	

	}

	//Metodo che permette il salvataggio dei dati dell'enviroment
	void Load()
	{

		Debug.Log ("Carichiamo i dari");
		jsonString = File.ReadAllText (FilePath);
		EnviromentsObjects = JsonUtility.FromJson<List<EnviromentData>> (jsonString);

	}
}

[System.Serializable]
public class EnviromentsData 
{
	//Lista degli oggetti in scena da salvare
	public List<EnviromentData> EnviromentObjects;

}


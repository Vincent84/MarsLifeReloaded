using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveLoad : MonoBehaviour {

	public PlayerData playerData;
	private string FilePath;

	// Use this for initialization
	void Start () 
	{
	
		FilePath = Path.Combine(Application.dataPath, "save.json");
		Load ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}


	void Save()
	{

		string jsonString = JsonUtility.ToJson (playerData);
		File.WriteAllText (FilePath, jsonString);

	}


	void Load()
	{

		string jsonString = File.ReadAllText (FilePath);
		JsonUtility.FromJsonOverwrite (jsonString, playerData);

	}

}

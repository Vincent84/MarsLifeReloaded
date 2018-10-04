using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager{

	public static void SavePlayer(Player player)
	{

		BinaryFormatter bf = new BinaryFormatter ();
		//flusso di dati
		FileStream stream = new FileStream (Application.persistentDataPath + "/player.sav", FileMode.Create);

		//dati da salvare 
		PlayerDataS data = new PlayerDataS (player);

		//serializiamo 
		bf.Serialize (stream, data);
		//chiudiamo il canale 
		stream.Close ();

	}
		
	public static int[] LoadPlayer()
	{

		if (File.Exists (Application.persistentDataPath + "/player.sav")) {

			BinaryFormatter bf = new BinaryFormatter ();
			FileStream stream = new FileStream (Application.persistentDataPath + "/player.sav", FileMode.Open);

			PlayerDataS data = bf.Deserialize (stream) as PlayerDataS;

			stream.Close ();

			return data.stats;

		} else {

			Debug.LogError ("File does not exist");
			return new int[4]; 

		}

	}

}

[Serializable]
public class PlayerDataS
{

	public int[] stats;

	public PlayerDataS(Player player)
	{

		stats = new int[4];
		stats[0] = player.Level;
		stats[1] = player.Health;
		stats[2] = player.Attack;
		stats[3] = player.Defense;

	}

}

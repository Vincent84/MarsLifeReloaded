using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int Level = 1;
	public int Health = 20;
	public int Attack = 6;
	public int Defense = 4;

	public void Save()
	{

		SaveLoadManager.SavePlayer (this);

	}

	public void Load()
	{

		int[] loadStats = SaveLoadManager.LoadPlayer ();

		Level = loadStats [0];
		Health = loadStats [1];
		Attack = loadStats [2];
		Defense = loadStats [3];

		GetComponent<PlayerDisplay>().UpdateDisplay ();

	}

}

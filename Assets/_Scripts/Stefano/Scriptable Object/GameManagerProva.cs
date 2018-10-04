using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerProva : MonoBehaviour {

	public EnemyMove enemy;

	public EnemyData[] levelData;

	[Range(1,5)]
	public int level;

	void Start()
	{

		levelData [0].Amici_Invitati = 2;

	}

	void Update()
	{

		Debug.Log (levelData [0].Amici_Invitati);

	}

}

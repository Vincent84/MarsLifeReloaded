using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyData : ScriptableObject {

	public string name;
	public float moveTime;
	public Color color;

	[Header("Lista di amici")]
	public List<string> Freiends;

	[Range(1,5)]
	public int Amici_Invitati;

	[Header("Posizione del personaggio")]
	public Vector3 Mia_Posizione;

	[Header("Tanti amici")]
	public List<Amico> Amici;


}

[Serializable]
public class Amico
{

	public string nome;
	public int amici;

}

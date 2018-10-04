using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CharacterData : ScriptableObject {

	public enum characterType{Medico, Ingeniere, Botanico, Geologo};

	[Header("Nome del personaggio")]
	public string name;
	[Header("Classe del personaggio")]
	public characterType type;
	[Header("Vita del personaggio")]
	[Range(0.0f, 100.0f)]
	public float health;
	[Header("Gadgets")]
	public List<Gadgets> gadgets;
	[Header("Oggetti trovati")]
	public List<MartianObject> MartianObjects;
	[Header("Missioni")]
	public List<Mission> missions;

}

//Classe gadget
[Serializable]
public class Gadgets
{

	public string name;
	public string informationText;
	public Image image;
	public bool isVisible = false;

}


//Classe oggetti marziani trovati
[Serializable]
public class MartianObject
{
	public enum objectType{Type1, Type2, Type3, Type4};

	public string name;
	public string informationText;
	public Image image;
	public GameObject model;
	[Range(0,99)]
	public int quantity;
	public objectType type;
	[Header("ID oggetto")]
	public int ID;
	public bool isVisible = false;

}

//Classe missione
[Serializable]
public class Mission
{

	public enum type{Type1, Type2, Type3, Type4};

	public string name;
	public string informationText;
	public Image image;
	public List<Subtask> subTasks;
	public type missionType;
	public bool done;

}

//Classe subtask
[Serializable]
public class Subtask
{

	public string name;
	public string informationText;
	public Image image;
	public bool done;

}




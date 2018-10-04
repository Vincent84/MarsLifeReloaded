using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SceneData : ScriptableObject {

	[Header("Lista di oggetti che cambiano posizione")]
	public List<SceneObject> SceneObjects;
	[Header("Lista di detriti")]
	public List<Debries> SceneDebries;
	[Header("lista dei luoghi da triverllare")]
	public List<DrilledPlace> DrilledPlaces;
	[Header("Lista degli eventi ambientali")]
	public List<AmbinetEvent> AmbientEvents;

}

[Serializable]
public class SceneObject
{

	//Utilizzare il tag Save

	public int hashcode;
	public Vector3 position;
	public bool isDestroyed;

}

[Serializable]
public class Debries
{

	//Utilizzare il tag Debries

	public int hashcode;
	public int ID_objectFound;
	public bool isDestroyed;

}

[Serializable]
public class DrilledPlace
{

	//Utilizzare il tag Driller

	public int hascode;
	public int ID_objectFound;
	public bool isDrilled;

}

[Serializable]
public class AmbinetEvent
{

	public string name;
	public string infoText;
	public ParticleSystem martEvent;
	public GameObject eventObject;
	public bool done;

}
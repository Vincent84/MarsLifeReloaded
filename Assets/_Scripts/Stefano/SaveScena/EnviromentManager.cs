using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentManager : MonoBehaviour {

	#region Public
	public string tag;
	public SceneData Scena;
	#endregion

	#region Private
	private List<GameObject> listSceneObjects = new List<GameObject>();
	#endregion

	#region Test
	void Awake()
	{

		if (Scena.SceneObjects.Count == 0) 
		{
			PopulateList ();
		}

		Load ();

	}

	void Update()
	{

		if (Input.GetKey (KeyCode.S)) {

			Save ();

		}

	}

	//DA UTILIZZARE SOLO PER TEST
	private void PopulateList()
	{

		GameObject[] Array = GameObject.FindGameObjectsWithTag (tag);

		for (int i = 0; i < Array.Length; i++) 
		{
			
			Scena.SceneObjects.Add(CreatNewSceneObject (Array [i]));

		}

	}

	private SceneObject CreatNewSceneObject(GameObject obj)
	{

		SceneObject Sobj = new SceneObject ();

		Sobj.hashcode = obj.GetHashCode ();
		Sobj.isDestroyed = obj.activeSelf;
		Sobj.position = obj.transform.position;

		return Sobj;

	}
	#endregion

	//popoliamo la lista con oggetti di scena
	public void SearchSceneObject()
	{

		listSceneObjects.Clear ();
		listSceneObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag (tag));

	}

	#region Save
	//Salvataggio dati degli oggetti di scena
	public void Save()
	{

		//Scorro tutta la lista 
		for (int i = 0; i < listSceneObjects.Count; i++)
		{


			ChangeSavedObject(Scena.SceneObjects.Find(x => x.hashcode == listSceneObjects [i].GetHashCode ()),listSceneObjects[i]);


		}

	}

	//Metodo che aggiorna i dati di un oggetto salvato
	private void ChangeSavedObject(SceneObject obj, GameObject newObj)
	{

		obj.isDestroyed = newObj.activeSelf;
		obj.position = newObj.transform.position;

	}
	#endregion

	#region Load
	//Caricamento dei dati degli oggetti di scena
	public void Load()
	{

		SearchSceneObject ();

		//Scorro tutta la lista 
		for (int i = 0; i < listSceneObjects.Count; i++)
		{


			ChangeCurrentObject(Scena.SceneObjects.Find(x => x.hashcode == listSceneObjects [i].GetHashCode ()),listSceneObjects[i]);


		}

	}

	//Metodo che aggiorna i dati di un oggetto in scena
	private void ChangeCurrentObject(SceneObject listObject, GameObject currentObject)
	{

		currentObject.SetActive (listObject.isDestroyed);
		currentObject.transform.position = listObject.position;

	}
	#endregion


}

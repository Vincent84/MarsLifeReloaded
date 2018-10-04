using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnviromentData: MonoBehaviour {

	//Per identificare univocamente l'oggetto
	public int hashcode;
	[Header("Posizione elemento")]
	public Vector3 position;
	[Header("Oggetto distrutto?")]
	public bool isDestroyed;

	void Awake()
	{

		hashcode = this.gameObject.GetHashCode();

	}

	public override string ToString ()
	{
		return string.Format ("Hashcode: {0} - Position {1} - isDestroyed {2}", hashcode, position, isDestroyed);
	}


}

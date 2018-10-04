using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSatellite : MonoBehaviour {

	#region Private

	private Musica2 music;
	private bool isEnter = false;

	#endregion

	#region Public

	public TriggerSatellite pairTrigger;

	#endregion

	void Start()
	{

		//Cerca automaticamente il componente nella scena
		music = GameObject.Find("_AUDIO").GetComponent<Musica2>();

	}

	//Quando entra il personaggio
	void OnTriggerEnter(Collider other)
	{

		if (other.tag == "Player") 
		{


			Debug.Log ("Entrati");

			if (isEnter == false) 
			{

				isEnter = true;
				pairTrigger.isEnter = true;

				music.GoMusicFade (0);

				Destroy (this.gameObject);

			} 
			else 
			{

				isEnter = false;
				pairTrigger.isEnter = false;

				music.GoClusterFade (0);

				Destroy (this.gameObject);

			}

		}


	}

}

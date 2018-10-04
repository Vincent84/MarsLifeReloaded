using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudioSong : MonoBehaviour {


	public int[] Songs;
	public bool isEnter = false;

	private Musica2 music;

	void Awake()
	{

		music = GameObject.Find ("_AUDIO").GetComponent<Musica2> ();

	}

	private void OnTriggerEnter(Collider other)
	{

		if (other.tag == "Player") 
		{
			

			//Controllo se siamo entrati 
			if (isEnter == false) 
			{

				isEnter = true;

				for (int i = 0; i < Songs.Length; i++) 
				{

					Debug.Log ("Suono qualche cosa ");
					music.GoStartMusic (0,Songs [i]);

				}

			}
			else //Controllo se siamo usciti 
			{

				isEnter = false;

				for (int i = 0; i < Songs.Length; i++) 
				{

					Debug.Log ("Stoppo qualche cosa ");
					music.GoStopMusic (0,Songs [i]);

				}

			}

		}
			
	}
		

}

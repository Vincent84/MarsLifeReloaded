using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerAudio : MonoBehaviour {

	#region Public 

	public int[] listSnapShotEnter;
	public int[] listSnapshotExit;
	public string name;

	public TriggerAudio pairTrigger;
	public bool isEnter = false;

	#endregion

	#region Private

	private Musica2 music;

	#endregion


	void Awake()
	{

		//Cerca automaticamente il componente nella scena
		music = GameObject.Find("_AUDIO").GetComponent<Musica2>();

	}

	//Qaundo entra nel trigger
	void OnTriggerEnter(Collider other)
	{

		if(other.tag == "Player")
		{
			

			if(isEnter == false)
			{

				isEnter = true;
				Debug.Log(name + "Entrato");

				if (pairTrigger != null) 
				{

					pairTrigger.isEnter = true;

				}

				if (SceneManager.GetActiveScene ().buildIndex == 1 && PlayerPrefs.GetInt ("isFirstTime") == 0) 
				{
					for (int i = 0; i < listSnapShotEnter.Length; i++) {

						music.GoSnapShotFade (listSnapshotExit [i]);

					}
				}
				else 
				{

					for (int i = 0; i < listSnapShotEnter.Length; i++) {

						music.GoSnapShotFade (listSnapShotEnter [i]);

					}

				}

			}
			else
			{

				isEnter = false;
				Debug.Log(name + "Uscito");

				if (pairTrigger != null) 
				{

					pairTrigger.isEnter = false;

				}

				for(int i=0; i< listSnapshotExit.Length; i++)
				{

					music.GoSnapShotFade(listSnapshotExit[i]);

				}

			}
		}


	}

	//Quando esce del trigger
	void OnTriggerExit(Collider other)
	{



	}


}

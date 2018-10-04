using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musica : MonoBehaviour {

	#region Public
	[Header("Suoni singoli")]
	public List<Brano> Suoni;
	[Header("Play list")]
	public List<BranoPlayList> PlayList;
	[Header("Play list suoni random")]
	public List<RandomAudio> ClusterSuoniRandom;
	[Header("Audio source per l'output")]
	public AudioSource Output;
	[Header("Audio source global")]
	public AudioSource Global;
	[Header("Debug mode")]
	public bool DebugMode;
	#endregion

	#region Private
	private int ID_suono;
	private int indexPlayList = 0;
	#endregion

	[System.Serializable]
	public class Brano
	{
		[Header("Nome della sezione")]
		public string NomeBrano;
		[Header("Suono da istanziare")]
		public AudioClip Audio;
		[Header("ID univoco della canzone numerico crescente")]
		public int ID;
		[Header("Descrizione del cluster")]
		public string Descrizione;
		[Header("Volume del cluster")]
		[Range(0f,1f)]
		public float Volume;

	}

	[System.Serializable]
	public class RandomAudio
	{
		[Header("Nome del cluster")]
		public string NomeCluster;
		[Header("Suoni randomici")]
		public List<AudioClip> ListaSuoni;
		[Header("Descrizione del suono")]
		public string Descrizione;
		[Header("Volume del suono")]
		[Range(0f,1f)]
		public float Volume;

	}

	[System.Serializable]
	public class BranoPlayList
	{
		[Header("Brano da riprodurre")]
		public AudioClip Brano;
		[Header("Volume del brano")]
		[Range(0f,1f)]
		public float Volume;
	}

	void Update()
	{

		/*
		if (Output.isPlaying == false) 
		{

			RiproduciPlayList ();
			indexPlayList++;

		}*/

	}

	/// <summary>
	/// Metodo che riproduce l'audio passando l'ID della canzone della lista
	/// </summary>
	/// <param name="ID">ID della canzone da riprodurre</param>
	public void RiproduciSuono(int ID)
	{

		if (Output.isActiveAndEnabled == true) 
		{

			for (int i = 0; i < Suoni.Count; i++) {

				if (Suoni [i].ID == ID) {

					if (DebugMode == true)
						Debug.Log ("Riproduco suono " + Suoni [i].ID);

					Output.PlayOneShot (Suoni [i].Audio, Suoni [i].Volume);
					return;

				}

			}
		} 
		else 
		{

			Debug.Log ("Errore nell'Audio Source");

		}

		Debug.LogError ("Muisca non trovata CONTROLLARE LISTA");


	}

	public void RiproduciPlayList()
	{

		if (indexPlayList == PlayList.Count - 1)
			indexPlayList = 0;

		Output.clip = PlayList [indexPlayList].Brano;
		Output.Play ();

	}

	/// <summary>
	/// Setting dell'audio source globale
	/// </summary>
	public void SetAudioSourceGlobal(float value)
	{

		Global.volume = value;

	}

	/// <summary>
	/// Setting dell'audio source globale
	/// </summary>
	public void SetAudioSourceSuoni(float value)
	{

		Output.volume = value;

	}
}

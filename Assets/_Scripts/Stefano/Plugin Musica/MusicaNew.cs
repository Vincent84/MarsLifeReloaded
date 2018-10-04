﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using Sirenix.OdinInspector;

[RequireComponent(typeof(AudioSource))]
public class MusicaNew: MonoBehaviour 
{

	#region Public 

	public AudioMixerSnapshot puased;
	public AudioMixerSnapshot unpaused;

	[Header("Master per l'uscita audio")]
	public AudioMixer master;

	[Space(50)]
	//[Header("Lista di mixer con le relative tracce audio")]
	[Title("GESTIONE DEI SUONI DI GIOCO")]
	[InfoBox("In questa sezione si inseriscono tutti i suoni e le canzoni di giochi collegate al rispettivo AUDIOSOURCE di riferimento")]
	public Mixer[] systemMixer;


	[Space(30)]
	//[Header("Lista di audio utili per i suoni della Canvas o rapidi PlayOneshot")]
	[Title("PLAY LIST ONE SHOT")]
	[InfoBox("Questa sezione del plugin è riservata all'utilizzo di suoni di sistema come per esempio: \n" +
		"Suoni della Canvas \n" +
		"Suoni di pop-up")]
	[InfoBox("!!! SCONSIGLIATO L'UTILIZZO DI MUSICA IN QUESTA SEZIONE !!!", InfoMessageType.Warning)]
	public Track[] PlayList;


	[Button(ButtonSizes.Medium), GUIColor(0, 1, 0, 1f),]
	private void InizializzaID() { CreatID(); } 

	[Space(30)]
	[Header("Utilizzato genericamente per i suoni one shot")]
	public AudioSource genericAudioSource;
	[Range(0f,1f)]
	public float allVolumePlayList;
	[Button(ButtonSizes.Medium), GUIColor(0, 1, 0, 1f)]
	private void SetVolume() { SetVolumeAllPlayList(); } 
	[Button(ButtonSizes.Medium), GUIColor(0, 1, 0, 1f)]
	private void SetAudioSource() { SetTheSameAudioSource(); }


	#endregion

	#region Inspector

	/// <summary>
	/// Metodo che setta da Inspector il valore degli ID di tutta la PlayList
	/// </summary>
	private void CreatID()
	{

		for (int i = 0; i < PlayList.Length; i++) 
		{

			PlayList [i].ID = i;

		}

		Debug.Log ("ID INIZIALIZZAT");

	}

	/// <summary>
	/// Cambia da editor tutti i valori delle tracce della play list
	/// </summary>
	private void SetVolumeAllPlayList()
	{

		for (int i = 0; i < PlayList.Length; i++) 
		{

			PlayList [i].volume = allVolumePlayList;

		}

		Debug.Log ("VOLUMI SETTATI");

	}

	/// <summary>
	/// Metodo che setta a tutti i suoni della play lost lo stesso audio source
	/// </summary>
	private void SetTheSameAudioSource()
	{

		for (int i = 0; i < PlayList.Length; i++) 
		{

			PlayList [i].output = genericAudioSource;

		}

	}

	#endregion

	#region Private

	private int ID_suono;
	public static AudioSource genericOutput;

	#endregion

	#region Class

	[Serializable]
	public class Mixer
	{

		[Header("Audio mixer")]
		[GUIColor(0.3f, 0.8f, 0.8f, 1f)]
		public AudioMixer mixer;
		[Header("Lista di tracce audio")]
		public TrackMusic[] listTrack;

		[Space(30)]
		[Range(0f,1f)]
		public float allVolumePlayList;
		[Button(ButtonSizes.Medium), GUIColor(0, 1, 0, 1f),]
		private void InizializzaID() { CreatID(); } 
		[Button(ButtonSizes.Medium), GUIColor(0, 1, 0, 1f)]
		private void SetVolume() { SetVolumeAllPlayList(); } 


		#region Metodi Mixer

		/// <summary>
		/// Metodo che setta da Inspector il valore degli ID di tutta la PlayList
		/// </summary>
		private void CreatID()
		{

			for (int i = 0; i < listTrack.Length; i++) 
			{

				listTrack[i].ID = i;

			}

			Debug.Log ("ID INIZIALIZZAT");

		}

		/// <summary>
		/// Cambia da editor tutti i valori delle tracce della play list
		/// </summary>
		private void SetVolumeAllPlayList()
		{

			for (int i = 0; i < listTrack.Length; i++) 
			{

				listTrack[i].volume = allVolumePlayList;

			}

			Debug.Log ("VOLUMI SETTATI");

		}

		#endregion

	}

	[Serializable]
	public class Track
	{

		[InlineButton("Play")]
		[Header("Suono")]
		[GUIColor(0.3f, 0.8f, 0.8f, 1f)]
		public AudioClip clip;
		[Header("ID univoco della canzone numerico crescente")]
		public int ID;
		[Header("Descrizione del suono")]
		[TextArea]
		public string description;
		[Header("Volume del suono")]
		[Range(0f,1f)]
		public float volume = 0.5f;
		[Header("Audio source di uscita")]
		public AudioSource output;


		#region Metodi Track

		/// <summary>
		/// Metodo che permette di ascoltare da editor il suono
		/// </summary>
		public void Play()
		{

			genericOutput.PlayOneShot (clip);

		}

		#endregion

	}

	[Serializable]
	public class TrackMusic
	{

		[InlineButton("Play")]
		[InlineButton("Stop")]
		[Header("Suono")]
		[GUIColor(0.3f, 0.8f, 0.8f, 1f)]
		public AudioClip clip;
		[Header("ID univoco della canzone numerico crescente")]
		public int ID;
		[Header("Descrizione del suono")]
		[TextArea]
		public string description;
		[Header("Volume del suono")]
		[Range(0f,1f)]
		public float volume = 0.5f;
		[Header("Audio source di uscita")]
		public AudioSource output;

		#region Metodi Track

		/// <summary>
		/// Metodo che avvia la canzone da editor
		/// </summary>
		public void Play()
		{

			genericOutput.clip = clip;
			genericOutput.Play ();

		}

		/// <summary>
		/// Metodo che ferma la canzona da editor
		/// </summary>
		public void Stop()
		{

			genericOutput.Stop ();

		}

		#endregion

	}


	#endregion

	void Awake()
	{

		genericOutput = gameObject.GetComponent<AudioSource> ();

	}


	#region Audio Source

	/// <summary>
	/// Metodo che riproduce l'audio passando l'ID della canzone della lista
	/// </summary>
	/// <param name="ID">ID della canzone da riprodurre</param>
	public void RiproduciSuono(int ID)
	{

		for (int i = 0; i < PlayList.Length; i++) 
		{

			if (PlayList [i].ID == ID) {

				Debug.Log ("Riproduco suono "+ PlayList[i].ID);
				genericOutput.PlayOneShot (PlayList [i].clip, PlayList[i].volume);
				return;

			}

		}

		Debug.LogError ("Muisca non trovata CONTROLLARE LISTA");

	}

	#endregion

	#region Audio Mixer

	/// <summary>
	/// Metodo che regolare il volume del master audio mixer
	/// </summary>
	/// <param name="value">Value.</param>
	public void SetVolumeMasterAudioMixer(float value)
	{

		master.SetFloat ("master", value);

	}

	/// <summary>
	/// Puases the music with snapshot.
	/// </summary>
	public void PuaseMusicWithSnapshot()
	{

		puased.TransitionTo (.01f);

	}

	/// <summary>
	/// Uns the paused with snapshot.
	/// </summary>
	public void UnPausedWithSnapshot()
	{

		unpaused.TransitionTo (.01f);

	}

	#endregion

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
#endif

[RequireComponent(typeof(AudioSource))]
public class Musica2 : MonoBehaviour
{

    #region Public

    [Header("Master per l'uscita audio")]
    public AudioMixer master;

    //Regione per gli snapshot
    #region SnapShot

    [FoldoutGroup("SNAP SHOT"), Space(10)]
    [Title("SYSTEM SNAP SHOT")]
    public SnapShot[] systemSnapShot;

    [FoldoutGroup("SNAP SHOT/DEBUG SNAP SHOT")]
    public int numberSnapShot = 0;
    [FoldoutGroup("SNAP SHOT/DEBUG SNAP SHOT")]
    public AudioClip TestSnapShotClip;

    [FoldoutGroup("SNAP SHOT/DEBUG SNAP SHOT")]
    [Button(ButtonSizes.Medium), GUIColor(0, 1, 0, 1f)]
    public void CreatSnapShotID()
    {
        SetSnapShotID();
    }

    [FoldoutGroup("SNAP SHOT/DEBUG SNAP SHOT")]
    [Button(ButtonSizes.Medium), GUIColor(0, 1, 0, 1f)]
    [FoldoutGroup("SNAP SHOT/DEBUG SNAP SHOT")]
    public void StartMusicForSnapShot()
    {
        StartMusicTestSnapShot(numberSnapShot);
    }

    [FoldoutGroup("SNAP SHOT/DEBUG SNAP SHOT")]
    [Button(ButtonSizes.Medium), GUIColor(0, 1, 0, 1f)]
    public void DoFadeSnapShot()
    {
        DoSnapShotFade(numberSnapShot);
    }

    #endregion


    //Regione per i fade
    #region SystemFade

    [FoldoutGroup("FADE"), Space(10)]
	public AudioSource Source1;
	[FoldoutGroup("FADE")]
	public AudioSource Source2;

	#region SimpleFade

	[FoldoutGroup("FADE/SIMPLE FADE")]
    [Title("SIMPLE FADE")]
	public Fade[] systemFade;

	/*[FoldoutGroup("FADE/SIMPLE FADE/DEBUG FADE"), Space(15)]
    public int numberFade = 0;*/

	/*[FoldoutGroup("FADE/SIMPLE FADE/DEBUG FADE")]
    [Button(ButtonSizes.Medium), GUIColor(0, 1, 0, 1f)]
    public void StartMusicForFade()
    {
        CheckValidation(numberFade);
    }*/

	/*[FoldoutGroup("FADE/SIMPLE FADE/DEBUG FADE")]
    [Button(ButtonSizes.Medium), GUIColor(0, 1, 0, 1f)]
    public void DoFade()
    {
        StartCoroutine(DoMixerFade(numberFade));
    }*/

	[FoldoutGroup("FADE/SIMPLE FADE/DEBUG FADE")]
    [Button(ButtonSizes.Medium), GUIColor(0, 1, 0, 1f)]
    public void CreatFadeID()
    {
		SetFadeID ();
    }

	#endregion

	#region ClusterFade

	[FoldoutGroup("FADE/CLUSTER FADE")]
	[Title("CLUSTER FADE")]
	public ClusterFade[] systemClusterFade;
	[FoldoutGroup("FADE/CLUSTER FADE/DEBUG CLUSTER FADE")]
	[Button(ButtonSizes.Medium), GUIColor(0, 1, 0, 1f)]
	public void CreatClusterFadeID()
	{
		SetClusterFadeID ();
	}


	#endregion


    #endregion


    //Regione per le musiche di gioco
    #region Mixer

    [InfoBox("In questa sezione si inseriscono tutti i suoni e le canzoni di giochi collegate al rispettivo AUDIOSOURCE di riferimento")]

    [FoldoutGroup("MUSIC"), Space(10)]
    [Title("GESTIONE DEI SUONI DI GIOCO")]
    public Mixer[] systemMixer;

    #endregion


    //Regione per i suoni one shot
    #region OneShot

    [FoldoutGroup("ONE SHOT")]
    [Title("PLAY LIST ONE SHOT")]
    [InfoBox("Questa sezione del plugin è riservata all'utilizzo di suoni di sistema come per esempio: \n" +
    "Suoni della Canvas \n" +
    "Suoni di pop-up")]
    [InfoBox("!!! SCONSIGLIATO L'UTILIZZO DI MUSICA IN QUESTA SEZIONE !!!", InfoMessageType.Warning)]
    public Track[] PlayList;
    [Space(30)]

	//-----------------------------------------------------------------------------------------------------------------

    [FoldoutGroup("ONE SHOT/DEBUG")]
    [Header("Utilizzato genericamente per i suoni one shot")]
    public AudioSource genericAudioSource;
    [FoldoutGroup("ONE SHOT/DEBUG")]
    [Range(0f, 1f)]
    public float allVolumePlayList;

    [FoldoutGroup("ONE SHOT/DEBUG")]
    [Button(ButtonSizes.Medium), GUIColor(0, 1, 0, 1f)]
    private void SetVolume()
    {
        SetVolumeAllPlayList();
    }

    [FoldoutGroup("ONE SHOT/DEBUG")]
    [Button(ButtonSizes.Medium), GUIColor(0, 1, 0, 1f)]
    private void SetAudioSource()
    {
        SetTheSameAudioSource();
    }

    [FoldoutGroup("ONE SHOT/DEBUG")]
    [Button(ButtonSizes.Medium), GUIColor(0, 1, 0, 1f)]
    private void InizializzaID()
    {
        CreatID();
    }

    #endregion


    #endregion

    #region Inspector

    /// <summary>
    /// Metodo che setta da Inspector il valore degli ID di tutta la PlayList
    /// </summary>
    private void CreatID()
    {

        for (int i = 0; i < PlayList.Length; i++)
        {

            PlayList[i].ID = i;

        }

        Debug.Log("ID INIZIALIZZAT");

    }

    /// <summary>
    /// Cambia da editor tutti i valori delle tracce della play list
    /// </summary>
    private void SetVolumeAllPlayList()
    {

        for (int i = 0; i < PlayList.Length; i++)
        {

            PlayList[i].volume = allVolumePlayList;

        }

        Debug.Log("VOLUMI SETTATI");

    }

    /// <summary>
    /// Metodo che setta a tutti i suoni della play lost lo stesso audio source
    /// </summary>
    private void SetTheSameAudioSource()
    {

        for (int i = 0; i < PlayList.Length; i++)
        {

            PlayList[i].output = genericAudioSource;

        }

    }

    /// <summary>
    /// Metodo statico che inzializza l'audio source del plugin
    /// </summary>
    private static void InizializeGenericAudioSource()
    {

        //genericOutput = this.gameObject.GetComponent<AudioSource> ();

    }

    #endregion

    #region Private

    private int ID_suono;
    public static AudioSource genericOutput;
	private bool findClusterAudioSource = false;
	private string nameClusterAudioSource = "";

	//ClusterFade
	private int audioSourceStopped = 0;
	private bool pauseClusterFade = false;

    #endregion

    #region Class

    [Serializable]
    public class SnapShot
    {

        [TabGroup("New Group", "Closed")]
        [ReadOnly]
        public int ID;
        [TabGroup("New Group", "Technical")]
        [Header("Inserire i parametri per il setting dello Snap Shot")]
        public float TimeFade;
        [TabGroup("New Group", "Technical")]
        public string NameGroup;
        [TabGroup("New Group", "Technical")]
        public AudioMixerSnapshot AudioSnapShot;

    }

	[Serializable]
	public class ClusterFade
	{

		[FoldoutGroup("$NameCluster")]
		public string clusterName;
		[FoldoutGroup("$NameCluster")]
		[ReadOnly]
		public int ID;
		[Space(10)]
		[FoldoutGroup("$NameCluster")]
		public FadeOfCluster[] clusterFade;

		#region MetodiCluster

		/// <summary>
		/// Metodo che ritorna il nome del cluster
		/// </summary>
		/// <returns>The track.</returns>
		public string NameCluster()
		{

			if (clusterName == "")
			{

				return "#NO NAME";

			}
			else
			{

				return clusterName;

			}


		}

		#endregion

	}

	[Serializable]
	public class FadeOfCluster
	{

		[TabGroup("New Group", "Closed")]
		[ReadOnly]
		public int ID;
		[TabGroup("New Group", "Closed")]
		[ReadOnly]
		public bool isInPlay = false;
		[TabGroup("New Group", "Console")]
		[Header("Inserire i parametri per il Fade")]
		[Range(0f, 1f)]
		[ReadOnly]
		public float Mixer1Volume = 1f;
		[TabGroup("New Group", "Console")]
		[Range(0f, 1f)]
		[ReadOnly]
		public float Mixer2Volume = 0f;
		[TabGroup("New Group", "Technical")]
		[Header("Inserire i parametri per ogni traccia")]
		public AudioClip clip;
		[TabGroup("New Group", "Technical")]
		public float TimeFade;
		[TabGroup("New Group", "Trigger")]
		[Header("Inserire i trigger per avviare il prossimo fade")]
		public int NumberLoop = 1;
		[TabGroup("New Group", "Trigger")]
		public bool forceNext = false;
		[TabGroup("New Group", "Trigger")]
		public bool forcePrevious = false;

	}

    [Serializable]
    public class Fade
    {


		[TabGroup("New Group", "Closed")]
		[ReadOnly]
		public int ID;
		[TabGroup("New Group", "Console")]
		[Header("Inserire i parametri per il Fade")]
		[Range(0f, 1f)]
		[ReadOnly]
		public float Mixer1Volume = 1f;
		[TabGroup("New Group", "Console")]
		[Range(0f, 1f)]
		[ReadOnly]
		public float Mixer2Volume = 0f;
		[TabGroup("New Group", "Technical")]
		[Header("Inserire i parametri per ogni traccia")]
		public AudioClip clip;
		[TabGroup("New Group", "Technical")]
		public float TimeFade;

    }

    /// <summary>
    /// Classe che contiene tutti i parametri delle musiche del gioco basati su Mixer
    /// </summary>
    [Serializable]
    public class Mixer
    {
        [FoldoutGroup("$NameMixer")]
        [FoldoutGroup("$NameMixer")]
        [Header("Audio mixer")]
        [GUIColor(0.3f, 0.8f, 0.8f, 1f)]
        public AudioMixer mixer;
        [FoldoutGroup("$NameMixer")]
        [Header("Lista di tracce audio")]
        public TrackMusic[] listTrack;

        [FoldoutGroup("$NameMixer/DEBUG")]
        [Space(30)]
        [Range(0f, 1f)]
        public float allVolumePlayList;

        [FoldoutGroup("$NameMixer/DEBUG")]
        [Button(ButtonSizes.Medium), GUIColor(0, 1, 0, 1f),]
        private void InizializzaID()
        {
            CreatID();
        }

        [FoldoutGroup("$NameMixer/DEBUG")]
        [Button(ButtonSizes.Medium), GUIColor(0, 1, 0, 1f)]
        private void SetVolume()
        {
            SetVolumeAllPlayList();
        }


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

            Debug.Log("ID INIZIALIZZATI");

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

            Debug.Log("VOLUMI SETTATI");

        }

        /// <summary>
        /// Metodo che ritorna il nome del mixer
        /// </summary>
        /// <returns>The mixer.</returns>
        public string NameMixer()
        {

            if (mixer == null)
            {

                return "#NO NAME";

            }
            else
            {

                return mixer.name;

            }

        }

        #endregion

    }

    /// <summary>
    /// Classe che definisce il suono da riprodurre
    /// </summary>
    [Serializable]
    public class Track
    {
        [FoldoutGroup("$NameTrack")]
        [InlineButton("Play")]
        [Header("Suono")]
        [GUIColor(0.3f, 0.8f, 0.8f, 1f)]
        public AudioClip clip;
        [FoldoutGroup("$NameTrack")]
        [Header("ID univoco della canzone numerico crescente")]
        [ReadOnly]
        public int ID;
        [FoldoutGroup("$NameTrack")]
        [Header("Descrizione del suono")]
        [TextArea]
        public string description;
        [FoldoutGroup("$NameTrack")]
        [Header("Volume del suono")]
        [Range(0f, 1f)]
        public float volume = 0.5f;
        [FoldoutGroup("$NameTrack")]
        [Header("Audio source di uscita")]
        public AudioSource output;


        #region Metodi Track

        /// <summary>
        /// Metodo che permette di ascoltare da editor il suono
        /// </summary>
        public void Play()
        {

            Musica2.InizializeGenericAudioSource();
            genericOutput.PlayOneShot(clip);

   
        }

        /// <summary>
        /// Metodo che ritorna il nome della track
        /// </summary>
        /// <returns>The track.</returns>
        public string NameTrack()
        {

            if (clip == null)
            {

                return "#NO NAME";

            }
            else
            {

                return clip.name;

            }


        }

        #endregion

    }

    /// <summary>
    /// Classe che definisce la musica da riprodurre
    /// </summary>
    [Serializable]
    public class TrackMusic
    {
        [FoldoutGroup("$NameTrack")]
        [InlineButton("Play")]
        [InlineButton("Stop")]
        [Header("Suono")]
        [GUIColor(0.3f, 0.8f, 0.8f, 1f)]
        public AudioClip clip;
        [FoldoutGroup("$NameTrack")]
        [ReadOnly]
        [Header("ID univoco della canzone numerico crescente")]
        public int ID;
        [FoldoutGroup("$NameTrack")]
        [Header("Descrizione del suono")]
        [TextArea]
        public string description;
        [FoldoutGroup("$NameTrack")]
        [Header("Volume del suono")]
        [Range(0f, 1f)]
        public float volume = 0.5f;
        [FoldoutGroup("$NameTrack")]
        [Header("Audio source di uscita")]
        public AudioSource output;


        #region Metodi Track

        /// <summary>
        /// Metodo che avvia la canzone da editor
        /// </summary>
        public void Play()
        {

            Musica2.InizializeGenericAudioSource();
            genericOutput.clip = clip;
            genericOutput.Play();

        }

        /// <summary>
        /// Metodo che ferma la canzona da editor
        /// </summary>
        public void Stop()
        {

            Musica2.InizializeGenericAudioSource();
            genericOutput.Stop();

        }

        /// <summary>
        /// Metodo che ritorna il nome della track
        /// </summary>
        /// <returns>The track.</returns>
        public string NameTrack()
        {

            if (clip == null)
            {

                return "#NO NAME";

            }
            else
            {

                return clip.name;

            }


        }

        #endregion

    }


    #endregion

    void Awake()
    {

        genericOutput = gameObject.GetComponent<AudioSource>();

    }

    #region GO

    /// <summary>
    /// Metodo che riproduce l'audio passando l'ID della canzone della lista
    /// </summary>
    /// <param name="ID">ID della canzone da riprodurre</param>
    public void GoPlayOneShot(int ID)
    {

        if (ID < PlayList.Length)
        {

			float SFXvolume = ES2.Load<float> ("Setting.txt?tag=" + master.name);

			if (SFXvolume == -80) {

				genericOutput.PlayOneShot (PlayList [ID].clip, 0f);

			} 
			else if (SFXvolume == 0) 
			{

				genericOutput.PlayOneShot (PlayList [ID].clip, PlayList [ID].volume);

			}
			else 
			{

                //float currentVolume =  1.0f + ((SFXvolume / 4)/10);
                float currentVolume = PlayList[ID].volume + ((SFXvolume / 4) / 10);

                genericOutput.PlayOneShot (PlayList [ID].clip, currentVolume);

			}

        }
        else
        {

            Debug.LogError("Muisca non trovata CONTROLLARE LISTA");

        }

    }

    /// <summary>
    /// Metodo che riproduce una canzone
    /// </summary>
    /// <param name="mixerID">Mixer I.</param>
    /// <param name="musicID">Music I.</param>
    public void GoStartMusic(int mixerID, int trackID)
    {

        if (mixerID < systemMixer.Length)
        {

            if (trackID < systemMixer[mixerID].listTrack.Length)
            {

				systemMixer [mixerID].listTrack [trackID].output.volume = systemMixer [mixerID].listTrack [trackID].volume;
				systemMixer [mixerID].listTrack [trackID].output.clip = systemMixer [mixerID].listTrack [trackID].clip;
				systemMixer[mixerID].listTrack[trackID].output.Play();
            }

        }
        else
        {

            Debug.Log("Muisca non trovata CONTROLLARE LISTA");

        }

    }

    /// <summary>
    /// Metodo che riproduce una canzone
    /// </summary>
    /// <param name="mixerID">Mixer I.</param>
    /// <param name="musicID">Music I.</param>
    public void GoStartMusic(string nameMixer, int trackID)
    {

        for (int i = 0; i < systemMixer.Length; i++)
        {

            if (systemMixer[i].mixer.name == nameMixer)
            {

                if (trackID < systemMixer[i].listTrack.Length)
                {
					systemMixer [i].listTrack [trackID].output.volume = systemMixer [i].listTrack [trackID].volume;
					systemMixer [i].listTrack [trackID].output.clip = systemMixer [i].listTrack [trackID].clip;
					systemMixer[i].listTrack[trackID].output.Play();
                }

                break;

            }

        }
   
        Debug.Log("Muisca non trovata CONTROLLARE LISTA");

    }

    /// <summary>
    /// Metodo che stoppa una canzone
    /// </summary>
    /// <param name="mixerID">Mixer I.</param>
    /// <param name="musicID">Music I.</param>
    public void GoStopMusic(int mixerID, int trackID)
    {

        if (mixerID < systemMixer.Length)
        {

            if (trackID < systemMixer[mixerID].listTrack.Length)
            {

				systemMixer[mixerID].listTrack[trackID].output.Stop();
            }

        }
        else
        {

            Debug.Log("Muisca non trovata CONTROLLARE LISTA");

        }

    }

    /// <summary>
    /// Metodo che riproduce una canzone
    /// </summary>
    /// <param name="mixerID">Mixer I.</param>
    /// <param name="musicID">Music I.</param>
    public void GoStopMusic(string nameMixer, int trackID)
    {

        for (int i = 0; i < systemMixer.Length; i++)
        {

            if (systemMixer[i].mixer.name == nameMixer)
            {

                if (trackID < systemMixer[i].listTrack.Length)
                {
					systemMixer[i].listTrack[trackID].output.Stop();
                }

                break;

            }

        }
   
        Debug.Log("Muisca non trovata CONTROLLARE LISTA");

    }

    /// <summary>
    /// Metodo che esegue il fade 
    /// </summary>
    /// <param name="ID">I.</param>
    public void GoMusicFade(int ID)
    {

		StopAllCoroutines ();

        StartCoroutine(DoMixerFade(ID));

    }

	/// <summary>
	/// Metodo che esegue il fade di un clister fade
	/// </summary>
	/// <param name="clusterID">Cluster I.</param>
	/// <param name="fadeID">Fade I.</param>
	public void GoClusterFade (int clusterID)
	{

		/*if (Source1.isPlaying == true) 
		{
			Source2.clip = systemClusterFade [clusterID].clusterFade [0].clip;
			Source2.Play ();
		} 
		else 
		{
			Source1.clip = systemClusterFade [clusterID].clusterFade [0].clip;
			Source1.Play ();
		}*/

		StopAllCoroutines ();

		StartCoroutine (DoClusterMixerFade (clusterID, 0));
		//Dico che questa canzone è in play
		systemClusterFade [clusterID].clusterFade [0].isInPlay = true;
		StartCoroutine (DoListenClusterTrigger (clusterID, 0));

	}

	public void GoRandomClusterFade(int clusterID)
	{

		StopAllCoroutines ();

		int IDsong = UnityEngine.Random.Range(0,systemClusterFade[clusterID].clusterFade.Length);

		StartCoroutine (DoClusterMixerFade (clusterID, IDsong ));
		//Dico che questa canzone è in play
		systemClusterFade [clusterID].clusterFade [IDsong].isInPlay = true;
		StartCoroutine (DoListenClusterTrigger (clusterID, IDsong));

	}

	public void GoNextMusicClusterFade(int IDcluster)
	{

		for (int i = 0; i < systemClusterFade [IDcluster].clusterFade.Length; i++) 
		{

			if (systemClusterFade [IDcluster].clusterFade [i].isInPlay == true) 
			{

				//Per sicurezza
				systemClusterFade [IDcluster].clusterFade [i].forcePrevious = false;
				systemClusterFade [IDcluster].clusterFade [i].forceNext = true;

			}

		}

	}

	public void GoPreviousMusicClusterFade(int IDcluster)
	{

		for (int i = 0; i < systemClusterFade [IDcluster].clusterFade.Length; i++) 
		{

			if (systemClusterFade [IDcluster].clusterFade [i].isInPlay == true) 
			{

				//Per sicurezza
				systemClusterFade [IDcluster].clusterFade [i].forceNext = false;
				systemClusterFade [IDcluster].clusterFade [i].forcePrevious = true;

			}

		}

	}

	public void GoPauseClusterFade(int IDcluster)
	{

		if (Source1.isPlaying == true) {

			Source1.Pause ();
			audioSourceStopped = 1;
			pauseClusterFade = true;

		} else if (Source2.isPlaying == true) {

			Source2.Pause ();
			audioSourceStopped = 2;
			pauseClusterFade = true;

		} else {

			Debug.Log ("NESSUN PLAYER IN ESECUZIONE DEL CLUSTER FADE");
			audioSourceStopped = 0;
		}


	}

	public void GoPlayClusterFade (int IDcluster)
	{

		if (audioSourceStopped == 1) {

			Source1.Play ();
			pauseClusterFade = false;

		} else if (audioSourceStopped == 2) {

			Source2.Play ();
			pauseClusterFade = false;

		} else {

			Debug.Log ("NESSUN PLAYER IN ESECUZIONE DEL CLUSTER FADE");
			audioSourceStopped = 0;

		}

	}

    /// <summary>
    /// Gos the snap shot fade.
    /// </summary>
    /// <param name="ID">I.</param>
    public void GoSnapShotFade(int ID)
    {

        if (ID < systemSnapShot.Length)
        {
			
            systemSnapShot[ID].AudioSnapShot.TransitionTo(systemSnapShot[ID].TimeFade);

        }
        else
        {

            Debug.Log("Snap Shot non trovato CONTROLLARE LISTA");

        }

    }


    #endregion


	#region Information Music

	public string InfoClusterFadeNameSongInPlay(int IDcluster)
	{

		for (int i = 0; i < systemClusterFade [IDcluster].clusterFade.Length; i++) 
		{

			if (systemClusterFade [IDcluster].clusterFade [i].isInPlay == true) 
			{

				return systemClusterFade [IDcluster].clusterFade [i].clip.name;

			}

		}

		return null;
	}

	public string InfoClsuterFadeNamePreviousSong(int IDcluster)
	{


		for (int i = 0; i < systemClusterFade [IDcluster].clusterFade.Length; i++) 
		{

			if (systemClusterFade [IDcluster].clusterFade [i].isInPlay == true) 
			{


				if (i - 1 < 0) 
				{

					return systemClusterFade [IDcluster].clusterFade [systemClusterFade[IDcluster].clusterFade.Length-1].clip.name;

				} 
				else 
				{

					return systemClusterFade [IDcluster].clusterFade [i-1].clip.name;

				}



			}

		}


		return null;

	}

	public string InfoClusterFadeNameNextSong(int IDcluster)
	{


		for (int i = 0; i < systemClusterFade [IDcluster].clusterFade.Length; i++) 
		{

			if (systemClusterFade [IDcluster].clusterFade [i].isInPlay == true) {


				if (i + 1 > systemClusterFade [IDcluster].clusterFade.Length - 1) 
				{

					return systemClusterFade [IDcluster].clusterFade [0].clip.name;

				} 
				else 
				{

					return systemClusterFade [IDcluster].clusterFade [i + 1].clip.name;

				}



			}

		}

		return null;
	}

	#endregion


    #region Audio Mixer

    /// <summary>
    /// Metodo che regolare il volume del master audio mixer
    /// </summary>
    /// <param name="value">Value.</param>
    public void SetMasterVolumeMixer(float value)
    {

        master.SetFloat("master", value);

    }

    /// <summary>
    /// Metodo che aggiorna il volume di un mixer
    /// </summary>
    /// <param name="name">Name.</param>
    /// <param name="value">Value.</param>
    public void SetVolumeMixer(string name, float value)
    {

        for (int i = 0; i < systemMixer.Length; i++)
        {

            if (systemMixer[i].mixer.name == name)
            {

                systemMixer[i].mixer.SetFloat("master", value);

            }

        }

        Debug.Log("Mixer non trovato");

    }

    #endregion


    #region Fade

	#region SimpleFade

    /// <summary>
    /// Metodo che setta gli id della lista dei Fade
    /// </summary>
    public void SetFadeID()
    {

        for (int i = 0; i < systemFade.Length; i++)
        {

            systemFade[i].ID = i;

        }

        Debug.Log("FADE ID inizializzati");

    }

	/*
    /// <summary>
    /// Metodo che controlla l'attendibilità dei valori delle tracce	/// </summary>
    /// <returns><c>true</c>, if validation was checked, <c>false</c> otherwise.</returns>
    /// <param name="mixerValue1">Mixer value1.</param>
    /// <param name="trackValue1">Track value1.</param>
    /// <param name="mixerValue2">Mixer value2.</param>
    /// <param name="trackValue2">Track value2.</param>
    public void CheckValidation(int ID)
    {
        systemFade[ID].CD1.volume = 1;
        systemFade[ID].CD2.volume = 0;

        systemFade[ID].CD1.Play();
        systemFade[ID].CD2.Play();

    }*/

	/// <summary>
	/// Metodo che esegue il fade tra due canzoni
	/// </summary>
	public IEnumerator DoMixerFade(int ID)
	{

		systemFade[ID].Mixer1Volume = 1;
		systemFade[ID].Mixer2Volume = 0;

		if (Source1.isPlaying == true) 
		{

			Source2.clip = systemFade [ID].clip;
			Source2.Play ();

			while (systemFade[ID].Mixer1Volume > 0)
			{

				Source1.volume -= Time.deltaTime / systemFade[ID].TimeFade;
				systemFade[ID].Mixer1Volume = Source1.volume;
				Source2.volume += Time.deltaTime / systemFade[ID].TimeFade;
				systemFade[ID].Mixer2Volume = Source2.volume;


				yield return null;

			}

			Source1.Stop ();

		} 
		else 
		{

			Source1.clip = systemFade[ID].clip;
			Source1.Play ();

			while (systemFade[ID].Mixer1Volume > 0)
			{

				Source1.volume += Time.deltaTime / systemFade[ID].TimeFade;
				systemFade[ID].Mixer1Volume = Source2.volume;
				Source2.volume -= Time.deltaTime / systemFade[ID].TimeFade;
				systemFade[ID].Mixer2Volume = Source1.volume;


				yield return null;

			}

			Source2.Stop ();

		}


		yield return null;


	}
			

	/*

    /// <summary>
    /// Metodo che esegue il fade tra due canzoni
    /// </summary>
    public IEnumerator DoMixerFade(int ID)
    {

        //Setto gli audio source prima di inizare il fade
        //poi li cercherò dentro alle liste per avere l'esatto valore del loro volume iniziale per evitare sbalza da ex: 0.7 a 1 di volume di settaggio

        if (systemFade[ID].CD1.volume > systemFade[ID].CD2.volume)
        {
            systemFade[ID].CD1.volume = 1;
            systemFade[ID].CD2.volume = 0;

            while (systemFade[ID].CD1.volume > 0)
            {

                systemFade[ID].CD1.volume -= Time.deltaTime / systemFade[ID].TimeFade;
                systemFade[ID].Mixer1Volume = systemFade[ID].CD1.volume;
                systemFade[ID].CD2.volume += Time.deltaTime / systemFade[ID].TimeFade;
                systemFade[ID].Mixer2Volume = systemFade[ID].CD2.volume;


                yield return null;

            }

        }
        else
        {


            systemFade[ID].CD1.volume = 0;
            systemFade[ID].CD2.volume = 1;

            while (systemFade[ID].CD2.volume > 0)
            {

                systemFade[ID].CD1.volume += Time.deltaTime / systemFade[ID].TimeFade;
                systemFade[ID].Mixer1Volume = systemFade[ID].CD1.volume;
                systemFade[ID].CD2.volume -= Time.deltaTime / systemFade[ID].TimeFade;
                systemFade[ID].Mixer2Volume = systemFade[ID].CD2.volume;


                yield return null;

            }

        }
		


        yield return null;

    }

    /// <summary>
    /// Metodo che esegue il fade tra due canzoni con la possibilità di metterle in pausa
    /// </summary>
    public IEnumerator DoMixerFade(int ID, bool pause)
    {

        //Setto gli audio source prima di inizare il fade
        //poi li cercherò dentro alle liste per avere l'esatto valore del loro volume iniziale per evitare sbalza da ex: 0.7 a 1 di volume di settaggio

        if (systemFade[ID].CD1.volume > systemFade[ID].CD2.volume)
        {
            systemFade[ID].CD1.volume = 1;
            systemFade[ID].CD2.volume = 0;

            while (systemFade[ID].CD1.volume > 0)
            {

                systemFade[ID].CD1.volume -= Time.deltaTime / systemFade[ID].TimeFade;
                systemFade[ID].Mixer1Volume = systemFade[ID].CD1.volume;
                systemFade[ID].CD2.volume += Time.deltaTime / systemFade[ID].TimeFade;
                systemFade[ID].Mixer2Volume = systemFade[ID].CD2.volume;


                yield return null;

            }

            if (pause == true)
                systemFade[ID].CD1.Pause();

        }
        else
        {


            systemFade[ID].CD1.volume = 0;
            systemFade[ID].CD2.volume = 1;

            while (systemFade[ID].CD2.volume > 0)
            {

                systemFade[ID].CD1.volume += Time.deltaTime / systemFade[ID].TimeFade;
                systemFade[ID].Mixer1Volume = systemFade[ID].CD1.volume;
                systemFade[ID].CD2.volume -= Time.deltaTime / systemFade[ID].TimeFade;
                systemFade[ID].Mixer2Volume = systemFade[ID].CD2.volume;


                yield return null;

            }

            if (pause == true)
                systemFade[ID].CD2.Pause();

        }



        yield return null;

    }

	*/

	#endregion

	#region ClusterFade

	/// <summary>
	/// Metodo che setta gli id dei cluster fade
	/// </summary>
	public void SetClusterFadeID()
	{

		for (int i = 0; i < systemClusterFade.Length; i++) 
		{

			systemClusterFade [i].ID = i; 

			for (int j = 0; j < systemClusterFade [i].clusterFade.Length; j++) 
			{

				systemClusterFade [i].clusterFade [j].ID = j;

			}
		}

	}

	/// <summary>
	/// Metodo che esegue il fade di un cluster tra due canzoni
	/// </summary>
	public IEnumerator DoClusterMixerFade(int IDcluster, int IDfade)
	{

		systemClusterFade [IDcluster].clusterFade [IDfade].Mixer1Volume = 1;
		systemClusterFade [IDcluster].clusterFade [IDfade].Mixer2Volume = 0;

		if (Source1.isPlaying == true) {

			Source2.clip = systemClusterFade [IDcluster].clusterFade [IDfade].clip;
			Source2.Play ();

			//Diciamo al Listener che abbiamo trovato l'audio source per questa canzone
			findClusterAudioSource = true;
			nameClusterAudioSource = Source2.name;

			while (systemClusterFade [IDcluster].clusterFade [IDfade].Mixer1Volume > 0) {

				Source1.volume -= Time.deltaTime / systemClusterFade [IDcluster].clusterFade [IDfade].TimeFade;
				systemClusterFade [IDcluster].clusterFade [IDfade].Mixer1Volume = Source1.volume;
				Source2.volume += Time.deltaTime / systemClusterFade [IDcluster].clusterFade [IDfade].TimeFade;
				systemClusterFade [IDcluster].clusterFade [IDfade].Mixer2Volume = Source2.volume;


				yield return null;

			}

			Source1.Stop ();

		} 
		else 
		{

			Source1.clip = systemClusterFade [IDcluster].clusterFade [IDfade].clip;
			Source1.Play ();

			//Diciamo al Listener che abbiamo trovato l'audio source per questa canzone
			findClusterAudioSource = true;
			nameClusterAudioSource = Source1.name;

			while (systemClusterFade [IDcluster].clusterFade [IDfade].Mixer1Volume > 0) {

				Source1.volume += Time.deltaTime / systemClusterFade [IDcluster].clusterFade [IDfade].TimeFade;
				systemClusterFade [IDcluster].clusterFade [IDfade].Mixer1Volume = Source2.volume;
				Source2.volume -= Time.deltaTime / systemClusterFade [IDcluster].clusterFade [IDfade].TimeFade;
				systemClusterFade [IDcluster].clusterFade [IDfade].Mixer2Volume = Source1.volume;


				yield return null;

			}

			Source2.Stop ();

		} 


		yield return null;

	}

	/// <summary>
	/// Metodo che esegue il fade di un cluster tra due canzoni
	/// </summary>
	public IEnumerator DoClusterMixerFade(string clusterName, int IDfade)
	{

		int IDcluster = SearchClusterFadeID (clusterName);

		if (IDcluster == -1) 
		{
			
			systemClusterFade [IDcluster].clusterFade [IDfade].Mixer1Volume = 1;
			systemClusterFade [IDcluster].clusterFade [IDfade].Mixer2Volume = 0;

			if (Source1.isPlaying == true) 
			{

				Source2.clip = systemClusterFade [IDcluster].clusterFade [IDfade].clip;
				Source2.Play ();

				//Diciamo al Listener che abbiamo trovato l'audio source per questa canzone
				findClusterAudioSource = true;
				nameClusterAudioSource = Source2.name;

				while (systemClusterFade [IDcluster].clusterFade [IDfade].Mixer1Volume > 0) {

					Source1.volume -= Time.deltaTime / systemClusterFade [IDcluster].clusterFade [IDfade].TimeFade;
					systemClusterFade [IDcluster].clusterFade [IDfade].Mixer1Volume = Source1.volume;
					Source2.volume += Time.deltaTime / systemClusterFade [IDcluster].clusterFade [IDfade].TimeFade;
					systemClusterFade [IDcluster].clusterFade [IDfade].Mixer2Volume = Source2.volume;


					yield return null;

				}

				Source1.Stop ();

			} 
			else 
			{

				Source1.clip = systemClusterFade [IDcluster].clusterFade [IDfade].clip;
				Source1.Play ();

				//Diciamo al Listener che abbiamo trovato l'audio source per questa canzone
				findClusterAudioSource = true;
				nameClusterAudioSource = Source1.name;

				while (systemClusterFade [IDcluster].clusterFade [IDfade].Mixer1Volume > 0) 
				{

					Source1.volume += Time.deltaTime / systemClusterFade [IDcluster].clusterFade [IDfade].TimeFade;
					systemClusterFade [IDcluster].clusterFade [IDfade].Mixer1Volume = Source2.volume;
					Source2.volume -= Time.deltaTime / systemClusterFade [IDcluster].clusterFade [IDfade].TimeFade;
					systemClusterFade [IDcluster].clusterFade [IDfade].Mixer2Volume = Source1.volume;

					yield return null;

				}

				Source2.Stop ();

			}
		}


		yield return null;

	}

	/// <summary>
	/// Metodo che controlla i trigger dei cluster fade
	/// </summary>
	/// <returns>The listen cluster trigger.</returns>
	/// <param name="IDcluster">I dcluster.</param>
	/// <param name="IDfade">I dfade.</param>
	public IEnumerator DoListenClusterTrigger(int IDcluster, int IDfade)
	{

		Debug.Log ("Ascolto: "+IDfade);
		Source1.loop = false;
		Source2.loop = false;

		//Variabile che gestisce il rilevamento dell'evento 
		bool isTriggered = false;

		int Nloop = systemClusterFade [IDcluster].clusterFade [IDfade].NumberLoop;

		while(findClusterAudioSource == false)
		{

			Debug.Log ("Attendo");

			yield return null;

		}
			

		if (Source1.isPlaying == true && nameClusterAudioSource == Source1.name) {

			while (isTriggered == false) {


				if (systemClusterFade [IDcluster].clusterFade [IDfade].forceNext == true) 
				{

					isTriggered = true;

				}

				if (systemClusterFade [IDcluster].clusterFade [IDfade].forcePrevious == true) 
				{

					isTriggered = true;

				}


				if (Source1.isPlaying == false && pauseClusterFade == false) 
				{

					Nloop--;
					Debug.Log (Nloop);
					Source1.Play ();

					if (Nloop < 0) 
					{

						isTriggered = true;

					}


				}

				yield return null;

			}

		} else if (Source2.isPlaying == true && nameClusterAudioSource == Source2.name) {

			while (isTriggered == false) {


				if (systemClusterFade [IDcluster].clusterFade [IDfade].forceNext == true) 
				{

					isTriggered = true;

				}

				if (systemClusterFade [IDcluster].clusterFade [IDfade].forcePrevious == true) 
				{

					isTriggered = true;

				}


				if (Source2.isPlaying == false && pauseClusterFade == false) 
				{

					Nloop--;
					Debug.Log (Nloop);
					Source1.Play ();

					if (Nloop < 0) 
					{

						isTriggered = true;

					}


				}

				yield return null;

			}

		} else {

			Debug.Log ("Errore");

		}
			

		isTriggered = false;

		//Controllo come si vuole scorrere il cluster
		if (systemClusterFade [IDcluster].clusterFade [IDfade].forcePrevious == true) 
		{

			//SCORRIAMO LA LISTA INDIETRO

			//Controllo se siamo all'estremo della lista
			if (IDfade - 1 >= 0) 
			{

				Debug.Log ("Prossima canzone: " + (IDfade - 1));

				//Resettiamo i parametri della canzone corrente
				ResetSongOnClusterFade (IDcluster, IDfade);

				findClusterAudioSource = false;
				StartCoroutine (DoClusterMixerFade (IDcluster, IDfade - 1));
				//Dico che questa canzone è in play
				systemClusterFade [IDcluster].clusterFade [IDfade - 1].isInPlay = true;
				StartCoroutine (DoListenClusterTrigger (IDcluster, IDfade - 1));

			} 
			else 
			{

				Debug.Log ("Prossima canzone: " + (systemClusterFade [IDcluster].clusterFade.Length-1));

				//Resettiamo i parametri della canzone corrente
				ResetSongOnClusterFade (IDcluster, IDfade);

				findClusterAudioSource = false;
				StartCoroutine( DoClusterMixerFade (IDcluster, systemClusterFade [IDcluster].clusterFade.Length-1));
				//Dico che questa canzone è in play
				systemClusterFade [IDcluster].clusterFade [systemClusterFade [IDcluster].clusterFade.Length-1].isInPlay = true;
				StartCoroutine( DoListenClusterTrigger (IDcluster,systemClusterFade [IDcluster].clusterFade.Length-1));

			}

		} 
		else 
		{

			//SCORRIAMO LA LISTA IN AVANTI

			//Controllo se siamo all'estremo della lista
			if(IDfade + 1 < systemClusterFade [IDcluster].clusterFade.Length)
			{

				Debug.Log ("Prossima canzone: " + (IDfade + 1));

				//Resettiamo i parametri della canzone corrente
				ResetSongOnClusterFade (IDcluster, IDfade);

				findClusterAudioSource = false;
				StartCoroutine( DoClusterMixerFade (IDcluster, IDfade + 1));
				//Dico che questa canzone è in play
				systemClusterFade [IDcluster].clusterFade [IDfade + 1].isInPlay = true;
				StartCoroutine( DoListenClusterTrigger (IDcluster, IDfade + 1));

			}
			else
			{

				Debug.Log ("Prossima canzone: " + 0);

				//Resettiamo i parametri della canzone corrente
				ResetSongOnClusterFade (IDcluster, IDfade);

				findClusterAudioSource = false;
				StartCoroutine( DoClusterMixerFade (IDcluster, 0));
				//Dico che questa canzone è in play
				systemClusterFade [IDcluster].clusterFade [0].isInPlay = true;
				StartCoroutine( DoListenClusterTrigger (IDcluster,0));

			}

		}
			

		yield return null;

	}

	/// <summary>
	/// Metodo che ritorna l'id del cluster
	/// </summary>
	/// <returns>The cluster fade I.</returns>
	/// <param name="clusterName">Cluster name.</param>
	public int SearchClusterFadeID(string clusterName)
	{

		for (int i = 0; i < systemClusterFade.Length; i++) 
		{

			if (systemClusterFade [i].clusterName == clusterName) 
			{

				return i;

			}

		}

		Debug.Log ("Il nome del cluster non esiste");
		return -1;

	}

	/// <summary>
	/// Metodo che resetta i parametri della canzone
	/// </summary>
	private void ResetSongOnClusterFade(int IDcluster, int IDfade)
	{

		//Dico che la canzone terminata non è più in play
		systemClusterFade [IDcluster].clusterFade [IDfade].isInPlay = false;
		//Resetto il forceNext
		systemClusterFade [IDcluster].clusterFade [IDfade].forceNext = false;
		//Resetto il focePrevious
		systemClusterFade [IDcluster].clusterFade [IDfade].forcePrevious = false;

	}

	#endregion

    #endregion


    #region SnapShot

    /// <summary>
    /// Metodo che inizializza gli id della lista di SnapShot
    /// </summary>
    public void SetSnapShotID()
    {

        for (int i = 0; i < systemSnapShot.Length; i++)
        {

            systemSnapShot[i].ID = i;

        }

        Debug.Log("SNAP SHOT ID inizializzati");

    }

    /// <summary>
    /// Metodo che avvia una clip per il testing di uno snap shot
    /// </summary>
    public void StartMusicTestSnapShot(int ID)
    {

        //Settimao al nostro default audio source il mixer per il testing 
        genericOutput.outputAudioMixerGroup = systemSnapShot[ID].AudioSnapShot.audioMixer.FindMatchingGroups(systemSnapShot[ID].NameGroup)[0];
        genericOutput.clip = TestSnapShotClip;
        genericOutput.Play();

    }

    /// <summary>
    /// Metodo che testa lo Snap Shot Fade
    /// </summary>
    /// <param name="ID">I.</param>
    public void DoSnapShotFade(int ID)
    {

        systemSnapShot[ID].AudioSnapShot.TransitionTo(systemSnapShot[ID].TimeFade);

    }

    #endregion

}

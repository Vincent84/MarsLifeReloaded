using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ossigeno : MonoBehaviour {

	#region Public
	[Header("Ossigeno in percentuale")]
	public int O2;
	[Header("Tempo per di durata per ogni percentuale")]
	public float timeRange;
	[Header("Barra di ossigeno")]
	public Slider sliderO2;  
	[Header("Immagine della barra dell'ossigeno")]
	public Image bar;
	[Header("Colore della barra")]
	public Color colorBar;
	[Header("Testo della bombola di ossigeno")]
	public Text textO2;
	[Header("Audio per l'ossigeno")]
	public AudioSource source;
	public AudioClip ossigeno1;
	public AudioClip ossigeno2;

	#endregion

	#region Private
	private float timer = 0; 
	#endregion

	void Awake()
	{

		sliderO2.value = sliderO2.maxValue;
		SetMaxO2 ();
		//source.clip = ossigeno1;
		//source.Play ();
	
	}

	void Update()
	{

		timer += Time.deltaTime;

		//Facciamo scendere il livello di ossigeno
		if (timer >= timeRange && O2 >= 1) 
		{

			timer = 0;
			O2decreases ();

		}

		//Controlliamo il livello di ossigeno e cambiamo colore
		if (O2 <= 50) 
		{

			//source.clip = ossigeno2;
			//source.Play ();
			ChangeColorOfBar ();

		}

	}

	//Perdere ossigeno nel tempo
	private void O2decreases()
	{

		O2 -= 1;
		sliderO2.value = O2;
		textO2.text = O2.ToString () + "%";

	}

	//Metodo che setta al massimo la bombola
	private void SetMaxO2()
	{

		O2 = 100;
		textO2.text = O2.ToString () + "%";

	}

	//Metodo che trasforma la barra dal colore blu al colore rosso
	private void ChangeColorOfBar()
	{

		bar.color = colorBar;

	}

	//Controlla se c'è ancora ossigeno
	public bool CheckO2()
	{

		return O2 > 0;

	}

	//Metodo che non rende visibile la barra dell'ossigeno 
	public void DisableOssigeno()
	{

		GameObject.Find("Canvas_ossigeno").layer = 20;

	}

	//Metodo che rende visibile la barra dell'ossigeno
	public void EnableOssigneo()
	{

		GameObject.Find("Canvas_ossigeno").layer = 19;


	}

}

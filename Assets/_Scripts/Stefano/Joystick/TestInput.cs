using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour {

	#region Public 
	public GameObject O2controller;
	public GameObject CompassController;
	public GameObject PauseMenu;
	#endregion

	#region Private 
	private Ossigeno O2;
	private CompassLocation compass;
	#endregion

	void Awake()
	{

		O2 = O2controller.GetComponent<Ossigeno> ();
		compass = CompassController.GetComponent<CompassLocation>();

	}
	
	// Update is called once per frame
	private void Update () 
	{

		if (InputManager.UPArrow ()) 
		{

			Debug.Log ("Disabilito la bussola");
			compass.EnableCompass ();

		}

		if (InputManager.DOWNArrow ()) {


			Debug.Log ("Attivo la bussola");
			compass.DisableCompass ();

		}

		if (InputManager.LEFTArrow ()) 
		{

			Debug.Log ("Disabilito l'ossigeno");
			O2.DisableOssigeno ();

		}

		if (InputManager.RIGHTArrow ()) 
		{

			Debug.Log ("Attivo l'ossigeno");
			O2.EnableOssigneo ();

		}

		if (InputManager.StartButton ())
		{

			Debug.Log ("Attivo il menu di start");
			PauseMenu.SetActive (true);

		}

		if (Input.GetKeyDown (KeyCode.M)) {

			Debug.Log ("Attivo il menu di start");
			PauseMenu.SetActive (true);

		}
		
	}
}

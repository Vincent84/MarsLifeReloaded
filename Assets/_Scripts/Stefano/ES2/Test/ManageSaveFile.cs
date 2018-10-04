using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

/// <summary>
/// Script che setta il valore 
/// </summary>
public class ManageSaveFile : MonoBehaviour 
{

	#region Public 

	[Header("Scena da caricare in caso di New Game")]
	public string firstScene;
	[Header("Pannello dan avviare in caso di cancellazione dati di gioco")]
	public GameObject panelChoise;

	#endregion

	#region Private 

	private string newGame;

	#endregion

	/// <summary>
	/// Settiamo un nuovo file di salvataggio
	/// </summary>
	public void SetNewSlot()
	{

		PlayerPrefs.SetString ("Slot", "Slot"+PlayerPrefs.GetInt("numberSlot").ToString());
		PlayerPrefs.SetInt ("numberSlot", PlayerPrefs.GetInt ("numberSlot") + 1);

	}

	/// <summary>
	/// Settiamo il file da cui caricheremo e salveremo i dati in questa partitia
	/// </summary>
	/// <param name="value">Value.</param>
	public void SetSlot(string value)
	{

		PlayerPrefs.SetString ("Slot", value);

	}

	/// <summary>
	/// Settiamo il nome del file che andremo a distruggere per poi ricrearlo
	/// </summary>
	/// <param name="value">Value.</param>
	public void SetNewGame(string value)
	{

		newGame = value;

		SetSlot (value);

		if (ES2.Exists (newGame + ".txt"))
			panelChoise.SetActive (true);
		else
			SceneManager.LoadScene (firstScene);

	}

}

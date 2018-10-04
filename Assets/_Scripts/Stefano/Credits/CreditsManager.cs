using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour 
{

	#region Public 

	public Animator CreditsAnimator;
	public Animator FadeAnimator;

	public CanvasGroup fadeCanvas;

	#endregion

	void Update()
	{

		if (InputManager.StartButton () == true || Input.GetKey(KeyCode.Escape) == true) 
		{

			FadeAnimator.Play ("CreditsFade");
			StartCoroutine (ChangeScene ());

		}

		if (CreditsAnimator.GetCurrentAnimatorStateInfo (0).IsName("GoCredits") == false) 
		{

			Debug.Log ("Finiti i credits");
			SceneManager.LoadScene (0);

		}

	}

	/// <summary>
	/// Coroutine per tornare al main menu
	/// </summary>
	/// <returns>The scene.</returns>
	private IEnumerator ChangeScene()
	{

		while(true)
		{

			if (fadeCanvas.alpha >= 1) 
			{

				//Carico il Main Menu
				SceneManager.LoadScene (0);

			}

			yield return null;

		}

	}

}

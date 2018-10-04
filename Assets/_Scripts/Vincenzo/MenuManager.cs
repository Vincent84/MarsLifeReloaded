using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public void StartNewGame(string sceneName)
    {
        GameManager.newGame = true;
		SceneManager.LoadScene(sceneName);
    }

	public void ExitGame()
	{
		Application.Quit ();
	}

}

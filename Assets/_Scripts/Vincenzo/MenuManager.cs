using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour {

    public AudioMixer audioMixer;

    public void SetGeneralAudio(float value)
    {
        audioMixer.SetFloat("GeneralVolume", value);
    }

    public void StartNewGame(string sceneName)
    {
        GameManager.newGame = true;
		SceneManager.LoadScene(sceneName);
    }

	public void ExitGame()
	{
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif

    }
}

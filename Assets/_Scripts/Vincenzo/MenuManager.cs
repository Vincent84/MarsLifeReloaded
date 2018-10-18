using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour {

    public AudioMixer audioMixer;

    private EventSystem eventSystem;
    public GameObject canvasEnabled;

    private void Awake()
    {
        eventSystem = GameObject.FindObjectOfType<EventSystem>();
        canvasEnabled = GameObject.Find("MainMenu");
    }

    public void SetGeneralVolume(float volume)
    {
        audioMixer.SetFloat("GeneralVolume", volume);
    }

    public void StartNewGame(string sceneName)
    {
        GameManager.newGame = true;
		SceneManager.LoadScene(sceneName);
    }

    public void SetActiveCanvas(GameObject canvasToEnable)
    {
        canvasEnabled = GameObject.Find(canvasToEnable.name);

        
        if(!canvasEnabled.GetComponentInChildren<Slider>())
        {
            eventSystem.SetSelectedGameObject(canvasEnabled.GetComponentInChildren<Button>().gameObject);
        }
        else
        {
            eventSystem.SetSelectedGameObject(canvasEnabled.GetComponentInChildren<Slider>().gameObject);
        }
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

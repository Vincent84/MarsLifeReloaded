using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class ProgressSceneLoader : MonoBehaviour
{
    // PER USARE IL LOADING BISOGNA INSERIRE IL PREFAB "ProgressSceneLoader" NELLA CARTELLA PREFAB/SIMONE E INFINE
    // INSERIRE (FindObjectOfType<ProgressSceneLoader>().LoadScene(nameScena);) AL POSTO DEL NORMALE SceneLoader

    [SerializeField][BoxGroup("Text Percentage")] private Text progressText;

    private AsyncOperation operation;
    private Canvas canvas;

    [BoxGroup("Loading Images")] public Image loadingBackground_0;
    [BoxGroup("Loading Images")] public Image loadingBackground_1;
    [BoxGroup("Loading Images")] public Image loadingBackground_2;
    [BoxGroup("Loading Images")] public Image loadingBackground_3;

    private int randomImages;

    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>(true);
        randomImages = Random.Range(0, 4);
    }

    public void LoadScene(string sceneName)
    {
        UpdateProgressUI(0);
        canvas.gameObject.SetActive(true);

        // IMAGES

        if(randomImages == 0)
            loadingBackground_0.gameObject.SetActive(true);

        else if(randomImages == 1)
            loadingBackground_1.gameObject.SetActive(true);

        else if (randomImages == 2)
            loadingBackground_2.gameObject.SetActive(true);

        else if (randomImages == 3)
            loadingBackground_3.gameObject.SetActive(true);

        StartCoroutine(BeginLoad(sceneName));
    }

    private IEnumerator BeginLoad(string sceneName)
    {
        operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            UpdateProgressUI(operation.progress);
            yield return null;
        }

        UpdateProgressUI(operation.progress);
        operation = null;
        canvas.gameObject.SetActive(false);
    }

    private void UpdateProgressUI(float progress)
    {
        progressText.text = (int)(progress * 100f) + "%";
    }
}
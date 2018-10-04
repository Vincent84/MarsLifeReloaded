using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Invector.CharacterController;

public class CinemachineManager : MonoBehaviour {

    bool cutsceneActived;
    PlayableDirector playableDirector;
	
	// Update is called once per frame
	void Update ()
    {
		if(cutsceneActived && playableDirector)
        {
            if (playableDirector.state == PlayState.Paused)
            {
                StopCutscene();
            }
        }
	}

    public void StartCutscene(GameObject cutscene)
    {
        if(cutscene)
        {
            vThirdPersonController.instance.GetComponent<GenericSettings>().LockPlayer();
            cutscene.SetActive(true);
            playableDirector = cutscene.GetComponentInChildren<PlayableDirector>(true);
            playableDirector.Play();
            cutsceneActived = true;   
        }
    }

    public void StopCutscene()
    {
        playableDirector.Stop();
        playableDirector.transform.parent.gameObject.SetActive(false);
        cutsceneActived = false;
        vThirdPersonController.instance.GetComponent<GenericSettings>().UnlockPlayer();
        Destroy(playableDirector.transform.parent.parent.gameObject);
    }

}

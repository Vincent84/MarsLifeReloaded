using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.CharacterController;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class RoverToScene : MonoBehaviour {

	[Tooltip("Write the name of the level you want to load")]
	public string levelToLoad;
	[Tooltip("True if you need to spawn the character into a transform location on the scene to load")]
	public bool findSpawnPoint = true;
	[Tooltip("Assign here the spawnPoint name of the scene that you will load")]
	public string spawnPointName;
	private GameObject player;

	public void ChangeScene()
	{


		player = vThirdPersonController.instance.gameObject;
		/*player.GetComponent<vThirdPersonInput>().lockInput = false;
        vThirdPersonController.instance.lockSpeed = false;
        vThirdPersonController.instance.lockRotation = false;*/

		vThirdPersonController.instance.GetComponent<GenericSettings>().UnlockPlayer();

		var spawnPointFinderObj = new GameObject("spawnPointFinder");
		var spawnPointFinder = spawnPointFinderObj.AddComponent<vFindSpawnPoint>();
		//Debug.Log(spawnPointName+" "+gameObject.name);

		spawnPointFinder.AlighObjetToSpawnPoint(player, spawnPointName);

        FindObjectOfType<ProgressSceneLoader>().LoadScene(levelToLoad);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTravel : Task 
{

    public override void EnableTask()
    {
        base.EnableTask();
        Database.DataScene dataScene = Database.scenes.Find(x => x.sceneName == taskScene);
        if(!dataScene.isUnlocked)
        {
            dataScene.isUnlocked = true;
			HUD.SetNewSceneUnlock (dataScene.sceneName);
        }
    }

    public override void ReadyTask()
    {
        base.ReadyTask();
		StartCoroutine (CompletingTask ());
        //this.CompleteTask();
    }

}

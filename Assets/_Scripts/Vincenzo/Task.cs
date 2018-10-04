using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Task : MonoBehaviour 
{
	
    public enum TaskState
    {
        DISABLED,
        ENABLED,
        READY,
        ACTIVED,
        COMPLETING,
        COMPLETED
    }

    public TaskState currentState;
    public string taskName;
    public int taskPriority;
    public string taskScene;


    public virtual void EnableTask()
    {
        this.currentState = TaskState.ENABLED;
        Database.currentQuest.activedTask.currentState = TaskState.ENABLED;
        QuestManager.instance.CurrentTarget = QuestManager.instance.travelTo + " " + this.taskScene;
        StartCoroutine(UIManager.instance.WriteTargetText());

        CompassLocation compass = GameManager.instance.gadgetManager.GetGadgetByType(GadgetManager.GadgetType.COMPASS).GetComponent<CompassLocation>();
        compass.listObjects.Clear();
        GameObject rover = GameObject.Find("LAND ROVER");
        compass.ChangeTargetMission(rover);


    }

    public virtual void ReadyTask()
    {
        this.currentState = TaskState.READY;
        Database.currentQuest.activedTask.currentState = TaskState.READY;
        QuestManager.instance.CurrentTarget = this.taskName;
        StartCoroutine(UIManager.instance.WriteTargetText());
    }

    public virtual void ActiveTask()
    {
        this.currentState = TaskState.ACTIVED;
        Database.currentQuest.activedTask.currentState = TaskState.ACTIVED;
    }

    public virtual void DoTask()
    {

    }

    public virtual IEnumerator CompletingTask()
    {
        this.currentState = TaskState.COMPLETING;

        Sequence sequenceAnimation = UIManager.instance.OverlineTargetText();

        sequenceAnimation.Append(UIManager.instance.FadeOutTargetText());

        yield return sequenceAnimation.WaitForCompletion();

        this.CompleteTask();
        
    }

    public virtual void CompleteTask()
    {
        this.currentState = TaskState.COMPLETED;
        Database.currentQuest.activedTask.currentState = TaskState.COMPLETED;
        QuestManager.instance.currentQuest.SwitchToNextTask();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{

    public enum QuestState
    {
        DISABLED,
        ENABLED,
        COMPLETED
    }

    public QuestState currentState;
    public string questName;
    public int questPriority;
    public Task taskActived;
    public List<Task> questTasks = new List<Task>();

    //public IEnumerator InitQuest(Database.DataQuest quest)
    //public void InitQuest(Database.DataQuest quest)
    public void InitQuest()
    {    
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {

            Database.DataQuest dataQuest = Database.quests[this.questPriority];

            Task task = this.gameObject.transform.GetChild(i).gameObject.GetComponent<Task>();
            questTasks.Add(task);

            Database.DataTask dataTask = new Database.DataTask(task.currentState, task.taskName, task.taskPriority);
            dataQuest.tasks.Add(dataTask);

            if (task.currentState == Task.TaskState.ENABLED)
            {
                taskActived = task;
                dataQuest.activedTask = dataTask;
            }
            
        }
        //yield return null;
    }

    //public IEnumerator SetQuest(Database.DataQuest quest)
    public void SetQuest(Database.DataQuest quest)
    {
        foreach(Database.DataTask dataTask in quest.tasks)
        {
            Task task = this.gameObject.transform.GetChild(dataTask.taskPriority).gameObject.GetComponent<Task>();
            task.currentState = dataTask.currentState;
            questTasks.Add(task);

            if (dataTask.currentState == Task.TaskState.ENABLED || dataTask.currentState == Task.TaskState.READY || dataTask.currentState == Task.TaskState.ACTIVED)
            {
                taskActived = questTasks[task.taskPriority];
            }
        }

        //yield return null;
    }

    public void EnableQuest()
    {
        this.currentState = QuestState.ENABLED;
        Database.quests[Database.currentQuest.questPriority].currentState = QuestState.ENABLED;
        UIManager.instance.ChangeQuestText();
    }

    public void CompleteQuest()
    {
        this.currentState = QuestState.COMPLETED;

        Database.quests[Database.currentQuest.questPriority].currentState = QuestState.COMPLETED;

        this.transform.parent.GetComponent<QuestManager>().SwitchToNextQuest();
    }

    public void SwitchToNextTask()
    {
        if (taskActived.taskPriority < questTasks.Count-1)
        {
            int tempPriority = taskActived.taskPriority;
            tempPriority++;

            taskActived = questTasks[tempPriority];
            Database.currentQuest.activedTask = Database.quests[Database.currentQuest.questPriority].tasks[tempPriority];

            taskActived.EnableTask();

            QuestManager.instance.CheckQuest();

        }
        else
        {
            CompleteQuest();
        }
    }

}

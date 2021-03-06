﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {

    public static QuestManager instance;
    public string travelTo;

    Quest currentQuest;
    List<Quest> quests = new List<Quest>();
    string currentTarget;
    string currentTargetObjects;

    public Quest CurrentQuest
    {
        get { return currentQuest; }
    }

    public string CurrentTarget
    {
        get { return currentTarget; }
        set 
        { 
            currentTarget = value; 
        }
    }

    public string CurrentTargetObjects
    {
        get { return currentTargetObjects; }
        set
        {
            currentTargetObjects = value;
        }
    }

    private void Awake()
    {

        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(this);

    }

    public void InitQuests()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Quest quest = this.transform.GetChild(i).gameObject.GetComponent<Quest>();
            quests.Add(quest);

            Database.DataQuest dataQuest = new Database.DataQuest(quest.currentState, quest.questName, quest.questPriority);
            Database.quests.Add(dataQuest);

            if (quest.currentState == Quest.QuestState.ENABLED)
            {
                currentQuest = quest;
                Database.currentQuest = dataQuest;
            }

            quest.InitQuest();

        }

        //UIManager.instance.ChangeQuestText();
    }

    public void SetQuests()
    {   
        quests.Clear();

        foreach(Database.DataQuest dataQuest in Database.quests)
        {
            Quest quest = this.transform.GetChild(dataQuest.questPriority).gameObject.GetComponent<Quest>();
            quest.currentState = dataQuest.currentState;
            quests.Add(quest);

            quest.SetQuest(dataQuest);

        }

        currentQuest = this.quests[Database.currentQuest.questPriority];
        UIManager.instance.ChangeQuestText();
    }

    public void SwitchToNextQuest()
    {
        if (currentQuest.questPriority < quests.Count-1)
        {
            int tempPriority = currentQuest.questPriority;
            tempPriority++;
            currentQuest = quests[tempPriority];
            Database.currentQuest = Database.quests[tempPriority];
            quests[tempPriority].EnableQuest();
            
            //currentQuest.taskActived = currentQuest.questTasks[0];
            //Database.currentQuest.activedTask = Database.currentQuest.tasks[0];
            //CurrentQuest.questTasks[0].EnableTask();

            UIManager.instance.ChangeQuestText();

            CheckQuest();

        }
        else
        {

            SceneManager.LoadScene("Credits");

        }
    }

    public void CheckQuest()
    {
        if(SceneManager.GetActiveScene().name == currentQuest.TaskActived.taskScene)
        {
            currentQuest.TaskActived.ReadyTask();
        }
        else
        {
            currentQuest.TaskActived.EnableTask();
        }

        UIManager.instance.ChangeQuestText();
    }

    public List<string> GetEnabledQuests()
    {
        List<string> enabledQuests = new List<string>();

        foreach(Quest quest in quests)
        {
            if(quest.currentState != Quest.QuestState.DISABLED)
            {
                enabledQuests.Add(quest.questName);
            }
        }

        return enabledQuests;
    }

    public Quest GetSelectedQuest(string questName)
    {

        Quest selectedQuest = quests.Find(x => x.questName == questName);

        return selectedQuest;

    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour {

    public enum NpcType
    {
        ENGINEER,
        BOTANIST,
        DOCTOR
    }
    public NpcType npc;
    public TextAsset npcDefaultDialogue;

    bool playerTriggered;

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton2)) && playerTriggered)
        {
            UIManager.instance.ShowDialoguePanel();
            if (QuestManager.instance.CurrentQuest.taskActived.GetComponent<TaskTalk>() &&
                QuestManager.instance.CurrentQuest.taskActived.GetComponent<TaskTalk>().npcAssociated == this.npc)
            {
                if (QuestManager.instance.CurrentQuest.taskActived.currentState == Task.TaskState.READY)
                {
                    this.transform.parent.GetComponent<Animator>().SetBool("isTalking", true);
                    QuestManager.instance.CurrentQuest.taskActived.ActiveTask();
                }
                else if (QuestManager.instance.CurrentQuest.taskActived.currentState == Task.TaskState.ACTIVED)
                {
                    QuestManager.instance.CurrentQuest.taskActived.DoTask();
                }
                else if (QuestManager.instance.CurrentQuest.taskActived.currentState == Task.TaskState.COMPLETING)
                {
                    DialogueManager.instance.SetDialogue(npcDefaultDialogue, true);
                }
            }
            else
            {
                DialogueManager.instance.SetDialogue(npcDefaultDialogue, true);
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !playerTriggered)
        {
            playerTriggered = true;
            //UIManager.instance.ShowHelpKeyPanel();
            UIManager.instance.ShowCanvasHelpKey(this.transform.parent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && playerTriggered)
        {
            playerTriggered = false;
            UIManager.instance.HideHelpKey();
            UIManager.instance.HideDialoguePanel();
        }
    }

}

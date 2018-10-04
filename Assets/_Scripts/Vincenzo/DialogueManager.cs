using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager
{
	/*public void SetDialogue(TextAsset dialogueText, bool defaultDialogue)
    {
        ShowDialog();
        string[] dialogues = (dialogueText.ToString()).Split('\n');
        if(!defaultDialogue)
        {
            SwitchDialogues(dialogues);
        }
        else
        {
            this.GetComponentInChildren<Text>().text = dialogues[Random.Range(0, dialogues.Length)];
        }
        
    }*/

    private static DialogueManager _instance;
    public static DialogueManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DialogueManager();
                //_instance = FindObjectOfType<DialogueManager>();
                //Tell unity not to destroy this object when loading a new scene
                //DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public void SetDialogue(TextAsset dialogue, bool defaultDialogue)
    {
        string[] dialogues = (dialogue.ToString()).Split('\n');
        if(defaultDialogue)
        {
            //this.GetComponentInChildren<Text>().text = dialogues[Random.Range(0, dialogues.Length)];
            UIManager.instance.dialoguePanel.GetComponentInChildren<Text>().text = dialogues[Random.Range(0, dialogues.Length)];

        }
        else
        {
            //this.GetComponentInChildren<Text>().text = dialogues[0];
            UIManager.instance.dialoguePanel.GetComponentInChildren<Text>().text = dialogues[0];
        }
    }

    public void SwitchDialogues(string[] dialoguesToSwitch)
    {
        TaskTalk dialoguePointer = QuestManager.instance.currentQuest.taskActived.GetComponent<TaskTalk>();
        (dialoguePointer.currentDialogue)++;
        //this.GetComponentInChildren<Text>().text = dialoguesToSwitch[dialoguePointer.currentDialogue];
        UIManager.instance.dialoguePanel.GetComponentInChildren<Text>().text = dialoguesToSwitch[dialoguePointer.currentDialogue];
    }

}

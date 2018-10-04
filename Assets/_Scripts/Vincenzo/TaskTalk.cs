using Invector.CharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTalk : Task 
{
    public Npc.NpcType npcAssociated;
    public TextAsset taskDialogue;
    public int currentDialogue = 0;
    public GadgetManager.GadgetType[] gadgetsReward;

    GameObject player;
    Npc npc;

    public override void ReadyTask()
    {
        base.ReadyTask();

        Npc[] npcs = FindObjectsOfType<Npc>();
        

        for(int i = 0; i < npcs.Length; i++)
        {
            if(npcs[i].npc == npcAssociated)
            {
                npc = npcs[i];
            }
        }

        CompassLocation compass = GameManager.instance.gadgetManager.GetGadgetByType(GadgetManager.GadgetType.COMPASS).GetComponent<CompassLocation>();
        compass.ChangeTargetMission(npc.transform.parent.gameObject);
    }

    public override void ActiveTask()
    {
        base.ActiveTask();

        //DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        DialogueManager.instance.SetDialogue(this.taskDialogue, false);
        vThirdPersonController.instance.GetComponent<GenericSettings>().LockPlayer();

    }

    public override void DoTask()
    {
        if (currentDialogue >= taskDialogue.ToString().Split('\n').Length-1)
        {
            UIManager.instance.HideDialoguePanel();
            StartCoroutine(CompletingTask());
        }
        else
        {
            //DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
            DialogueManager.instance.SwitchDialogues(taskDialogue.ToString().Split('\n'));
        }
    }

	public override IEnumerator CompletingTask()
	{
        vThirdPersonController.instance.GetComponent<GenericSettings>().UnlockPlayer();
        npc.transform.parent.gameObject.GetComponent<Animator>().SetBool("isTalking", false);

        if (gadgetsReward.Length > 0)
        {
            foreach (GadgetManager.GadgetType gadget in gadgetsReward)
            {
                Gadget gadgetToActivate = GameManager.instance.gadgetManager.gadgets.Find(x => x.gadgetType == gadget);
                gadgetToActivate.EnableGadget();

                Database.gadgets.Find(x => x.gadgetName == gadgetToActivate.name.ToUpper()).isActive = true;

                UIManager.instance.ShowInfoPanel(gadgetToActivate.image, gadgetToActivate.name, gadgetToActivate.description, gadgetToActivate.commands);
            }
        }

        return base.CompletingTask();
	}

	public override void CompleteTask()
    {

        base.CompleteTask();

        //DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();

       
        

    }


}

using Invector.CharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskInteract : Task 
{

    //public List<String> taskObjectsName;
    public string tagTaskObjects;
    public List<GameObject> taskObjects;
    public bool isDestroyable;

    //protected GameObject[] taskObjects;
    protected int taskObjectsNumber = 0;
    protected int allTaskObjectsNumber = 0;
    

    protected virtual void InitTaskObjects()
    {
        GameObject[] taskArrayObjects = GameObject.FindGameObjectsWithTag(tagTaskObjects);
        taskObjects = new List<GameObject>(taskArrayObjects);

        allTaskObjectsNumber = taskObjects.Count;
        //taskObjectsName = new List<string>();

        for(int i = 0; i < allTaskObjectsNumber; i++)
        {
            GameObject action = taskObjects[i].transform.GetChild(0).gameObject;
            SetInteractableListener(action);

            //taskObjectsName.Add(taskObjects[i].name);

            Database.InteractableObject interactableObject = new Database.InteractableObject(
                (InteractableType)Enum.Parse(typeof(InteractableType), tagTaskObjects.ToUpper()), taskObjects[i].name, true, taskScene, isDestroyable);
            Database.interactableObjects.Add(interactableObject);
        }
        
        //QuestManager.instance.CurrentTarget += "\n" + tagTaskObjects + ": " + taskObjectsNumber + "/" + allTaskObjectsNumber;
        QuestManager.instance.CurrentTargetObjects = tagTaskObjects + ": " + taskObjectsNumber + "/" + allTaskObjectsNumber;
        

    }

    protected virtual void LoadTaskObjects()
    {
        taskObjects.Clear();
        allTaskObjectsNumber = 0;
        taskObjectsNumber = 0;

        foreach (Database.InteractableObject interactable in Database.interactableObjects)
        {
            if(interactable.type == (InteractableType)Enum.Parse(typeof(InteractableType), tagTaskObjects.ToUpper())) 
            {

                allTaskObjectsNumber++;

                if(interactable.isInteractable)
                {
                    GameObject action = GameObject.Find(interactable.interactableName).transform.GetChild(0).gameObject;
                    SetInteractableListener(action);

                    taskObjects.Add(action.transform.parent.gameObject);

                }
                else
                {
                    taskObjectsNumber++;
                }
                
            }
        }

        //QuestManager.instance.CurrentTarget = taskName + "\n" + tagTaskObjects + ": " + taskObjectsNumber + "/" + allTaskObjectsNumber;
        QuestManager.instance.CurrentTargetObjects = tagTaskObjects + ": " + taskObjectsNumber + "/" + allTaskObjectsNumber;
    }

    protected virtual void SetTaskObject(GameObject interactable)
    {
        Database.interactableObjects.Find(x => x.interactableName == interactable.name).isInteractable = false;
        taskObjects.Remove(interactable);
        interactable.transform.GetChild(0).GetComponent<vTriggerGenericAction>().OnDoAction.RemoveListener(() => SetTaskObject(interactable));

        /*if(isDestroyable)
            Destroy(interactable);*/

        UIManager.instance.HideHelpKey();
        taskObjectsNumber++;
        QuestManager.instance.CurrentTargetObjects = tagTaskObjects + ": " + taskObjectsNumber + "/" + allTaskObjectsNumber;
        UIManager.instance.ChangeTargetObjectText();
        if(taskObjectsNumber >= allTaskObjectsNumber)
        {
            StartCoroutine(CompletingTask());
        }
    }

    public override void EnableTask()
    {
        base.EnableTask();
        this.taskObjects.Clear();
        QuestManager.instance.CurrentTargetObjects = "";
    }

    public override void ReadyTask()
    {
        base.ReadyTask();

        if(!Database.interactableObjects.Exists(x => x.type == (InteractableType)Enum.Parse(typeof(InteractableType), tagTaskObjects.ToUpper())))
        {
            InitTaskObjects();
            SetCompassObjects(taskObjects);
        }
        else
        {
            LoadTaskObjects();
            SetCompassObjects(taskObjects);
        }

    }

    protected virtual void SetInteractableListener(GameObject action)
    {
        action.SetActive(true);
        vTriggerGenericAction actionComponent = action.GetComponent<vTriggerGenericAction>();

        if (tagTaskObjects == "Equipment")
        {
            GadgetManager gadgetManager = FindObjectOfType<GadgetManager>();
            actionComponent.OnDoAction.AddListener(() =>
            {
                UpdateCompassObjects(action.transform.parent.gameObject);
                SetTaskObject(action.transform.parent.gameObject);
                Gadget gadget = gadgetManager.gadgets
                    .Find(x => x.gadgetType == (GadgetManager.GadgetType)Enum.Parse(typeof(GadgetManager.GadgetType), action.transform.parent.name.ToUpper()));
                gadget.EnableGadget();

                Database.gadgets.Find(x => x.gadgetName == gadget.name.ToUpper()).isActive = true;

                UIManager.instance.ShowInfoPanel(gadget.image, gadget.name, gadget.description, gadget.commands);
                /*gadgetManager = FindObjectOfType<GadgetManager>();
                GadgetManager.GadgetType gadget = (GadgetManager.GadgetType)Enum.Parse(typeof(GadgetManager.GadgetType), action.transform.parent.name.ToUpper());
                gadgetManager.ActivateGadget(gadget, true);*/
            });
        }
        else
        {
            actionComponent.OnDoAction.AddListener(() =>{
                UpdateCompassObjects(action.transform.parent.gameObject);

                SetTaskObject(action.transform.parent.gameObject);
            });
        }

        //actionComponent.OnPlayerEnter.AddListener(() => UIManager.instance.ShowHelpKeyPanel());
        actionComponent.OnPlayerEnter.AddListener(() => UIManager.instance.ShowCanvasHelpKey(action.transform.parent));
        actionComponent.OnPlayerExit.AddListener(() => UIManager.instance.HideHelpKey());

    }

    protected virtual void SetCompassObjects(List<GameObject> compassObjects)
    {
        CompassLocation compass = GameManager.instance.gadgetManager.gadgets.Find(x => x.gadgetType == GadgetManager.GadgetType.COMPASS).GetComponent<CompassLocation>();

        compass.listObjects = new List<GameObject>(compassObjects);

        compass.ChangeTargetMissionSequenzialy();

    }

    protected virtual void UpdateCompassObjects(GameObject compassObjectToDelete)
    {
        CompassLocation compass = GameManager.instance.gadgetManager.gadgets.Find(x => x.gadgetType == GadgetManager.GadgetType.COMPASS).GetComponent<CompassLocation>();
        GameObject objectToDestroy = compass.listObjects.Find(x => x.name == compassObjectToDelete.name);
        compass.listObjects.Remove(objectToDestroy);
    }
}

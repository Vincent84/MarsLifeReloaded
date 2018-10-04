using Invector.CharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskGadget : TaskInteract {

    public GadgetManager.GadgetType gadgetType;
    public string tagZone;

    Gadget gadget;
    //GadgetManager gadgetManager;
    List<GameObject> interactableListZone;

    public override void EnableTask()
    {
        base.EnableTask();
        gadget = GameManager.instance.gadgetManager.GetGadgetByType(gadgetType);
        gadget.listObjects.Clear();
    }

    public override void ReadyTask()
    {
        CompassLocation compass = GameManager.instance.gadgetManager.gadgets.Find(x => x.gadgetType == GadgetManager.GadgetType.COMPASS).GetComponent<CompassLocation>();
        compass.listObjects.Clear();

        GameObject[] interactableArrayZone = GameObject.FindGameObjectsWithTag(tagZone);
        interactableListZone = new List<GameObject>(interactableArrayZone);
        
        base.ReadyTask();

        gadget = GameManager.instance.gadgetManager.GetGadgetByType(gadgetType);

        gadget.listObjects.Clear();
        gadget.listObjects = new List<GameObject>(taskObjects);

        SetCompassObjects(interactableListZone);

    }

    protected override void InitTaskObjects()
    {
        base.InitTaskObjects();

        foreach(GameObject zone in interactableListZone)
        {
            Database.InteractableObject interactableZoneObject = new Database.InteractableObject(
                (InteractableType)Enum.Parse(typeof(InteractableType), tagZone.ToUpper()), zone.name, true, taskScene, isDestroyable);
            Database.interactableObjects.Add(interactableZoneObject);
        }

    }

    protected override void LoadTaskObjects()
    {

        base.LoadTaskObjects();

        interactableListZone.Clear();

        foreach (Database.InteractableObject zone in Database.interactableObjects)
        {
            if (zone.type == (InteractableType)Enum.Parse(typeof(InteractableType), tagZone.ToUpper()) && zone.isInteractable)
            {
                GameObject zoneToFind = GameObject.Find(zone.interactableName).gameObject;
                interactableListZone.Add(zoneToFind);
            }
        }

    }

    protected override void SetInteractableListener(GameObject action)
    {
        vTriggerGenericAction actionComponent = action.GetComponent<vTriggerGenericAction>();

        actionComponent.OnDoAction.AddListener(() =>
        {
            SetTaskObject(action.transform.parent.gameObject);
        });
        //actionComponent.OnPlayerEnter.AddListener(() => UIManager.instance.ShowHelpKeyPanel());
        actionComponent.OnPlayerEnter.AddListener(() => UIManager.instance.ShowCanvasHelpKey(action.transform.parent));
        actionComponent.OnPlayerExit.AddListener(() => UIManager.instance.HideHelpKey());
    }

    protected override void SetTaskObject(GameObject interactable)
    {
        GameObject interactableZone = interactable.transform.parent.gameObject;
        
        gadget.listObjects.Remove(interactable);
        base.SetTaskObject(interactable);

        StartCoroutine(CheckZone(interactableZone));    
    }

    IEnumerator CheckZone(GameObject interactableZone)
    //void CheckZone(GameObject interactableZone)
    {
        yield return null;

        /*if (interactableZone.transform.childCount <= 0)
        {*/
            UpdateCompassObjects(interactableZone);
            Database.interactableObjects.Find(x => x.interactableName == interactableZone.name).isInteractable = false;
            //Destroy(interactableZone);
        //}

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Invector.CharacterController;

public class Gadget : MonoBehaviour {

    public GadgetManager.GadgetType gadgetType;
    public bool isEnabled;
    public bool isEquipped;
    [Header("Lista di oggetti da cercare nella scena")]
    public List<GameObject> listObjects;
    //public TextAsset description;

    [Header("Info Panel Parameters")]
    [TextArea]
    public string description;
    [TextArea]
    public string commands;
    public Sprite image;

    public virtual void EnableGadget()
    {
        this.isEnabled = true;
        //vThirdPersonController.instance.GetComponentInChildren<FeedbackIcons>().EnableFeedback(gameObject.name);
    }

    public virtual void SetGadget()
    {
        if(this.isEnabled)
        {
            this.isEquipped = !this.isEquipped;
        }
            
    }

    protected virtual void UseGadget()
    {

    }

}

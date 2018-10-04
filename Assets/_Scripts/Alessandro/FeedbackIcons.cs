using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackIcons : MonoBehaviour
{
    public Image torchImage;
    public Image scannerImage;
    public Image geigerImage;
    public Image jetpackImage;

    private Animator anim;
    private bool isOpen;

    void Start ()
    {
        anim = GetComponent<Animator>();
    }

    public void ToggleFeedbackGadget()
    {
        isOpen = !isOpen;
        anim.SetBool("isOpen", isOpen);
    }

    public void EnableFeedback(string gadgetName)
    {
        GameObject gadget = this.transform.GetChild(0).Find(gadgetName).gameObject;

        //print(gadget.name);

        for(int i = 0; i < gadget.transform.childCount; i++)
        {
            gadget.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}

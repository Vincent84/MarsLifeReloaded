using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorController : MonoBehaviour
{
    public Animator anim;
    public bool isEntering;

	// Use this for initialization
	void Start ()
    {
        Collider box = GetComponent<Collider>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isEntering = true;
            anim.SetBool("isEntering", isEntering);
        }
	}

    void OnTriggerExit (Collider other)

    {
        if (other.gameObject.tag == "Player")
        {
            isEntering = false;
            anim.SetBool("isEntering", isEntering);
        }
    }
}

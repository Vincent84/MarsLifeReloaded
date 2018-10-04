using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustATest : MonoBehaviour {

    public GameObject npc;


	// Use this for initialization
	void Start () {
        npc.GetComponent<Animator>().SetBool("isTalking", true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UTJ.Alembic;
using Invector.CharacterController;

public class PickaxeAction : vTriggerGenericAction {

    private GameObject poketPickaxe;                                                 // Pala (Tasca)
    private GameObject handPickaxe;                                                  // Pala (Mano)
    private AlembicStreamPlayer alembic;
    private bool destroyed;

    protected override void Start()
    {
        alembic = this.transform.parent.GetComponentInChildren<AlembicStreamPlayer>();
        destroyed = false;

        base.Start();

        poketPickaxe = GameObject.FindGameObjectWithTag("PalaPoket");                // Trova il GameObject con la TAG "PalaPoket"
        handPickaxe = GameObject.FindGameObjectWithTag("PalaPickaxeHand");           // Trova il GameObject con la TAG "PalaHand"

        poketPickaxe.transform.GetChild(0).gameObject.SetActive(true);               // Prendi il Figlio
        handPickaxe.transform.GetChild(0).gameObject.SetActive(false);               // Prendi il Figlio

        OnDoAction.AddListener(() => GetPickaxe());
    }

    private void Update()
    {
        if (destroyed)
        {
			if(alembic.currentTime < (alembic.endTime - 0.1))
            	alembic.currentTime += Time.deltaTime;
			else if (alembic.currentTime >= (alembic.endTime - 0.1))
			{
				this.transform.parent.GetComponent<BoxCollider> ().enabled = false;
                Destroy (this.gameObject);
                //this.gameObject.SetActive(false);
			}
        }
    }

    public void GetPickaxe()
    {
        
        StartCoroutine(UsePickaxe());                                                // Attiva nel momento in cui il Player preme Azione sul collider dell'animazione
    }

    public IEnumerator UsePickaxe()                                                  // Attiva e disattiva la pala tra la Tasca e la Mano
    {
        Animator playerAnimator = vThirdPersonController.instance.GetComponent<Animator>();
        int pickaxeState = playerAnimator.GetInteger("PickaxeState");

		this.gameObject.GetComponent<BoxCollider> ().enabled = false;

        playerAnimator.SetInteger("PickaxeState", 0);
        handPickaxe.transform.GetChild(0).gameObject.SetActive(true);
        handPickaxe.transform.GetChild(0).GetComponent<Animator>().SetTrigger("PickaxeApertura");
        poketPickaxe.transform.GetChild(0).gameObject.SetActive(false);

        yield return new WaitForSeconds(4.6f);

        playerAnimator.SetInteger("PickaxeState", 3);
        handPickaxe.transform.GetChild(0).gameObject.SetActive(false);
        poketPickaxe.transform.GetChild(0).gameObject.SetActive(true);
        destroyed = true;


    }
}

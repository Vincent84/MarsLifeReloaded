using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Invector.CharacterController;


public class ShovelAction : vTriggerGenericAction {

    private GameObject pocketShovel;                                                 // Pala (Tasca)
    private GameObject handShovel;                                                  // Pala (Mano)

    protected override void Start ()
    {

        base.Start();

        //poketShovel = GameObject.FindGameObjectWithTag("PalaPoket");                // Trova il GameObject con la TAG "PalaPoket"
        //handShovel = GameObject.FindGameObjectWithTag("PalaHand");                  // Trova il GameObject con la TAG "PalaHand"

        pocketShovel = vThirdPersonController.instance.GetComponent<GenericSettings>().pocketShovel;
        handShovel = vThirdPersonController.instance.GetComponent<GenericSettings>().handShovel;

        pocketShovel.transform.GetChild(0).gameObject.SetActive(true);               // Prendi il Figlio
        handShovel.transform.GetChild(0).gameObject.SetActive(false);               // Prendi il Figlio

        OnDoAction.AddListener(() => GetShovel());
    }

    public void GetShovel()
    {
        StartCoroutine(UseShovel());
    }

    public IEnumerator UseShovel()
    {
        handShovel.transform.GetChild(0).gameObject.SetActive(true);
        handShovel.transform.GetChild(0).GetComponent<Animator>().SetTrigger("ShovelApertura");
        pocketShovel.transform.GetChild(0).gameObject.SetActive(false);

        yield return new WaitForSeconds(1);

        if (transform.parent.gameObject.CompareTag("Panels"))
        {
            transform.parent.DOMoveY(-10, 10);
        }

        yield return new WaitForSeconds(3.4f);

        handShovel.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Idle");
        handShovel.transform.GetChild(0).gameObject.SetActive(false);
        pocketShovel.transform.GetChild(0).gameObject.SetActive(true);

        Destroy(this.transform.parent.gameObject);

    }




}

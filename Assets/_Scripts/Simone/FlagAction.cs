using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagAction : vTriggerGenericAction {

    private GameObject flag;                                                        // BANDIERA (mixamorig:RightHand, Flag)
    private GameObject openFlag;                                                    // Prendi la Bandiera chiusa
    private GameObject closedFlag;                                                  // Prendi la Bandiera aperta

    protected override void Start()
    {
        base.Start();

        flag = GameObject.FindGameObjectWithTag("CloseFlag");

        closedFlag = flag.transform.GetChild(0).gameObject;                         // Prendi il Figlio (0)
        openFlag = flag.transform.GetChild(1).gameObject;                           // Prendi il Figlio (1)

        closedFlag.gameObject.SetActive(false);                                     
        openFlag.gameObject.SetActive(false);                                         

        OnDoAction.AddListener(() => GetFlag());
    }

    public void GetFlag()
    {
        StartCoroutine(UseFlag());
    }

    public IEnumerator UseFlag()
    {
        closedFlag.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        openFlag.gameObject.SetActive(true);
        openFlag.transform.position = new Vector3 (closedFlag.transform.position.x, closedFlag.transform.position.y, closedFlag.transform.position.z);
        //openFlag.transform.position = new Vector3 (closedFlag.transform.position.x + 1.8f, closedFlag.transform.position.y - 1.2f, closedFlag.transform.position.z);
        transform.parent.GetChild(1).position = new Vector3(closedFlag.transform.position.x + 1.8f, closedFlag.transform.position.y - 1.2f, closedFlag.transform.position.z);
        closedFlag.gameObject.SetActive(false);
        flag.transform.DetachChildren();

        transform.parent.GetChild(1).gameObject.SetActive(true);
        Destroy(this.gameObject);
    }
}

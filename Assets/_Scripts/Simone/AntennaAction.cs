using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntennaAction : vTriggerGenericAction {

    protected override void Start()
    {
        base.Start();
        OnDoAction.AddListener(() => GetAntenna());
    }

    public void GetAntenna()
    {
        StartCoroutine(UseAntenna());
    }

    public IEnumerator UseAntenna()
    {
        yield return new WaitForSeconds(4.4f);
        // Attiva l'antenna
        //Destroy(gameObject);
        gameObject.SetActive(false);
        this.transform.parent.GetChild(1).gameObject.SetActive(true);
    }
}

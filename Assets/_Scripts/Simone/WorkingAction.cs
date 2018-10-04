using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingAction : vTriggerGenericAction {

    protected override void Start()
    {
        base.Start();
        OnDoAction.AddListener(() => GetWorking());
    }

    public void GetWorking()
    {
        StartCoroutine(UseWorking());
    }

    public IEnumerator UseWorking()
    {
        yield return new WaitForSeconds(4.4f);
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}

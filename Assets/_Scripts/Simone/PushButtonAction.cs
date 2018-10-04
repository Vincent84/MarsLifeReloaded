using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButtonAction : vTriggerGenericAction {

    protected override void Start()
    {
        base.Start();
        OnDoAction.AddListener(() => GetPush());
    }

    public void GetPush()
    {
        StartCoroutine(UsePush());
    }

    public IEnumerator UsePush()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}

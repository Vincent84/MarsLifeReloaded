using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.CharacterController;

public class CodexAction : vTriggerGenericAction {

    public GameObject CodexUI;

	public CodexStefano codex;
    [SerializeField] private bool isCodex;

	protected override void Start () 
	{

        base.Start();
        OnDoAction.AddListener(() => GetCodex());
    }
	
    public void GetCodex()
    {
        isCodex = true;

        CodexUI.SetActive(true);

        HUD hud = GameObject.Find("PauseMenu").GetComponent<HUD>();

		if (hud.GetMenuIsOpen() == false )
        {

			CodexUI.gameObject.SetActive(true);
			codex.MoveOnCodex("Open");
            hud.SetCodexMenuIsOpen(true);
            vThirdPersonController.instance.GetComponent<GenericSettings>().LockPlayer();
        }


    }
}

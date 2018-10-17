using Invector;
using Invector.CharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : vThirdPersonInput
{
    [vEditorToolbar("Inputs")]
    [Header("Custom Input")]
    public GenericInput torchInput = new Invector.GenericInput("T", "Y", "");
    public GenericInput scannerInput = new Invector.GenericInput("1", "RB", "");
    public GenericInput geigerInput = new Invector.GenericInput("2", "LB", "");
    public GenericInput compassInput = new Invector.GenericInput("2", "LB", "");
    public GenericInput menuInput = new Invector.GenericInput("M", "Start", "");

    GadgetManager gadgetManager;

    protected override void Start()
    {
        base.Start();

        gadgetManager = GetComponent<GadgetManager>();
    }

    protected override void InputHandle()
    {
        base.InputHandle();

        if (lockInput || cc.lockMovement || cc.ragdolled) return;

        TorchInput(); // Torcia
        GeigerInput(); // Geiger
        ScannerInput(); // Scanner
        CompassInput(); // Compass
    }

    private void TorchInput() // Torcia
    {

        if (torchInput.GetButtonDown() && cc.GetComponent<GenericSettings>().isOutside)
        {
            cc.GetComponentInChildren<Torch>().SetGadget();
            //cc.Torch();
        }

    }

    private void ScannerInput() // Scanner
    {
        if (scannerInput.GetButtonDown() && cc.GetComponent<GenericSettings>().isOutside)
            cc.GetComponentInChildren<Scanner>().SetGadget();
        //cc.Scanner();
    }

    private void GeigerInput() // Geiger
    {
        if (geigerInput.GetButtonDown() && cc.GetComponent<GenericSettings>().isOutside)
            cc.GetComponentInChildren<Geiger>().SetGadget();
        //cc.Geiger();
    }

    private void CompassInput() // Compass
    {
        if (compassInput.GetButtonDown())
        {
            cc.GetComponentInChildren<CompassLocation>(true).SetGadget();
        }

    }

}

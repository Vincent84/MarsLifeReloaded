using Invector.CharacterController;

public class JetPack : Gadget {

    public override void EnableGadget()
    {
        base.EnableGadget();

        FindObjectOfType<vThirdPersonController>().MultiJump = 2;

        //vThirdPersonController.instance.MultiJump = 2;
    }

}

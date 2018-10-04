using UnityEngine;

public class Door : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
	{
        if(other.CompareTag("Player") &&
            GameManager.instance.gadgetManager.GetGadgetByType(GadgetManager.GadgetType.HELMET).isEnabled &&
            GameManager.instance.gadgetManager.GetGadgetByType(GadgetManager.GadgetType.BACKPACK).isEnabled &&
            GameManager.instance.gadgetManager.GetGadgetByType(GadgetManager.GadgetType.TORCH).isEnabled &&
            GameManager.instance.gadgetManager.GetGadgetByType(GadgetManager.GadgetType.PICKAXE).isEnabled &&
            GameManager.instance.gadgetManager.GetGadgetByType(GadgetManager.GadgetType.COMPASS).isEnabled)
        {
            StartCoroutine(other.GetComponent<GenericSettings>().ChangePlayer());
        }
	}

}

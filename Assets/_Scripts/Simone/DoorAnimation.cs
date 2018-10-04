using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorAnimation : MonoBehaviour
{
    public enum DoorBehaviour { Outside, Inside }
    public DoorBehaviour doorBehaviour;

    //private AudioSource source;

    //public AudioClip open;
    //public AudioClip close;

    public Animator doorDown;
    public Animator doorUp;

	private Musica2 music;

	private bool isEnter = false;

	void Awake()
	{

		if (SceneManager.GetActiveScene ().buildIndex != 0) 
		{
			
			music = GameObject.Find ("_AUDIO").GetComponent<Musica2> ();
		} 
		else 
		{

			music = GameObject.Find ("MainCamera").GetComponent<Musica2> ();

		}

	}

    public void OnTriggerEnter(Collider other)
    {

		Debug.Log ("Eseguito");

		//Per evitare di eseguire quando le porte sono bloccate
		if (isEnter == false && doorDown.IsInTransition(0) == true && doorUp.IsInTransition(0) == true) 
		{
			isEnter = true;
			music.GoPlayOneShot (4);
		}

        //source.PlayOneShot(close); // TOGLIERE IL COMMENTO
        if (doorBehaviour == DoorBehaviour.Outside)
        {
            if (other.gameObject.tag == "Player" &&
                GameManager.instance.gadgetManager.GetGadgetByType(GadgetManager.GadgetType.HELMET).isEnabled &&
                GameManager.instance.gadgetManager.GetGadgetByType(GadgetManager.GadgetType.BACKPACK).isEnabled &&
                GameManager.instance.gadgetManager.GetGadgetByType(GadgetManager.GadgetType.TORCH).isEnabled &&
                GameManager.instance.gadgetManager.GetGadgetByType(GadgetManager.GadgetType.PICKAXE).isEnabled &&
                GameManager.instance.gadgetManager.GetGadgetByType(GadgetManager.GadgetType.COMPASS).isEnabled) 
            {
                doorDown.SetTrigger("DownOpen");
                doorUp.SetTrigger("UpOpen");
            }
        }

        if (doorBehaviour == DoorBehaviour.Inside)
        {
            if (other.gameObject.tag == "Player")
            {
                doorDown.SetTrigger("DownOpen");
                doorUp.SetTrigger("UpOpen");
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {

		Debug.Log ("Eseguito");

		if (isEnter == true) 
		{
			isEnter = false;
			music.GoPlayOneShot (4);
		}

        //source.PlayOneShot(open); // TOGLIERE IL COMMENTO
        if (doorBehaviour == DoorBehaviour.Outside || doorBehaviour == DoorBehaviour.Inside)
        {
            if (other.gameObject.tag == "Player")
            {
                doorDown.SetTrigger("DownClose");
                doorUp.SetTrigger("UpClose");      
            }
        }
    }
}

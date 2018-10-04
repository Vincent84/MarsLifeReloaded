using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector;

public class SoundJump : MonoBehaviour 
{

	public AudioSource source;
	public AudioClip clipJumpUp;
	public AudioClip clipJumpReturn;
	public AudioClip clipArrivedRover;
	public GameObject eSystem;

	private bool returnGround = false;
	private float timer = 0f;

	void Awake()
	{

		source.PlayOneShot (clipArrivedRover);

	}

	void Update()
	{

		if (this.GetComponent<Invector.CharacterController.vThirdPersonController> ().isGrounded == true && this.GetComponent<Invector.CharacterController.vThirdPersonInput>().jumpInput.GetButtonDown() == true && eSystem.activeSelf == false) 
		{

			returnGround = true;
			source.PlayOneShot (clipJumpUp);
			//Debug.Log ("UP");

		}

		/*if (returnGround == true) 
		{

			timer += Time.deltaTime;

		}


		if (timer >= 0.1) 
		{

			if (returnGround == true && this.GetComponent<Invector.CharacterController.vThirdPersonController> ().isGrounded == true) {

				returnGround = false;
				timer = 0;
				source.PlayOneShot (clipJumpReturn);
				Debug.Log ("Down");

			}
		}*/

	}


}

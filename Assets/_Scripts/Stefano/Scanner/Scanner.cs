using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Scanner : Gadget {

    #region Public
	[Header("Variabile che gestisce la velocità di aggiornamento dello scanner")]
	public float updateSpeed = 0.1f;
    //public bool equip = false;
    public float T_distance = 50;
	/*[Header("Lista di oggetti da cercare nella scena")]
	public List<Transform> listObjects;*/
	[Header("Suono da riprodurre")]
	public AudioClip beep;
	public AudioSource audioSource;
	[Header("Casi d'uso per il suono")]
	public List<BeepSystem> listBeepSystem;
	[Header("Effetto circolare")]
	public ParticleSystem ripple;
	[Header("Casi d'uso per il ripple")]
	public List<RippleSystem> listRiplleSystem;
	#endregion

	#region Private 
	private float timer;
	private float currentDistance;
	#endregion

	[Serializable]
	public class BeepSystem
	{

		public float distance; 
		public float timer;
		public int numberBeep;

	}

	[Serializable]
	public class RippleSystem
	{

		public float distance;
		public Color color;
        public float frequency;

	}

    // Update is called once per frame
    void Update () 
	{

        if(isEnabled)
            UseGadget();


	}

    protected override void UseGadget()
    {

        if (listObjects.Count > 0)
        {
            SearchObject();

            timer += Time.deltaTime;

			if (timer >= updateSpeed)
            {
                if (currentDistance <= T_distance && isEquipped == false)
                {
                   //audioSource.PlayOneShot(beep);
                }

                if (isEquipped == true)
                {
                    ripple.gameObject.SetActive(true);
                    ChangeRippleColor(ChooseRippleEffect().color);
                    ChangeRippleFrequency(ChooseRippleEffect().frequency);
                    StartCoroutine(Sound());
                }
                else
                {
                    ripple.gameObject.SetActive(false);
                }
                timer = 0;

            }

        }
        else
        {
            if (isEquipped)
            {
                ripple.gameObject.SetActive(true);
                ChangeRippleColor(listRiplleSystem[0].color);
                ChangeRippleFrequency(listRiplleSystem[0].frequency);
            }
            else
                ripple.gameObject.SetActive(false);
                
        }

    }

    public override void SetGadget()
    {
        base.SetGadget();

        if(listObjects.Count < 1)
        {
            return;
        }

        foreach (GameObject interactableObject in listObjects)
        {
            if (isEquipped)
            {
                interactableObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                interactableObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        
    }

    /// <summary>
    /// Oggetto da cercare calcolando la distanza
    /// </summary>
    private void SearchObject()
	{

		GameObject obj = listObjects[0];
		float distance;
		float compareDistance = Vector3.Distance(listObjects [0].transform.position, transform.position);

		for (int i = 0; i < listObjects.Count; i++) 
		{

			distance = Vector3.Distance (listObjects [i].transform.position, transform.position);
				
			if (distance <= compareDistance) 
			{

				obj = listObjects [i];
				compareDistance = distance;

			}
				
		}

		currentDistance = Vector3.Distance(obj.transform.position,transform.position);

		//Debug.Log (currentDistance);

	}

	#region Ripple

	/// <summary>
	/// Metodo che identifica la casistica dell'effetto da utilizzare
	/// </summary>
	private RippleSystem ChooseRippleEffect()
	{

		RippleSystem value = listRiplleSystem[0];

		//Calcolo la casistica 
		for (int i = 0; i < listRiplleSystem.Count; i++) 
		{

			if (listRiplleSystem [i].distance >= currentDistance) 
			{

				value = listRiplleSystem [i];

			}

		}

		return value;

	}

	/// <summary>
	/// Metodo che cambia il colore al particle system
	/// </summary>
	private void ChangeRippleColor(Color c)
	{

		var main = ripple.main;
		c.a = 255;
		main.startColor = c;

	}

    /// <summary>
    /// Metodo che cambia la frequenza di emissione del particle system
    /// </summary>
    
    private void ChangeRippleFrequency(float frequency)
    {
        var main = ripple.emission;
        main.rateOverTime = frequency;
    }

	#endregion

	#region Beep

	/// <summary>
	/// Metodo che identifica la casistica di suoni da utilizzare
	/// </summary>
	private BeepSystem ChooseBeepSound()
	{

		BeepSystem value = listBeepSystem[0];

		//Calcolo la casistica 
		for (int i = 0; i < listBeepSystem.Count; i++) 
		{

			if (listBeepSystem [i].distance >= currentDistance) 
			{

				value = listBeepSystem [i];

			}

		}
			
		return value;

	}

	/// <summary>
	/// Coroutine che esegue i Beep
	/// </summary>
	IEnumerator Sound()
	{


		//Debug.Log ("Avvio");

		BeepSystem bs = ChooseBeepSound ();

		/*for (int i = 0; i < bs.numberBeep; i++) 
		{

			audioSource.PlayOneShot (beep);
			yield return new WaitForSeconds(bs.timer);
		}*/


		yield return null;
	}

	#endregion

}

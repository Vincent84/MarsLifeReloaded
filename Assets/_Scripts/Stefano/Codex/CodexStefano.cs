using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix;
using Sirenix.Utilities;
using UnityEngine.UI;
using System;
using Invector.CharacterController;


public class CodexStefano : MonoBehaviour 
{

	#region Class

	/// <summary>
	/// Classe che definisce un gadget 
	/// </summary>
	[Serializable]
	public class CodexGadget
	{

		[FoldoutGroup("$name")]
		[Header("Nome del Gadget")]

		public string name;

		[FoldoutGroup("$name")]
		[Header("Descrizione del Gadget")]
		[TextArea]

		public string description;

		[FoldoutGroup("$name")]
		[Header("Immagine del Gadget")]

		public Sprite photo;

		[FoldoutGroup("$name")]
		[Header("Modello 3d del Gadget")]
		[AssetList]
		[InlineEditor(InlineEditorModes.LargePreview)]

		public GameObject gadgetObj;

		[FoldoutGroup("$name")]
		[Space(10)]
		[Header("variabile che indica se queste informazioni sono disponibili")]
		public bool isAvaiable;

	}

	/// <summary>
	/// Classe che definiscie un personaggio
	/// </summary>
	[Serializable]
	public class CodexCharacter
	{

		[FoldoutGroup("$name")]
		[Header("Nome del Personaggio")]

		public string name;

		[FoldoutGroup("$name")]
		[Header("Descrizione del Personaggio")]
		[TextArea]

		public string description; 

		[FoldoutGroup("$name")]
		[Header("Immagine del personaggio")]

		public Sprite photo;

		[FoldoutGroup("$name")]
		[Header("Modello 3d del personaggio")]
		[AssetList]
		[InlineEditor(InlineEditorModes.LargePreview)]
		public GameObject charcterObj;

		[FoldoutGroup("$name")]
		[Space(10)]
		[Header("variabile che indica se queste informazioni sono disponibili")]
		public bool isAvaiable;

	}

	/// <summary>
	/// Classe che definisce un luogo del mondo di gioco
	/// </summary>
	[Serializable]
	public class CodexPlace
	{

		[FoldoutGroup("$name")]
		[Header("Nome del luogo")]

		public string name; 

		[FoldoutGroup("$name")]
		[Header("Descrizione del luogo")]
		[TextArea]

		public string description;

		[FoldoutGroup("$name")]
		[Header("Immagine del luogo")]
		public Sprite photo;

		[FoldoutGroup("$name")]
		[Space(10)]
		[Header("variabile che indica se queste informazioni sono disponibili")]
		public bool isAvaiable;

	}

	/// <summary>
	/// Classe che definisce una missione
	/// </summary>
	[Serializable]
	public class CodexMission
	{

		[FoldoutGroup("$name")]
		[Header("Nome della Missione")]

		public string name; 

		[FoldoutGroup("$name")]
		[Header("Descrizione della Missione")]
		[TextArea]

		public string description;

		[FoldoutGroup("$name")]
		[Header("Immagine della Missione")]
		public Sprite photo;

		[FoldoutGroup("$name")]
		[Space(10)]
		[Header("variabile che indica se queste informazioni sono disponibili")]
		public bool isAvaiable;


	}

	#endregion

	#region Public

	[Header("Lista delle informazioni del Codex")]
	public CodexGadget[] listGadget;
	[Space(10)]
	public CodexCharacter[] listCharacter;
	[Space(10)]
	public CodexPlace[] listPlace;
	[Space(10)]
	public CodexMission[] listMission;

	[Header("Variabili dedicate alla grafica del Codex")]
	public Text name;
	public Text description; 
	public Image photo;
	public GameObject obj;
    [Space(20)]
    public Text[] category;
    [Space(20)]
    public Color32 enableColor;
    public Color32 disableColor;

	[Space(30)]
	[Header("Sezione per l'animazione del Codex")]
	public Animator anim;

	[Header("Codex crashed")]
	public GameObject crashed;

	#endregion

	#region Private

	//Variabile che indica la categoria corrente del codex che stiamo visualizzando
	private int currentCategory = 0;
	//Variabile che indica la scheda corrente del codex che stiamo visualizzando
	private int currentSchede = 0;

	//Variabile che indica se il codex è aperto
	private bool isOpen = false;

	private HUD hud; 

	//Variabili per la gestione del cambio di categorie
	private bool isLeft = false;
	private bool isRight = false;

	private float timeAction = 0.2f;
	private float timerAction = 0f;
	private bool doAction = false;

	private bool isCrashed = false;

	#endregion

	void Start()
	{

		//Assegno la referenza del mio menu di pausa
		hud = GameObject.Find ("PauseMenu").GetComponent<HUD> ();

		GetFirstView ();
        ChangeCategoryUI(0);

	}

	void Update()
	{


        if (hud.GetCodexMenuIsOpen() == true)
        {

            //Chiudiamo il menu del Codex
			if(Input.GetKeyDown(KeyCode.Escape) || InputManager.StartButton() || InputManager.BBotton())
            {

                MoveOnCodex("Close");
                StartCoroutine(WaitAnimationCodex());
                
            }

            //Gestione dei comandi per il cambio di scheda da Joystick
			if (InputManager.RBbutton())
            {

                //NextSchede ();
                NextCategory();

            }

			if (InputManager.LBbutton())
            {

                //PreviousSchede ();
                PreviousCategory();

            }


			if (InputManager.MainHorizontal() > 0.5 && doAction == false)
            {

                Debug.Log("Right");
				NextSchede ();

                isRight = true;
                isLeft = true;

				doAction = true;


            }


			if (InputManager.MainHorizontal() < -0.5 && doAction == false)
            {

                Debug.Log("Left");
				PreviousSchede ();

                isLeft = true;
                isRight = true;

				doAction = true;


            }

            if (InputManager.MainHorizontal() == 0 && InputManager.RTbutton() == 0 && isLeft == true && isRight == true)
            {

                isRight = false;
                isLeft = false;

            }

			//Rallentiamo gli input
			if (doAction == true) 
			{

				timerAction += Time.deltaTime;

				if (timerAction >= timeAction) 
				{

					timerAction = 0;
					doAction = false;

				}

			}

        }

	}

	#region Controller

	/// <summary>
	/// Metodo che passa alla scheda successiva del Codex
	/// </summary>
	public void NextSchede()
	{
		//Evitiamo di andare in out of the range
		#region OutOfTheRange
		if (currentCategory == 0) 
		{

			if (currentSchede == listGadget.Length -1) 
			{
				currentSchede = 0;
			}
			else
			{

				currentSchede++;

			}

		} 
		else if (currentCategory == 1) 
		{

			if (currentSchede == listCharacter.Length - 1) 
			{
				currentSchede = 0;
			}
			else
			{

				currentSchede++;

			}

		} 
		else if (currentCategory == 2) 
		{

			if (currentSchede == listPlace.Length - 1) 
			{
				currentSchede = 0;
			}
			else
			{

				currentSchede++;

			}

		} 
		else if (currentCategory == 3) 
		{

			if (currentSchede == listMission.Length - 1) 
			{
				currentSchede = 0;
			}
			else
			{

				currentSchede++;

			}

		} 

		#endregion

		try
		{

			//Controllo che array devo scorrere a seconda della categoria
			if (currentCategory == 0) 
			{

				GetCodexGadgetData (listGadget [currentSchede]);

			} 
			else if (currentCategory == 1) 
			{

				GetCodexCharactertData (listCharacter [currentSchede]);

			} 
			else if (currentCategory == 2) 
			{

				GetCodexPlacetData (listPlace [currentSchede]);

			} 
			else if (currentCategory == 3) 
			{

				GetCodexMissionData ( listMission[currentSchede]);

			}
		}
		catch(Exception ex) {

			Debug.Log ("Error");

			crashed.SetActive (true);
			isCrashed = true;

			//Controllo che array devo scorrere a seconda della categoria
			if (currentCategory == 0) 
			{

				GetCodexGadgetData (listGadget [0]);

			} 
			else if (currentCategory == 1) 
			{

				GetCodexCharactertData (listCharacter [0]);

			} 
			else if (currentCategory == 2) 
			{

				GetCodexPlacetData (listPlace [0]);

			} 
			else if (currentCategory == 3) 
			{

				GetCodexMissionData ( listMission[0]);

			}

		}

		Debug.Log ("Next scheda");

	}

	/// <summary>
	/// Meotodo che passa alla scheda precedente del Codex
	/// </summary>
	public void PreviousSchede()
	{

		#region OutOfTheRange
		if (currentCategory == 0) 
		{

			if (currentSchede == 0) 
			{
				currentSchede = listGadget.Length -1;
			}
			else
			{

				currentSchede--;

			}

		} 
		else if (currentCategory == 1) 
		{

			if (currentSchede == 0) 
			{
				currentSchede = listCharacter.Length - 1;
			}
			else
			{

				currentSchede--;

			}

		} 
		else if (currentCategory == 2) 
		{

			if (currentSchede == 0 ) 
			{
				currentSchede = listPlace.Length - 1;
			}
			else
			{

				currentSchede--;

			}

		} 
		else if (currentCategory == 3) 
		{

			if (currentSchede == 0 ) 
			{
				currentSchede = listMission.Length - 1;
			}
			else
			{

				currentSchede--;

			}

		} 
		#endregion

		try
		{

			//Controllo che array devo scorrere a seconda della categoria
			if (currentCategory == 0) 
			{
				
				GetCodexGadgetData (listGadget [currentSchede]);

			} 
			else if (currentCategory == 1) 
			{
				
				GetCodexCharactertData (listCharacter [currentSchede]);

			} 
			else if (currentCategory == 2) 
			{
				
				GetCodexPlacetData (listPlace [currentSchede]);

			} 
			else if (currentCategory == 3) 
			{

				GetCodexMissionData (listMission [currentSchede]);

			}

		}
		catch(Exception e) 
		{

			Debug.Log ("Error");

			crashed.SetActive (true);
			isCrashed = true;

			//Controllo che array devo scorrere a seconda della categoria
			if (currentCategory == 0) 
			{

				GetCodexGadgetData (listGadget [0]);

			} 
			else if (currentCategory == 1) 
			{

				GetCodexCharactertData (listCharacter [0]);

			} 
			else if (currentCategory == 2) 
			{

				GetCodexPlacetData (listPlace [0]);

			} 
			else if (currentCategory == 3) 
			{

				GetCodexMissionData (listMission [0]);

			}


		}

		Debug.Log ("Previous scheda");

	}
	

	/// <summary>
	/// Metodoo che passa alla categoria successiva
	/// </summary>
	public void NextCategory()
	{

		#region OutOfTherRange
		if (currentCategory == 3) 
		{

			currentCategory = 0;

		} 
		else 
		{

			currentCategory++;

		}
		#endregion

		ChangeCategory (currentCategory);

		Debug.Log ("Next categoria");

	}


	/// <summary>
	/// Metodo che passa alla categoria precedente
	/// </summary>
	public void PreviousCategory()
	{

		#region OutOfTherRange
		if (currentCategory == 0) 
		{

			currentCategory = 3;

		} 
		else 
		{

			currentCategory--;

		}
		#endregion

		ChangeCategory (currentCategory);

		Debug.Log ("Previous categoria");

	}

 

	#endregion

	#region Manipulate Data

	/// <summary>
	/// Metodo che passando la classe, ti restituisce i valori a schermo
	/// </summary>
	public void GetCodexGadgetData(CodexGadget cg)
	{

		//Stampo i valori a schermo 
		name.text = cg.name;
		description.text = cg.description;
		photo.sprite = cg.photo;

		if (listGadget [0].gadgetObj != null) 
		{

			//Instantiate (listGadget [0].gadgetObj, obj.transform);
			//obj = listGadget [0].gadgetObj;

		}

	}

	/// <summary>
	/// Metodo che passando la classe, ti restituisce i valori a schermo
	/// </summary>
	public void GetCodexCharactertData(CodexCharacter ch)
	{

		//Stampo i valori a schermo 
		name.text = ch.name;
		description.text = ch.description;
		photo.sprite = ch.photo;

		if (listCharacter [0].charcterObj != null) 
		{

			//Instantiate (listCharacter [0].charcterObj, obj.transform);
			//obj = listCharacter [0].charcterObj.GetComponent<Mesh>();

		}

	}

	/// <summary>
	/// Metodo che passando la classe, ti restituisce i valori a schermo
	/// </summary>
	public void GetCodexMissionData(CodexMission cm)
	{

		//Stampo i valori a schermo 
		name.text = cm.name;
		description.text = cm.description;
		photo.sprite = cm.photo;

	}

	/// <summary>
	/// Metodo che passando la classe, ti restituisce i valori a schermo
	/// </summary>
	public void GetCodexPlacetData(CodexPlace cp)
	{

		//Stampo i valori a schermo 
		name.text = cp.name;
		description.text = cp.description;
		photo.sprite = cp.photo;

	}

	/// <summary>
	/// Metodo che definisce azioni quando avviene il cambio di categoria
	/// </summary>
	public void ChangeCategory(int category)
	{

        currentCategory = category;

		if (currentCategory == 0) 
		{

			//Stampo i valori a schermo 
			name.text = listGadget [0].name;
			description.text = listGadget [0].description;
			photo.sprite = listGadget [0].photo;

			if (listGadget [0].gadgetObj != null) 
			{

				//Instantiate (listGadget [0].gadgetObj, obj.transform);
				//obj = listGadget [0].gadgetObj;

			}


		}
		else if (currentCategory == 1) 
		{

			//Stampo i valori a schermo 
			name.text = listCharacter [0].name;
			description.text = listCharacter [0].description;
			photo.sprite = listCharacter [0].photo;

			if (listCharacter [0].charcterObj != null) {

				//Instantiate (listCharacter [0].charcterObj, obj.transform);
				//obj = listCharacter [0].charcterObj.GetComponent<Mesh>();

			}


		} 
		else if (currentCategory == 2) 
		{


			//Stampo i valori a schermo 
			name.text = listPlace [0].name;
			description.text = listPlace [0].description;
			photo.sprite = listPlace [0].photo;


		}
		else if (currentCategory == 3) 
		{

			//Stampo i valori a schermo 
			name.text = listMission [0].name;
			description.text = listMission [0].description;
			photo.sprite = listMission [0].photo;

		}

        ChangeCategoryUI(currentCategory);

	}

	/// <summary>
	/// Metodo richiamato al primo avvio del codex
	/// </summary>
	public void GetFirstView()
	{

		//Stampo i valori a schermo 
		name.text = listGadget[0].name;
		description.text = listGadget[0].description;
		photo.sprite = listGadget[0].photo;

		if (listGadget [0].gadgetObj != null) 
		{

			Instantiate (listGadget [0].gadgetObj, obj.transform);
			//obj = listCharacter [0].charcterObj.GetComponent<Mesh>();

		}

	

	}

	#endregion

	#region Animation

	/// <summary>
	/// Metodo che ti muovi nel menu
	/// </summa
	public void MoveOnCodex(string value)
	{

		if (isCrashed == true && value == "Open") 
		{

			crashed.SetActive (true);

		}
		else
		{
			crashed.SetActive (false);
		}

		anim.Play (value);

	}

    #endregion

    #region CodexCanvas

    /// <summary>
    /// Metodo che gestice le categorie della UI a livello grafico
    /// </summary>
    public void ChangeCategoryUI(GameObject obj)
    {

        for(int i=0; i<category.Length; i++)
        {

            if (obj.name == category[i].name)
            {

                //category[i].enabled = true;
                category[i].color = enableColor;

            }
            else
            {

                //category[i].enabled = true;
                category[i].color = disableColor;

            }


        }

    }

    /// <summary>
    /// Metodo che gestice le categorie della UI a livello grafico
    /// </summary>
    public void ChangeCategoryUI(int currentCategory)
    {

        for (int i = 0; i < category.Length; i++)
        {

            if (currentCategory == i )
            {

                //category[i].enabled = true;
                category[i].color = enableColor;

            }
            else
            {

                //category[i].enabled = false;
                category[i].color = disableColor;

            }


        }

    }

    public void SetCloseCodex()
    {

        hud.SetCodexMenuIsOpen(false);
        vThirdPersonController.instance.GetComponent<GenericSettings>().UnlockPlayer();

    }

    public IEnumerator WaitAnimationCodex()
    {

        yield return new WaitForSeconds(1.5f);

        SetCloseCodex();

        Debug.Log("Codex chiuso");

    }

    #endregion

}


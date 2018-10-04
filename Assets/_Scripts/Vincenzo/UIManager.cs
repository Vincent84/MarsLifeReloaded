using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.CharacterController;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour 
{
    /// <summary>
    /// 
    /// Public members that set the animation's speed of the target text  
    /// 
    /// </summary>
    [Header("Speed animation text")]
    public float overlineSpeed;
    public float checkSpeed;
    public float fadeSpeed;
    public float colorSpeed;
    public float writeSpeed;
    public Color colorCheckedText;

    [Header("Speed animation info panel")]
    public float xVelocityOpen;
    public float yVelocityOpen;
    public float xVelocityClose;
    public float yVelocityClose;

    [Header("Speed animation dialogue panel")]
    public float dialogueSpeed;

    /// <summary>
    /// 
    /// Public member that sets the y position of the help key canvas
    /// 
    /// </summary>
    [Header("HelpKey Y Offset")]
    public float yOffsetHelpKey;

    /// <summary>
    /// 
    /// Public member that identify the ui's game object
    /// 
    /// </summary>
    [Header("UI Panel Objects")]
    public GameObject dialoguePanel;
    public GameObject targetText;
    public GameObject questText;
    public GameObject helpKeyPanel;
    public Image fadeImage;
    public GameObject infoPanel;

    /// <summary>
    /// 
    /// Private mamber that checks the dialogue panel animation
    /// 
    /// </summary>
    private bool moveUp;

    private bool infoPanelOpen;

    /// <summary>
    /// 
    /// The instance of the ui manager
    /// 
    /// </summary>
    private static UIManager _instance;
    public static UIManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
                //Tell unity not to destroy this object when loading a new scene
                //DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    private void Update()
    {

        if(InputManager.StartButton() && infoPanelOpen)
        {
            StartCoroutine(HideInfoPanel());
        }

        if(helpKeyPanel.gameObject.activeSelf)
        {
            Vector3 relativePos = vThirdPersonCamera.instance.transform.position;
            relativePos.y = helpKeyPanel.transform.position.y;
            helpKeyPanel.transform.LookAt(relativePos);
        }
    }

    /// <summary>
    /// 
    /// Function that shows the dialogue panel
    /// 
    /// </summary>
    public void ShowDialoguePanel()
    {
        this.dialoguePanel.GetComponentInChildren<Text>().text = "";

        this.dialoguePanel.SetActive(true);
        this.dialoguePanel.transform.DOScaleY(1f, dialogueSpeed);

        // ANIMATION

        /*if (!moveUp)
            dialoguePanel.GetComponent<Animation>().Play("MoveUp_Dialogue");

        moveUp = true;*/
    }

    /// <summary>
    /// 
    /// Function that hides the dialogue panel
    /// 
    /// </summary>
    public void HideDialoguePanel()
    {
        this.dialoguePanel.GetComponentInChildren<Text>().text = "";

        // ANIMATION

        /*if (moveUp)
            dialoguePanel.GetComponent<Animation>().Play("MoveDown_Dialogue");

        moveUp = false;*/

        
        this.dialoguePanel.transform.DOScaleY(0, dialogueSpeed);
        this.dialoguePanel.SetActive(false);

    }



    /// <summary>
    /// 
    /// Function that sets the target's text
    /// 
    /// </summary>
    /// <param name="targetText">string that represents the target text to show</param>
    public void SetTartgetText(string targetText)
    {
        this.targetText.GetComponent<Text>().text = targetText;
    }

    /// <summary>
    /// 
    /// Function that shows and repositions the help key canvas 
    /// 
    /// </summary>
    /// <param name="triggerObjectTransform">transform of the object where will be positioned the help key canvas</param>
    public void ShowCanvasHelpKey(Transform triggerObjectTransform)
    {
        helpKeyPanel.gameObject.SetActive(true);

        if(triggerObjectTransform.gameObject.CompareTag("Panels") || 
           triggerObjectTransform.gameObject.CompareTag("Wall") || 
           triggerObjectTransform.gameObject.CompareTag("Tubes") ||
           triggerObjectTransform.gameObject.CompareTag("Picture") ||
           triggerObjectTransform.gameObject.CompareTag("Pieces") ||
           triggerObjectTransform.gameObject.CompareTag("Flag") ||
           triggerObjectTransform.gameObject.CompareTag("Antenna") ||
           triggerObjectTransform.gameObject.CompareTag("Rock"))
        {
            helpKeyPanel.transform.position = new Vector3(vThirdPersonController.instance.transform.position.x,
            vThirdPersonController.instance.transform.position.y + vThirdPersonController.instance.transform.GetComponent<Collider>().bounds.size.y + yOffsetHelpKey,
            vThirdPersonController.instance.transform.position.z);
        }
        else
        {
            helpKeyPanel.transform.position = new Vector3(triggerObjectTransform.position.x,
            triggerObjectTransform.position.y + triggerObjectTransform.gameObject.GetComponent<Collider>().bounds.size.y,
            triggerObjectTransform.position.z);
        }
        
        if (Input.GetJoystickNames().Length > 0)
        {
            helpKeyPanel.transform.GetChild(1).GetComponent<Image>().DOFade(1, 0.4f);
            helpKeyPanel.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().DOFade(1, 0);
            helpKeyPanel.transform.GetChild(1).GetComponent<RectTransform>().DOScale(0.8f, 0.6f);
            helpKeyPanel.transform.GetChild(1).GetComponent<RectTransform>().DOLocalMoveY(16, 0.4f);
        }
        else
        {
            helpKeyPanel.transform.GetChild(0).GetComponent<Image>().DOFade(1, 0.4f);
            helpKeyPanel.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().DOFade(1, 0);
            helpKeyPanel.transform.GetChild(0).GetComponent<RectTransform>().DOScale(1f, 0.6f);
            helpKeyPanel.transform.GetChild(0).GetComponent<RectTransform>().DOLocalMoveY(16, 0.4f);
        }
    }

    /// <summary>
    /// 
    /// Function that animates the help key canvas
    /// 
    /// </summary>
    public void HideHelpKey()
    {
        Sequence sequenceAnimation = DOTween.Sequence();

        if (Input.GetJoystickNames().Length > 0)
        {
            sequenceAnimation.Append(helpKeyPanel.transform.GetChild(1).GetComponent<Image>().DOFade(0, 0.4f));
            sequenceAnimation.Join(helpKeyPanel.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().DOFade(0, 0.4f));
            sequenceAnimation.Join(helpKeyPanel.transform.GetChild(1).GetComponent<RectTransform>().DOScale(0.6f, 0.4f));
            sequenceAnimation.Join(helpKeyPanel.transform.GetChild(1).GetComponent<RectTransform>().DOLocalMoveY(-16, 0.4f));
        }
        else
        {
            sequenceAnimation.Append(helpKeyPanel.transform.GetChild(0).GetComponent<Image>().DOFade(0, 0.4f));
            sequenceAnimation.Join(helpKeyPanel.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().DOFade(0, 0.4f));
            sequenceAnimation.Join(helpKeyPanel.transform.GetChild(0).GetComponent<RectTransform>().DOScale(0.6f, 0.4f));
            sequenceAnimation.Join(helpKeyPanel.transform.GetChild(0).GetComponent<RectTransform>().DOLocalMoveY(-16, 0.4f));
        }

        StartCoroutine(HideHelpKeyPanel(sequenceAnimation));
    }

    /// <summary>
    /// Function that Hides the help key panel.
    /// 
    /// </summary>
    /// 
    /// <returns>The end of the help key panel animation.</returns>
    /// 
    /// <param name="sequenceAnimation">The hide help key sequence animation.</param>
    IEnumerator HideHelpKeyPanel(Sequence sequenceAnimation)
    {
        yield return sequenceAnimation.WaitForCompletion();
        helpKeyPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// 
    /// Function that manages the fade in and the fade out when the player dead.
    /// 
    /// </summary>
    /// 
    /// <returns>The death.</returns>
    public IEnumerator FadeDeath()
    {

        vThirdPersonController.instance.animator.CrossFadeInFixedTime("Death", 0.1f);

        fadeImage.GetComponent<Image>().DOFade(1, 3);
        yield return new WaitForSeconds(3.9f);
        //fadeImage.GetComponent<Image>().DOFade(0, 0);
        //fadeImage.enabled = false;

        GenericSettings genericSettings = vThirdPersonController.instance.gameObject.GetComponent<GenericSettings>();
        genericSettings.UnlockPlayer();

        var spawnPointFinderObj = new GameObject("spawnPointFinder");
        var spawnPointFinder = spawnPointFinderObj.AddComponent<vFindSpawnPoint>();

        spawnPointFinder.AlighObjetToSpawnPoint(vThirdPersonController.instance.gameObject, Invector.vGameController.instance.spawnPoint.name);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        yield return new WaitForSeconds(1f);

        genericSettings.IsDead = false;

    }

    /// <summary>
    /// 
    /// Function that overlines the target text.
    /// 
    /// </summary>
    /// 
    /// <returns>The sequence animation of the target text.</returns>
    public Sequence OverlineTargetText()
    {
        Sequence animationSequence = DOTween.Sequence();

        if (QuestManager.instance.CurrentTargetObjects != null && QuestManager.instance.CurrentTargetObjects != "")
        {
            animationSequence.Append(targetText.transform.parent.GetChild(3).GetComponent<Text>().DOFade(0, fadeSpeed));
            animationSequence.Append(targetText.transform.parent.GetChild(3).GetComponent<Text>().DOText("", 0));
            animationSequence.Join(targetText.transform.parent.GetChild(3).GetComponent<Text>().DOFade(1, 0));
            QuestManager.instance.CurrentTargetObjects = "";
        }

        animationSequence.Join(targetText.transform.parent.GetChild(2).GetComponent<Image>().DOFillAmount(1, checkSpeed));
        animationSequence.Append(targetText.transform.GetChild(0).GetComponent<Image>().DOFillAmount(1, overlineSpeed));
        
        return animationSequence;

    }

    /// <summary>
    /// 
    /// Function that fades out the target text.
    /// 
    /// </summary>
    /// 
    /// <returns>The animation's sequence.</returns>
    public Sequence FadeOutTargetText()
    {
        Sequence animationSequence = DOTween.Sequence();

        animationSequence.Append(targetText.transform.GetChild(0).GetComponent<Image>().DOFade(0, fadeSpeed));
        animationSequence.Join(targetText.transform.parent.GetChild(2).GetComponent<Image>().DOFade(0, fadeSpeed));
        animationSequence.Join(targetText.GetComponent<Text>().DOFade(0, fadeSpeed));

        animationSequence.Append(targetText.transform.GetChild(0).GetComponent<Image>().DOFillAmount(0, 0));
        animationSequence.Append(targetText.transform.parent.GetChild(2).GetComponent<Image>().DOFillAmount(0, 0));

        animationSequence.Append(targetText.GetComponent<Text>().DOText("", 0));
        animationSequence.Join(targetText.GetComponent<Text>().DOFade(1, fadeSpeed));
        animationSequence.Join(targetText.transform.GetChild(0).GetComponent<Image>().DOFade(1, 0));
        animationSequence.Join(targetText.transform.parent.GetChild(2).GetComponent<Image>().DOFade(1, 0));

        return animationSequence;
    }

    /// <summary>
    /// 
    /// Function that sets the target text with write animation.
    /// 
    /// </summary>
    /// 
    /// <returns>The end of the target text animation.</returns>
    public IEnumerator WriteTargetText()
    {
        Sequence animationSequence = DOTween.Sequence();

        animationSequence.Append(targetText.GetComponent<Text>().DOText(QuestManager.instance.CurrentTarget, writeSpeed));

        yield return animationSequence.WaitForCompletion();

        if (QuestManager.instance.CurrentTargetObjects != null && QuestManager.instance.CurrentTargetObjects != "")
        {
            animationSequence.Append(targetText.transform.parent.GetChild(3).GetComponent<Text>().DOText(QuestManager.instance.CurrentTargetObjects, writeSpeed));
        }
    }

    /// <summary>
    /// 
    /// Funtion that updates the target object text.
    /// 
    /// </summary>
    public void ChangeTargetObjectText()
    {
        targetText.transform.parent.GetChild(3).GetComponent<Text>().text = QuestManager.instance.CurrentTargetObjects;
    }

    /// <summary>
    /// 
    /// Function that updates the quest text.
    /// 
    /// </summary>
    public void ChangeQuestText()
    {
        QuestManager questManager = FindObjectOfType<QuestManager>();
        questText.GetComponent<Text>().text = questManager.currentQuest.questName;
    }

    /// <summary>
    /// 
    /// Function that shows the info panel.
    /// 
    /// </summary>
    public void ShowInfoPanel(Sprite image, string title, string description, string commands)
    {
        infoPanelOpen = true;

        vThirdPersonController.instance.GetComponent<GenericSettings>().LockPlayer();

        infoPanel.SetActive(true);
        infoPanel.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = image;
        infoPanel.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = title;
        infoPanel.transform.GetChild(1).transform.GetChild(1).GetComponent<Text>().text = commands;
        infoPanel.transform.GetChild(1).transform.GetChild(2).GetComponent<Text>().text = description;

        Sequence infoAnimation = DOTween.Sequence();

        //Animation (First Y, Second X) 

        infoAnimation.Append(infoPanel.GetComponent<RectTransform>().DOScaleY(1.0f, yVelocityOpen));
        infoAnimation.Append(infoPanel.GetComponent<RectTransform>().DOScaleX(1.0f, xVelocityOpen));

        //Animation scale complete

        //infoAnimation.Append(infoPanel.GetComponent<RectTransform>().DOScale(1.0f, 1.0f));
    }

    /// <summary>
    /// 
    /// Function that hides the info panel.
    /// 
    /// </summary>
    /// 
    /// <returns>The end of the info panel animation.</returns>
    public IEnumerator HideInfoPanel()
    {
        infoPanelOpen = false;

        Sequence infoAnimation = DOTween.Sequence();

        //Animation (First X, Second Y) 

        infoAnimation.Append(infoPanel.GetComponent<RectTransform>().DOScaleX(0.005f, xVelocityClose));
        infoAnimation.Append(infoPanel.GetComponent<RectTransform>().DOScaleY(0.01f, yVelocityClose));

        //Animation scale complete
        //infoAnimation.Append(infoPanel.GetComponent<RectTransform>().DOScale(0f, 1.0f));

        yield return infoAnimation.WaitForCompletion();

        infoPanel.SetActive(false);

        vThirdPersonController.instance.GetComponent<GenericSettings>().UnlockPlayer();
    }

}

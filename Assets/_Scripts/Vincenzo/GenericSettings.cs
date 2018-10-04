using UnityEngine;
using Invector.CharacterController;
using System.Collections;
using DG.Tweening;

public class GenericSettings : MonoBehaviour
{

    private bool isDead;

    [Header("Player's models variable")]
    public bool isOutside = false;
    public Avatar avatarInside;
    public GameObject modelInside;
    public Avatar avatarOutside;
    public GameObject modelOutside;
    public GameObject currentModel;

    [Header("Shovel Objects")]
    public GameObject pocketShovel;
    public GameObject handShovel;

    /*[Header("Change Player Positions")]
    public GameObject playerExit;
    public GameObject playerEntry;*/

    public bool IsDead
    {
        get { return isDead; }
        set
        {
            isDead = value;
        }
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(ChangePlayer());
        }
    }*/

    /*void Update()
    {
        if (vThirdPersonController.instance.animator.GetFloat("VerticalVelocity") <= vThirdPersonController.instance.ragdollVel && 
            vThirdPersonController.instance.animator.GetFloat("GroundDistance") <= 0.1f && !isDead)
        {
            this.LockPlayer();
            StartCoroutine(UIManager.instance.FadeDeath());
            isDead = true;
        }      
    }*/

    public void LockPlayer()
    {
        vThirdPersonCamera.instance.lockCamera = true;
        vThirdPersonController.instance.GetComponent<vThirdPersonInput>().lockInput = true;
        vThirdPersonController.instance.lockSpeed = true;
        vThirdPersonController.instance.lockRotation = true;
    }

    public void UnlockPlayer()
    {
        vThirdPersonCamera.instance.lockCamera = false;
        vThirdPersonController.instance.GetComponent<vThirdPersonInput>().lockInput = false;
        vThirdPersonController.instance.lockSpeed = false;
        vThirdPersonController.instance.lockRotation = false;
    }

    public IEnumerator ChangePlayer()
    {
        LockPlayer();

        Sequence astronautAnimation = DOTween.Sequence();

        astronautAnimation.Append(UIManager.instance.fadeImage.DOFade(1, 1f));
        yield return astronautAnimation.WaitForCompletion();

        if (currentModel == modelInside)
        {
            GameObject playerExit = GameObject.Find("Uscita_player");

            modelInside.SetActive(false);
            this.gameObject.GetComponent<Animator>().avatar = avatarOutside;
            modelOutside.SetActive(true);
            currentModel = modelOutside;

            /*vThirdPersonController.instance.transform.position = new Vector3(vThirdPersonController.instance.transform.position.x + 5f,
                                                                             vThirdPersonController.instance.transform.position.y, 
                                                                             vThirdPersonController.instance.transform.position.z);*/
            vThirdPersonController.instance.transform.position = playerExit.transform.position;

            isOutside = true;
            Database.playerIsOutside = true;
        }
        else
        {

            GameObject playerEntry = GameObject.Find("Entrata_player");

            modelOutside.SetActive(false);
            this.gameObject.GetComponent<Animator>().avatar = avatarInside;
            modelInside.SetActive(true);
            currentModel = modelInside;

            /*vThirdPersonController.instance.transform.position = new Vector3(vThirdPersonController.instance.transform.position.x - 5f,
                                                                             vThirdPersonController.instance.transform.position.y,
                                                                             vThirdPersonController.instance.transform.position.z);*/
            vThirdPersonController.instance.transform.position = playerEntry.transform.position;

            isOutside = false;
            Database.playerIsOutside = false;

        }

        yield return new WaitForSeconds(0.5f);

        UIManager.instance.fadeImage.DOFade(0, 1f);
        UnlockPlayer();

    }


    public void SetPlayer()
    {
        if (Database.playerIsOutside)
        {
            modelInside.SetActive(false);
            this.gameObject.GetComponent<Animator>().avatar = avatarOutside;
            modelOutside.SetActive(true);
            currentModel = modelOutside;

            isOutside = true;
        }
        else
        {

            modelOutside.SetActive(false);
            this.gameObject.GetComponent<Animator>().avatar = avatarInside;
            modelInside.SetActive(true);
            currentModel = modelInside;

            isOutside = false;

        }

        this.gameObject.transform.position = new Vector3(Database.playerPosition.x, Database.playerPosition.y, Database.playerPosition.z);

    }


}
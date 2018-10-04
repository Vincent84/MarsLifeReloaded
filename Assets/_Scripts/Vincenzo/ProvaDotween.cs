using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Playables;
using Cinemachine;
using Invector.CharacterController;

public class ProvaDotween : MonoBehaviour {

    PlayableDirector playable;
    CinemachineSmoothPath cineMachine;
    public vThirdPersonController player;

    bool activePlayable = false;

    // Use this for initialization
    void Start () {
        
	}

    /*private void Update()
    {
        if(playable.state == PlayState.Paused && activePlayable)
        {
            activePlayable = false;
            this.gameObject.SetActive(false);
        }
    }*/

    public void StartCinematic()
    {
        /*this.gameObject.SetActive(true);
        playable = GetComponent<PlayableDirector>();
        activePlayable = true;
        playable.Play();*/
        player.GetComponent<vThirdPersonInput>().lockInput = true;

    }

    // Update is called once per frame


    /*protected virtual IEnumerator Prova()
    {
        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(gameObject.transform.DOMoveX(50, 2));
        mySequence.Append(gameObject.transform.DOScaleY(10, 2));

        yield return mySequence.WaitForCompletion();

        Debug.Log("Ciao");
    }*/



    
}

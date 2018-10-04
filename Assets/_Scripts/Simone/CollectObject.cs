using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectObject : MonoBehaviour {

    public GameObject Rock;
    public SpriteRenderer collectable;

    private bool isCollected;


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.E) && !isCollected)
        {
            isCollected = true;
            Sequence mySequence = DOTween.Sequence();

            mySequence.Append(Rock.transform.DOMoveY(-0.6f, 5));                                // This is a wait
            mySequence.Append(collectable.transform.DOMoveY(2, 3));
            mySequence.Append(Rock.transform.DOMoveY(-0.6f, 0.2f));                             // This is a wait
            mySequence.Join(collectable.DOFade(0, 1.5f));

        }
    }


}

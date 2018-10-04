using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class KeyboardAnimation : MonoBehaviour {

    private void OnEnable()
    {
        GetComponent<Image>().DOFade(1, 0.4f);
        GetComponent<RectTransform>().DOScale(2f, 0.6f);
        GetComponent<RectTransform>().DOLocalMoveY(16, 0.4f);
    }

    private void OnDisable()
    {
        GetComponent<Image>().DOFade(0, 0f);
        GetComponent<RectTransform>().DOScale(0.6f, 0f);
        GetComponent<RectTransform>().DOLocalMoveY(-16, 0f);
    }
}

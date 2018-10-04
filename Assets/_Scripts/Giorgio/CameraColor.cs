using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColor : MonoBehaviour {

    [Range(0, 100)]
    public float range;

    public Color color1;
    public Color color2;

	// Use this for initialization
	void Start () {
        GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
    }
	
	// Update is called once per frame
	void Update () {

        GetComponent<Camera>().backgroundColor = Color.Lerp(GetComponent<Camera>().backgroundColor, color2, 0.3f * Time.deltaTime);

        if(range <= 50)
        {
            Debug.Log("sotto i 50");
        }

    }
}

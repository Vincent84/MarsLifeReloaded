using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Hide();
	}

    public void Hide ()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update ()
    {
        if (Input.GetKeyDown("escape"))
            Show();
    }

    public void Show ()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gizmo : MonoBehaviour {

    public Color gizmoColor = Color.green;
    private Component comp = null;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        comp = gameObject.GetComponent<BoxCollider>();

        if (comp != null)
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.GetComponent<BoxCollider>().center = Vector3.zero;
            gameObject.GetComponent<BoxCollider>().size = Vector3.one;
        }

        Gizmos.matrix = transform.localToWorldMatrix;
        if (comp == null) gameObject.AddComponent<BoxCollider>();
        Gizmos.DrawCube(Vector3.zero, Vector3.one);

		Gizmos.DrawLine(Vector3.zero, Vector3.forward);
    }
}

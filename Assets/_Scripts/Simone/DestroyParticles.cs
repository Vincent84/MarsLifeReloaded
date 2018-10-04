using UnityEngine;
using System.Collections;

public class DestroyParticles : MonoBehaviour
{

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

}
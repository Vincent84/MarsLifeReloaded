using System.Collections;
using System.Collections.Generic;
using Invector.CharacterController;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProvaFiglio : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            vThirdPersonInput input = other.GetComponent<vThirdPersonInput>();

            //vThirdPersonAnimator a = other.GetComponent<vThirdPersonAnimator>();

            //vThirdPersonMotor m = other.GetComponent<vThirdPersonMotor>();
            //m.ControlSpeed(0f);



            vThirdPersonController.instance.lockSpeed = true;
            //vThirdPersonController.instance.animator.SetFloat("InputMagnitude", 0f); 
            //vThirdPersonController.instance.stopMove = true; 
            //vThirdPersonController.instance.quickStop = true;
            //vThirdPersonController.instance.velocity = 0f;
            input.lockInput = true;

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {

            if(Input.GetKeyDown(KeyCode.P))
            {
                vThirdPersonInput input = other.GetComponent<vThirdPersonInput>();
                vThirdPersonController.instance.lockSpeed = true;
                //vThirdPersonController.instance.animator.SetFloat("InputMagnitude", 0f); 
                //vThirdPersonController.instance.stopMove = true; 
                //vThirdPersonController.instance.quickStop = true;
                //vThirdPersonController.instance.velocity = 0f;
                input.lockInput = true;
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                vThirdPersonInput input = other.GetComponent<vThirdPersonInput>();
                vThirdPersonController.instance.lockSpeed = false;
                //vThirdPersonController.instance.animator.SetFloat("InputMagnitude", 0f); 
                //vThirdPersonController.instance.stopMove = true; 
                //vThirdPersonController.instance.quickStop = true;
                //vThirdPersonController.instance.velocity = 0f;
                input.lockInput = false;
            }


            //vThirdPersonAnimator a = other.GetComponent<vThirdPersonAnimator>();

            //vThirdPersonMotor m = other.GetComponent<vThirdPersonMotor>();
            //m.ControlSpeed(0f);





        }
    }

   /*public string[] scenes;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            scenes = ReadNames();

            foreach(string s in scenes)
            {
                print(s);
            }
            
            //StartCoroutine(Prova());
        }


    }

    

    private static string[] ReadNames()
    {
        List<string> temp = new List<string>();
        foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
        {
            if (S.enabled)
            {
                string name = S.path.Substring(S.path.LastIndexOf('/') + 1);
                name = name.Substring(0, name.Length - 6);
                temp.Add(name);
            }
        }
        return temp.ToArray();
    }

    protected override IEnumerator Prova()
    {
        

        yield return StartCoroutine(base.Prova());

        Debug.Log("Olè");

        yield return null;

    }*/
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProveAction : vTriggerGenericAction {

    protected override void Start()
    {
        base.Start();
        OnDoAction.AddListener(() => GetProve());
    }

    
    public void GetProve()
    {
        if(QuestManager.instance.currentQuest.taskActived is TaskInteract)
        {
            if (QuestManager.instance.currentQuest.taskActived.GetComponent<TaskInteract>().isDestroyable)
            {
                //Destroy(this.transform.parent.gameObject);

                if(this.transform.parent.tag == "Picture")
                {
                    GameManager.instance.GetComponent<CinemachineManager>().StartCutscene(this.transform.parent.GetChild(1).gameObject);
                    this.gameObject.SetActive(false);
                }
                else
                {
                    this.transform.parent.gameObject.SetActive(false);
                }
                
            }
            else
            {
                this.transform.parent.GetChild(1).gameObject.SetActive(true);
                this.gameObject.SetActive(false);
            }
                

        }
		
    }

}

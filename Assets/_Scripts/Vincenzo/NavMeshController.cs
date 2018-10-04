using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
/*
public class NavMeshController : MonoBehaviour
{

    public Transform targetTransform;

    NavMeshAgent agent;
    Animator animator;
    Npc npc;
    Quest activedQuest;
    QuestManager questManager;
    int currentTarget = 0;
    bool activityTriggered = false;
    bool playerTriggered = false;

    void Start()
    {
        agent = transform.parent.GetComponent<NavMeshAgent>();
        animator = transform.parent.GetComponent<Animator>();
        npc = transform.parent.GetComponent<Npc>();
        questManager = FindObjectOfType<QuestManager>();
        activedQuest = questManager.currentQuest;
        //agent.SetDestination(npc.targets[currentTarget].transform.position);
        //animator.SetBool("Move", true);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !playerTriggered)
        {
            playerTriggered = true;
            agent.transform.GetChild(2).gameObject.SetActive(true);

            if (activedQuest.GetComponent<Quest>().npcAssociated == npc.gameObject)
            {
                //print(activedQuest.GetComponent<Quest>().dialogue[0]);
                questManager.ActivateQuest();


            }
            /*agent.transform.GetChild(2).gameObject.SetActive(true);

            if (agent.hasPath)
            {
                agent.isStopped = true;
                animator.SetBool("Move", false);
                
            }
            //animator.SetBool("Greet", true);
            
        }
        if (other.tag == "NpcActivity" && !activityTriggered)
        {
            animator.SetBool("Move", false);
            activityTriggered = true;

            //StartCoroutine(SetTarget());
        }
    }

    /*private void OnCollisionStay(Collision collision)
    {
        print("Stay");
        if (collision.gameObject.tag == "Player")
        {
            if (agent.hasPath)
            {
                agent.isStopped = true;
                animator.SetBool("Move", false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && playerTriggered)
        {
            playerTriggered = false;

            if (agent.hasPath)
            {
                agent.isStopped = false;

                animator.SetBool("Move", true);

            }
            agent.transform.GetChild(2).gameObject.SetActive(false);
            animator.SetBool("Greet", false);
        }
        if (other.tag == "NpcActivity" && activityTriggered)
        {
            animator.SetBool("Move", true);
            activityTriggered = false;
        }

    }

    

    /*IEnumerator SetTarget()
    {

        yield return new WaitForSeconds(2);

        currentTarget++;
        if (currentTarget >= npc.targets.Length)
        {
            currentTarget = 0;
        }

        agent.SetDestination(npc.targets[currentTarget].transform.position);
        animator.SetBool("Move", true);

        
        
    }

}
*/
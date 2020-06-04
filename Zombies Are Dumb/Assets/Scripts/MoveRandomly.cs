using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveRandomly : MonoBehaviour {

    NavMeshAgent navMeshAgent;
    NavMeshPath path;
    public float timeForNewPath;
    bool inCoRoutine;
    Vector3 target;
    bool validPath;
    Animator anim;


    // Use this for initialization
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        anim = GetComponent<Animator>();
        anim.SetBool("isAttacking", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isIdle", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!inCoRoutine)
            StartCoroutine(DoSomething());
    }
    Vector3 getNewRandomPosition ()
        // setting these ranges is vital larger seems better 
    {
        float x = Random.Range(-300, 300);
     //   float y = Random.Range(-20, 20);
        float z = Random.Range(-300, 300);
        Vector3 pos = new Vector3(x, 0, z);
        return pos;

}
    IEnumerator DoSomething()
    {
        inCoRoutine = true;
        yield return new WaitForSeconds(timeForNewPath);
        GetNewPath();
        validPath = navMeshAgent.CalculatePath(target, path);
        while (!validPath)
        {
            yield return new WaitForSeconds(0.01f);
            GetNewPath();
            validPath = navMeshAgent.CalculatePath(target, path);
            anim.SetBool("isAttacking", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", true);
        }

        inCoRoutine = false;
    }
    void GetNewPath()
    {
        target = getNewRandomPosition();
        navMeshAgent.SetDestination(target);
    }
}

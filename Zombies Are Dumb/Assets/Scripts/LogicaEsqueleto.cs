using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LogicaEsqueleto : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    NavMeshPath path;
    public float timeForNewPath;
    bool inCoRoutine;
    Vector3 target;
    bool validPath;
    Animator anim;
    public GameObject llave;
    public GameObject throwPrefab;
    Transform player;
    Vida vida;
    float cooldown;
    public GameObject bloodEffect;
    public GameObject plasmaEffect;


    // Use this for initialization
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        anim = GetComponent<Animator>();
        anim.SetBool("isAttacking", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isDie", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isIdle", true);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        vida = this.GetComponent<Vida>();
    }

    // Update is called once per frame
    void Update()
    {
        RevisarVida();
        cooldown -= Time.deltaTime;
        Vector3 direction = player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);

        if (Vector3.Distance(player.position, this.transform.position) < 30 && angle < 60 || Vector3.Distance(player.position, this.transform.position) < 15 )
        {

            if (vida.valor == 200)
            {
                navMeshAgent.isStopped = false;
                anim.SetBool("isAttacking", false);
                anim.SetBool("isRunning", true);
                navMeshAgent.speed = 20f;
                anim.SetBool("isDie", false);
                anim.SetBool("isWalking", false);
                Vector3 dirToPlayer = this.transform.position - player.position;
                Vector3 newPos = this.transform.position + dirToPlayer;
                navMeshAgent.SetDestination(newPos);
            }

            else
            {
                if (direction.magnitude > 20)
                {
                    anim.SetBool("isIdle", false);
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isDie", false);
                    anim.SetBool("isRunning", true);
                    navMeshAgent.isStopped = false;
                    navMeshAgent.speed = 2f;
                    navMeshAgent.SetDestination(player.position);
                }

                else
                {
                    
                    navMeshAgent.isStopped = true;
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                    if (cooldown <= 0f)
                    {
                        anim.SetBool("isIdle", false);
                        anim.SetBool("isDie", false);
                        anim.SetBool("isRunning", false);
                        anim.SetBool("isWalking", false);
                        anim.SetBool("isAttacking", true);
                        StartCoroutine(Pausa());
                        
                    }
                }
            }
        }

        else
        {
            
            if (!inCoRoutine)
                StartCoroutine(DoSomething());
        }


        
    }

    IEnumerator Pausa()
    {
        cooldown = 5f;

        yield return new WaitForSeconds(1f);
        GameObject fireBall1 = Instantiate(throwPrefab, new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z), this.transform.rotation);
        GameObject fireBall2 = Instantiate(throwPrefab, new Vector3(this.transform.position.x + 0.25f, this.transform.position.y + 1f, this.transform.position.z), this.transform.rotation);
        GameObject fireBall3 = Instantiate(throwPrefab, new Vector3(this.transform.position.x - 0.025f, this.transform.position.y + 1f, this.transform.position.z), this.transform.rotation);
        GameObject plasma = Instantiate(plasmaEffect, new Vector3(this.transform.position.x, this.transform.position.y + 1f, this.transform.position.z + 1f), this.transform.rotation);
        Destroy(plasma, 0.5f);
        fireBall1.GetComponent<Rigidbody>().velocity = fireBall1.transform.forward * 10f;
        fireBall2.GetComponent<Rigidbody>().velocity = fireBall2.transform.forward * 10f;
        fireBall3.GetComponent<Rigidbody>().velocity = fireBall3.transform.forward * 10f;
        anim.SetBool("isIdle", true);
        anim.SetBool("isDie", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", false);
    }


    void RevisarVida()
    {

        if (vida.valor <= 0)
        {
            gameObject.GetComponent<Animator>().SetBool("isDie", true);

            StartCoroutine(Morir());
        }
    }

    IEnumerator Morir()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(llave, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), this.transform.rotation);
        Destroy(this.gameObject);
    }

    Vector3 getNewRandomPosition()
    // setting these ranges is vital larger seems better 
    {
        float x = Random.Range(-500, 500);
        //   float y = Random.Range(-20, 20);
        float z = Random.Range(-500, 500);
        Vector3 pos = new Vector3(x, 0, z);
        return pos;

    }
    IEnumerator DoSomething()
    {
        
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = 2f;
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
            anim.SetBool("isDie", false);
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", true);

        }

        inCoRoutine = false;
    }
    void GetNewPath()
    {
        
        target = getNewRandomPosition();
        navMeshAgent.SetDestination(target);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bala")
        {
            crearEfectoSangre(collision.transform.position, collision.transform.rotation);
            gameObject.GetComponent<Vida>().recibirDaño(20f);
        }

    }

    public void crearEfectoSangre(Vector3 pos, Quaternion rot)
    {
        GameObject efectoSangre = Instantiate(bloodEffect, pos, rot);
        Destroy(efectoSangre, 1f);
    }
}

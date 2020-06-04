using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LogicaProtectiveZombie : MonoBehaviour
{
    private Vida vida;
    public GameObject bloodEffect;
    NavMeshAgent navMeshAgent;
    Transform player;
    Animator anim;
    public GameObject protectTarget;
    float cooldown;
    public bool protectingZone;
    
    // Start is called before the first frame update
    void Start()
    {
        vida = GetComponent<Vida>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = this.gameObject.GetComponent<Animator>();
        protectingZone = false;

    }

    // Update is called once per frame
    void Update()
    {
        
        RevisarVida();
        cooldown -= Time.deltaTime;
        Vector3 direction = player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);

        if ((Vector3.Distance(player.position, this.transform.position) < 40 && angle < 45) || Vector3.Distance(player.position, this.transform.position) < 15)
        {
            
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

            if (direction.magnitude > 3)
            {
                anim.SetBool("isIdle", false);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isWalking", true);
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(player.position);
            }

            else
            {
                navMeshAgent.isStopped = true;
                anim.SetBool("isIdle", false);
                anim.SetBool("isWalking", false);
                anim.SetBool("isAttacking", true);
                if (cooldown <= 0f)
                {
                    player.GetComponent<Vida>().recibirDaño(5f);
                    cooldown = 0.8f;
                }
            }

        }

        else
        {
            if (protectingZone)
            {
                navMeshAgent.isStopped = true;
                anim.SetBool("isAttacking", false);
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", true);
            }

            else
            {
                if(protectTarget != null)
                {
                    navMeshAgent.isStopped = false;
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("isIdle", false);
                    anim.SetBool("isWalking", true);
                    navMeshAgent.SetDestination(protectTarget.transform.position);
                }
            }

            
        }
    }
    void RevisarVida()
    {

        if (vida.valor <= 0)
        {
            gameObject.GetComponent<Animator>().SetBool("isDead", true);
            StartCoroutine(Morir());
        }
    }

    IEnumerator Morir()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bala")
        {
            crearEfectoSangre(collision.transform.position, collision.transform.rotation);
            gameObject.GetComponent<Vida>().recibirDaño(25f);
        }

    }

    public void crearEfectoSangre(Vector3 pos, Quaternion rot)
    {
        GameObject efectoSangre = Instantiate(bloodEffect, pos, rot);
        Destroy(efectoSangre, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == protectTarget.name)
        {
            protectingZone = true;
            this.transform.rotation = protectTarget.transform.rotation;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == protectTarget.name)
        {
            protectingZone = false;
        }
    }
}

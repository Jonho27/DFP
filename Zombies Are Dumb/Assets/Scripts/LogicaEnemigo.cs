using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class LogicaEnemigo : MonoBehaviour
{
    private Vida vida;
    private Animator animator;
    private Collider collider;
    private Vida vidaJugador;
    private LogicaJugador logicaJugador;
    public bool vida0 = false;
    public bool estaAtacando = false;
    public float daño = 25;
    public FlockAgent myFlockAgent;
    public GeneticPathfinder myGeneticPathfinder;
    public Chase myChase;
    public bool geneticaNoNecesaria = false;
    public bool isDead;
    public GameObject bloodEffect;

    // Start is called before the first frame update
    void Start()
    {

        myFlockAgent = this.gameObject.GetComponent<FlockAgent>();
        myGeneticPathfinder = this.gameObject.GetComponent<GeneticPathfinder>();
        myChase = this.gameObject.GetComponent<Chase>();
        vida = GetComponent<Vida>();
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        RevisarVida();

        if (geneticaNoNecesaria)
        {
            Destroy(gameObject);
        }
        
    }

    

    void RevisarVida()
    {
        if (skullController.imDestroyed)
        {
            vida.valor = -10;
        }

        if (vida0) return;
        if (vida.valor <= 0)
        {
            isDead = true;
            vida0 = true;
            if(myFlockAgent != null)
            {
                
                gameObject.GetComponent<Animator>().SetBool("isDead", true);
                StartCoroutine(FlockMuerto());
            }

            else if(myGeneticPathfinder != null)
            {
                if(myChase.enabled == false)
                {
                    myGeneticPathfinder.hasFinished = true;
                    myGeneticPathfinder.enabled = false;
                    gameObject.GetComponent<Animator>().SetBool("isDead", true);
                    StartCoroutine(Desaparecer());
                }

                else
                {
                    gameObject.GetComponent<Animator>().SetBool("isDead", true);
                    StartCoroutine(Desaparecer());
                }
            }

            else
            {
                gameObject.GetComponent<Animator>().SetBool("isDead", true);
                StartCoroutine(Morir());
            }
            
        }
    }

    IEnumerator Morir()
    {
        yield return new WaitForSeconds(1.75f);
        Destroy(gameObject);

    }

    IEnumerator Desaparecer()
    {
        yield return new WaitForSeconds(1.75f);
        gameObject.transform.localScale = new Vector3(0, 0, 0);

    }

    IEnumerator FlockMuerto()
    {
        yield return new WaitForSeconds(1.25f);
        myFlockAgent.imDead = true;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Bala")
        {
            crearEfectoSangre(collision.transform.position, collision.transform.rotation);
            gameObject.GetComponent<Vida>().recibirDaño(50f);
        }

    }

    public void crearEfectoSangre(Vector3 pos, Quaternion rot)
    {
        GameObject efectoSangre = Instantiate(bloodEffect, pos, rot);
        Destroy(efectoSangre, 1f);
    }


}

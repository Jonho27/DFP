using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AI;

public class brotherLevel2 : MonoBehaviour
{
    Transform player;
    Animator anim;
    public bool playerSeen = false;
    public Text currentLifeText;
    public Vida vida;
    public GameObject bloodEffect;
    NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        vida = GetComponent<Vida>();
        vida.valor = valuesBetweenLevels.vidaHermano;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        currentLifeText.text = vida.valor.ToString();

        Vector3 direction = player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);

        if (gameObject.GetComponent<Vida>().valor <= 0)
        {
            StartCoroutine(ChangeScene("GameOver2"));
        }

        else
        {
            if (Vector3.Distance(player.position, this.transform.position) < 40 && angle < 60)
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", false);
                anim.SetBool("isRunning", false);
                anim.SetBool("isHello", true);
                playerSeen = true;
                direction.y = 0;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);



                if (direction.magnitude > 4.5 && direction.magnitude < 20)
                {
                    navMeshAgent.isStopped = false;
                    if ((Input.GetKey(KeyCode.LeftShift)) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
                    {
                        anim.SetBool("isIdle", false);
                        anim.SetBool("isHello", false);
                        anim.SetBool("isWalking", false);
                        anim.SetBool("isRunning", true);
                        navMeshAgent.speed = 6f;
                    }

                    else
                    {
                        anim.SetBool("isIdle", false);
                        anim.SetBool("isHello", false);
                        anim.SetBool("isRunning", false);
                        anim.SetBool("isWalking", true);
                        navMeshAgent.speed = 4f;
                    }

                    navMeshAgent.SetDestination(player.position);


                }

                else if (direction.magnitude > 20)
                {
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isIdle", false);
                    anim.SetBool("isHello", true);
                    navMeshAgent.isStopped = true;
                }

                else
                {
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isHello", false);
                    anim.SetBool("isIdle", true);
                    navMeshAgent.isStopped = true;
                }



            }

            else
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isWalking", false);
                anim.SetBool("isHello", false);
                anim.SetBool("isIdle", true);
                playerSeen = false;
                navMeshAgent.isStopped = true;
            }
        }





    }

    IEnumerator ChangeScene(string sceneName)
    {
        yield return new WaitForSeconds(1f);
        FindObjectOfType<ProgressSceneLoader>().LoadScene(sceneName);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bala")
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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAI : MonoBehaviour
{
    public Transform player;
    public Animator anim;
    public bool playerSeen = false;
    public float cooldown;
    public GameObject body;
    public Material materialTransparente;
    public Material materialNormal;


    public float moveSpeed = 1f;
    public float rotSpeed = 100f;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;


    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0f;
        anim = this.gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        

        cooldown -= Time.deltaTime;
        Vector3 direction = player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);

        if(this.GetComponent<Vida>().valor > 0)
        {
            if (Vector3.Distance(player.position, this.transform.position) < 40 && angle < 60)
            {
                isWandering = false;
                isRotatingLeft = false;
                isRotatingRight = false;
                isWalking = false;
                playerSeen = true;
                direction.y = 0;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);

                if (direction.magnitude > 2.5)
                {
                    anim.SetBool("isRunning", true);
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("isIdle", false);

                    transform.position += transform.forward * 0.1f;
                }

                else
                {
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isIdle", false);
                    anim.SetBool("isAttacking", true);
                    if (cooldown <= 0f && this.GetComponent<Vida>().valor > 0)
                    {
                        player.GetComponent<Vida>().recibirDaño(10f);
                        cooldown = 1f;
                    }
                }

            }

            else
            {
                
                //Debug.Log("Playerseen ya no");
                playerSeen = false;

                anim.SetBool("isAttacking", false);
                anim.SetBool("isWalking", false);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isIdle", true);

                if (!isWandering)
                {
                    StartCoroutine(Wander());
                }

                if (isRotatingRight)
                {
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("isIdle", true);
                    transform.Rotate(transform.up * Time.deltaTime * rotSpeed);


                }

                if (isRotatingLeft)
                {
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("isIdle", true);
                    transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
                }

                if (isWalking)
                {
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("isIdle", false);
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("isWalking", true);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                }
            }
        }
        
    }



    IEnumerator Wander()
    {
        

        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 4);
        int rotateLorR = Random.Range(0, 3);
        int walkWait = Random.Range(1, 4);
        int walkTime = Random.Range(5, 12);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(rotateWait);
        if (rotateLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }

        if (rotateLorR == 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
        }

        isWandering = false;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            body.GetComponent<SkinnedMeshRenderer>().material = materialTransparente;


        }


    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            body.GetComponent<SkinnedMeshRenderer>().material = materialTransparente;


        }


    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            body.GetComponent<SkinnedMeshRenderer>().material = materialNormal;
        }
    }

}

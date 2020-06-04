using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBrother : MonoBehaviour
{
    public Transform player;
    public Animator anim;
    public bool playerSeen = false;
    public float cooldown;

    /*public float moveSpeed = 0.8f;
    public float rotSpeed = 100f;

    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;*/


    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0f;
        anim = this.gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Brother").transform;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        Vector3 direction = player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);

        if(this.GetComponent<Vida>().valor > 0)
        {
            if (Vector3.Distance(player.position, this.transform.position) < 10 && angle < 45)
            {
                playerSeen = true;
                direction.y = 0;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);



                if (direction.magnitude > 1)
                {
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("isWalking", true);
                    this.transform.Translate(0f, 0f, 0.05f);
                }

                else
                {

                    anim.SetBool("isWalking", false);
                    anim.SetBool("isAttacking", true);
                    if (cooldown <= 0f)
                    {
                        player.GetComponent<Vida>().recibirDaño(5f);
                        cooldown = 1.5f;
                    }



                }

            }

            else
            {
                playerSeen = false;
            }
        }
    }
}

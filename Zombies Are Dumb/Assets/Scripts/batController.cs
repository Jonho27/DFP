using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batController : MonoBehaviour
{
    public Transform player;
    public bool playerSeen = false;
    public float cooldown;
    public float moveSpeed = 1f;
    public float rotSpeed = 100f;
    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;
    public GameObject throwPrefab;
    public GameObject bloodEffect;
    Vida vida;


    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        vida = GetComponent<Vida>();
    }

    // Update is called once per frame
    void Update()
    {
        if (skullController.imDestroyed)
        {
            vida.valor = -10;
        }

        RevisarVida();

        cooldown -= Time.deltaTime;
        Vector3 direction = player.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);

        if (this.GetComponent<Vida>().valor > 0)
        {
            if (Vector3.Distance(player.position, this.transform.position) < 20)
            {
                isWandering = false;
                isRotatingLeft = false;
                isRotatingRight = false;
                isWalking = false;
                playerSeen = true;
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                if (cooldown <= 0f)
                {
                    GameObject fireBall = Instantiate(throwPrefab, new Vector3(this.transform.position.x, this.transform.position.y -0.25f, this.transform.position.z), Quaternion.LookRotation(direction));
                    fireBall.GetComponent<Rigidbody>().velocity = fireBall.transform.forward * 20f;
                    cooldown = 3f;
                }
                

            }

            else
            {

                //Debug.Log("Playerseen ya no");
                playerSeen = false;

                if (!isWandering)
                {
                    StartCoroutine(Wander());
                }

                if (isRotatingRight)
                {
                    transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
                }

                if (isRotatingLeft)
                {
                    transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
                }

                if (isWalking)
                {
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                }
            }
        }

    }

    void RevisarVida()
    {

        if (vida.valor <= 0)
        {
            Destroy(this.gameObject);
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
        if (collision.collider.tag == "Bala")
        {
            crearEfectoSangre(collision.transform.position, collision.transform.rotation);
            gameObject.GetComponent<Vida>().recibirDaño(50f);
        }

        if (collision.gameObject.layer == 8)
        {

            transform.Translate(new Vector3(Random.Range(-90f, 30f), this.transform.position.y, Random.Range(-80f, 30f)));

        }
    }



    public void crearEfectoSangre(Vector3 pos, Quaternion rot)
    {
        GameObject efectoSangre = Instantiate(bloodEffect, pos, rot);
        Destroy(efectoSangre, 1f);
    }
}

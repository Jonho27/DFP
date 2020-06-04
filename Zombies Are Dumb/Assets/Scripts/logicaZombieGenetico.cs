using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logicaZombieGenetico : MonoBehaviour
{
    private Vida vida;
    public GameObject bloodEffect;
    // Start is called before the first frame update
    void Start()
    {
        vida = GetComponent<Vida>();
    }

    // Update is called once per frame
    void Update()
    {
        RevisarVida();
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
        yield return new WaitForSeconds(1.75f);
        pruebaGeneticPathfinder[] moscas = FindObjectsOfType<pruebaGeneticPathfinder>();
        for(int i = 0; i < moscas.Length; i++)
        {
            Destroy(moscas[i].gameObject);
        }
        Destroy(this.gameObject);

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

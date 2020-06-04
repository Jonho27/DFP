using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Vida : MonoBehaviour
{
    public float valor = 100;
    public GameObject prefabBlood;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void recibirDaño(float daño)
    {
        if(this.gameObject.tag == "Player")
        {
            GameObject efectoSangre = Instantiate(prefabBlood, this.gameObject.transform.position, this.gameObject.transform.rotation);
            Destroy(efectoSangre, 1f);
        }

        if (this.gameObject.tag == "Brother")
        {
            GameObject efectoSangre = Instantiate(prefabBlood, this.gameObject.transform.position, this.gameObject.transform.rotation);
            Destroy(efectoSangre, 1f);
        }

        valor -= daño;

        if (valor < 0)
        {
            valor = 0;
        }
    }

    public void recuperarSalud(float salud)
    {
        valor += salud;
        if(valor > 100)
        {
            valor = 100;
        }
    }
}

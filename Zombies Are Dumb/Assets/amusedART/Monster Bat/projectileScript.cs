using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Brother")
        {
            collision.gameObject.GetComponent<Vida>().recibirDaño(5f);
            Destroy(gameObject, 0.5f);
        }

        

        else if(collision.gameObject.tag != "Skeleton")
        {
            Destroy(gameObject, 2f);
        }



        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textMovement : MonoBehaviour
{
    public float tiempo;
    bool sube;
    bool baja;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.position.y); //2.037
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        sube = true;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < 1f)
        {
            sube = true;
            baja = false;
        }

        else if (transform.position.y > 2f)
        {
            sube = false;
            baja = true;
        }


        if (sube)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z), Time.deltaTime * 2.5f);
            
        }

        if (baja)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y - 0.25f, transform.position.z), Time.deltaTime * 2.5f);
            
        }






    }

}

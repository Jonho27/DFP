using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuegoCasaController : MonoBehaviour
{
    public GameObject canvasNoPasar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canvasNoPasar.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canvasNoPasar.SetActive(false);
        }
    }
}

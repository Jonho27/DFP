using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterController : MonoBehaviour
{
    public GameObject grabWater;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            grabWater.SetActive(true);

            if (Input.GetButton("Interact"))
            {
                grabWater.SetActive(false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<LogicaJugador>().water = true;
                
                Destroy(this.gameObject);
            }


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            grabWater.SetActive(false);
        }

    }
}

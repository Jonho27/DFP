using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstAidController : MonoBehaviour
{
    public GameObject grabItems;
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
            grabItems.SetActive(true);
            if (Input.GetButton("Interact"))
            {
                grabItems.SetActive(false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<Vida>().recuperarSalud(30);

                Destroy(this.gameObject);
            }


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            grabItems.SetActive(false);
        }

    }
}

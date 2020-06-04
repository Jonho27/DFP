using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mummyController : MonoBehaviour
{
    public GameObject canvasTakeMummy;
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
        if (other.tag == "Player")
        {
            canvasTakeMummy.SetActive(true);
        }
            
    }

    private void OnTriggerStay(Collider other)
    {
        

        if (other.tag == "Player")
        {
            canvasTakeMummy.SetActive(true);

            if (Input.GetButton("Interact"))
            {
                playerLevel3.haveMummy = true;
                canvasTakeMummy.SetActive(false);
                Destroy(this.gameObject);
            }


        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canvasTakeMummy.SetActive(true);
        }
            
    }
}


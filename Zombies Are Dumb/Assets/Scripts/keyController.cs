using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyController : MonoBehaviour
{
    public GameObject grabKey;
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
            grabKey.SetActive(true);
            if (Input.GetButton("Interact"))
            {
                grabKey.SetActive(false);
                playerLevel2.haveKey = true;
                Destroy(this.gameObject);
            }


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            grabKey.SetActive(false);
        }

    }
}

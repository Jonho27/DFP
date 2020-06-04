using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gasStation : MonoBehaviour
{
    public GameObject messageObjective;
    // Start is called before the first frame update
    void Start()
    {
        messageObjective.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            messageObjective.SetActive(true);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            messageObjective.SetActive(false);
        }
        
    }
}

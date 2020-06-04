using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorController : MonoBehaviour
{
    public GameObject infoKey;
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
        if(other.tag == "Player" && playerLevel2.haveKey)
        {
            if(this.gameObject.name == "Gate 1")
            {
                StartCoroutine(OpenDoor1(-100f));
                this.gameObject.GetComponent<Collider>().enabled = false;
            }

            else if(this.gameObject.name == "Gate 2")
            {
                StartCoroutine(OpenDoor2(100f));
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }

        else if(other.tag == "Player")
        {
            infoKey.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && playerLevel2.haveKey)
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else if (other.tag == "Player")
        {
            infoKey.SetActive(false);
        }
    }

    

    IEnumerator OpenDoor2(float angles)
    {
        for (float ft = 0f; ft <= angles; ft += 2.5f)
        {
            this.transform.rotation = Quaternion.Euler(0, ft, 0);
            yield return null;
        }
        
    }

    IEnumerator OpenDoor1(float angles)
    {
        for (float ft = 0f; ft >= angles; ft -= 2.5f)
        {
            this.transform.rotation = Quaternion.Euler(0, ft, 0);
            yield return null;
        }
    }


}

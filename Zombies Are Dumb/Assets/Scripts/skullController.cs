using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skullController : MonoBehaviour
{
    public GameObject canvasNoMummy;
    public GameObject canvasYesMummy;
    public GameObject explosionPrefab;
    public static bool imDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        imDestroyed = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !playerLevel3.haveMummy)
        {
            canvasNoMummy.SetActive(true);
        }

        else if (other.tag == "Player")
        {
            canvasYesMummy.SetActive(true);
        }

    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Player" && !playerLevel3.haveMummy)
        {
            canvasNoMummy.SetActive(true);
        }

        else if (other.tag == "Player")
        {
            canvasYesMummy.SetActive(true);

            if (Input.GetButton("Interact"))
            {
                canvasYesMummy.SetActive(false);
                GameObject explosion = Instantiate(explosionPrefab, this.transform.position, this.transform.rotation);
                GameObject skull = GameObject.FindGameObjectWithTag("skullPlatform");
                Destroy(explosion, 1f);
                imDestroyed = true;
                Destroy(skull);
                Destroy(this.gameObject);
                
            }


        }



    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !playerLevel3.haveMummy)
        {
            canvasNoMummy.SetActive(false);
        }

        else if (other.tag == "Player")
        {
            canvasYesMummy.SetActive(false);
        }

    }
}

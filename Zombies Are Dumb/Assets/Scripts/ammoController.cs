using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoController : MonoBehaviour
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
                GameObject.FindGameObjectWithTag("Gun").GetComponent<HandgunScriptLPFP>().ammo += 15;
                GameObject.FindGameObjectWithTag("Gun").GetComponent<HandgunScriptLPFP>().totalAmmoText.text = GameObject.FindGameObjectWithTag("Gun").GetComponent<HandgunScriptLPFP>().ammo.ToString();
                GameObject.FindGameObjectWithTag("Gun").GetComponent<HandgunScriptLPFP>().numberOfGrenades += 1;
                GameObject.FindGameObjectWithTag("Gun").GetComponent<HandgunScriptLPFP>().grenades.text = GameObject.FindGameObjectWithTag("Gun").GetComponent<HandgunScriptLPFP>().numberOfGrenades.ToString();
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

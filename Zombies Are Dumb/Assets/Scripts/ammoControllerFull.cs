using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoControllerFull : MonoBehaviour
{
    public GameObject grabItems;
    public GameObject box;
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
        if (other.tag == "Player" && box.active)
        {
            grabItems.SetActive(true);

            if (Input.GetButton("Interact"))
            {
                grabItems.SetActive(false);
                GameObject.FindGameObjectWithTag("Gun").GetComponent<HandgunScriptLPFP>().ammo += 50;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Vida>().recuperarSalud(20);
                GameObject.FindGameObjectWithTag("Gun").GetComponent<HandgunScriptLPFP>().totalAmmoText.text = GameObject.FindGameObjectWithTag("Gun").GetComponent<HandgunScriptLPFP>().ammo.ToString();
                box.SetActive(false);
                StartCoroutine(boxReactivate());
            }


        }
    }

    IEnumerator boxReactivate()
    {
        yield return new WaitForSeconds(20f);
        box.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            grabItems.SetActive(false);
        }
           
    }

}

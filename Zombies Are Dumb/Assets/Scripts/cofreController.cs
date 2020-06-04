using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cofreController : MonoBehaviour
{
    public GameObject mummy;
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
        if (playerLevel3.haveKey)
        {
            GameObject parteSuperior = GameObject.FindGameObjectWithTag("ChestSup");
            parteSuperior.transform.rotation = Quaternion.Lerp(parteSuperior.transform.rotation, Quaternion.Euler(new Vector3(-50f, 180, 0f)), 1f);
            if(mummy != null)
            {
                mummy.SetActive(true);
            }
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infiniteAmmoBoxes : MonoBehaviour
{
    public GameObject box;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!box.activeInHierarchy)
        {
            StartCoroutine(boxReactivate());
        }
        else
        {
            box.SetActive(false);
        }
    }

    IEnumerator boxReactivate()
    {
        yield return new WaitForSeconds(10f);
        box.SetActive(true);
    }
}

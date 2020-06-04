using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mexicanMan : MonoBehaviour
{
    public GameObject playerCanvas;
    public GameObject dialogueCanvas;
    public GameObject hermano;
    public GameObject losDos;
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
        if(other.tag == "Player" && hermano.GetComponent<brotherLevel2>().playerSeen)
        {
            playerCanvas.SetActive(false);
            dialogueCanvas.SetActive(true);
        }

        else if(other.tag == "Player")
        {
            losDos.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && hermano.GetComponent<brotherLevel2>().playerSeen && (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space)))
        {
            FindObjectOfType<ProgressSceneLoader>().LoadScene("TransitionLevel");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        losDos.SetActive(false);
    }
}

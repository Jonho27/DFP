using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MenuButton.audioOn)
        {
            this.GetComponent<AudioSource>().volume = 0.5f;
        }

        else
        {
            this.GetComponent<AudioSource>().volume = 0f;
        }
    }
}

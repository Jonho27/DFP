using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkScene : MonoBehaviour
{
    private Music music;
    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.FindObjectOfType<Music>();
        music.changeTheme();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

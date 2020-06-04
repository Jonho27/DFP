using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinAround : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        this.transform.rotation = Quaternion.Euler(0f, 0f, time * -100f);
    }
}

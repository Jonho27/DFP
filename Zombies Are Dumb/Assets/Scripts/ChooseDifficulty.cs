using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseDifficulty : MonoBehaviour
{
    public static int difficulty = 0;
    public static string optionSelected = "Easy";

    // Start is called before the first frame update
    void Start()
    {
        optionSelected = "Easy";
        difficulty = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2Controller : MonoBehaviour
{
    public GameObject[] zombiesFlies;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i <= ChooseDifficulty.difficulty; i++)
        {
            zombiesFlies[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

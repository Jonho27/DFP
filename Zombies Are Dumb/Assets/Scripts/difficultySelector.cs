using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class difficultySelector : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() { 
    
        this.GetComponent<Dropdown>().value = ChooseDifficulty.difficulty;

    }

    public void UpdateInfo()
    {

        Debug.Log("He clicado");

        ChooseDifficulty.difficulty = this.GetComponent<Dropdown>().value;
        ChooseDifficulty.optionSelected = this.GetComponent<Dropdown>().options[this.GetComponent<Dropdown>().value].text;

        Debug.Log("La dificultad va a ser segun la variable optionSelected: " + ChooseDifficulty.optionSelected);
    }


}

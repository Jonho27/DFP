using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicaJugador : MonoBehaviour
{
    public Vida vida;
    public GameObject hermano;
    public bool water;
    public Text currentLifeText;
    public Text totalLifeText;
    public GameObject objective;
    public GameObject takeWaterText;
    public GameObject useWaterText;
    public GameObject waterPrefab;
    public GameObject population;
    bool waterFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        objective.gameObject.SetActive(true);
        vida = GetComponent<Vida>();
        totalLifeText.text = "100";
        water = false;

    }

    // Update is called once per frame
    void Update()
    {
        RevisarVida();
        currentLifeText.text = vida.valor.ToString();
        
    }

    void RevisarVida()
    {
        

        if (vida.valor <= 0)
        {
            FindObjectOfType<ProgressSceneLoader>().LoadScene("GameOver1");


        }
    }


    


    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Fuego")
        {
            if (water && !waterFinished)
            {
                
                useWaterText.SetActive(true);
                if (Input.GetKey(KeyCode.Z))
                {
                    takeWaterText.SetActive(false);
                    useWaterText.SetActive(false);
                    waterFinished = true;
                    Destroy(other.gameObject, 0.5f);
                    
                }
            }

            else if(!water)
            {
                
                takeWaterText.SetActive(true);
            }
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Casa")
        {
            objective.gameObject.SetActive(true);
        }

        else if (other.gameObject.tag == "nextLevel" && hermano.GetComponent<brotherController>().playerSeen)
        {
            valuesBetweenLevels.vidaJugador = vida.valor;
            valuesBetweenLevels.ammo = GameObject.FindGameObjectWithTag("Gun").GetComponent<HandgunScriptLPFP>().ammo;
            valuesBetweenLevels.currentAmmo = GameObject.FindGameObjectWithTag("Gun").GetComponent<HandgunScriptLPFP>().currentAmmo;
            valuesBetweenLevels.grenades = GameObject.FindGameObjectWithTag("Gun").GetComponent<HandgunScriptLPFP>().numberOfGrenades;
            valuesBetweenLevels.vidaHermano = hermano.GetComponent<Vida>().valor;
            FindObjectOfType<ProgressSceneLoader>().LoadScene("Level2");
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Casa")
        {
            objective.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Fuego")
        {
            takeWaterText.SetActive(false);
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Blue" || collision.transform.tag == "Orange" || collision.transform.tag == "Genetic" || collision.transform.tag == "ZombiesNew")
        {
            if (Input.GetButton("KnifeAttack"))
            {
                collision.gameObject.GetComponent<Vida>().recibirDaño(20f);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Blue" || collision.transform.tag == "Orange" || collision.transform.tag == "Genetic")
        {
            if (Input.GetButton("KnifeAttack"))
            {
                collision.gameObject.GetComponent<Vida>().recibirDaño(20f);
            }
        }
    }

}



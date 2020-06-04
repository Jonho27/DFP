using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerLevel3 : MonoBehaviour
{
    public Vida vida;
    public Text currentLifeText;
    public Text totalLifeText;
    public Text totalAmmoText;
    public static bool haveKey;
    public static bool haveMummy;

    // Start is called before the first frame update
    void Start()
    {
        vida = GetComponent<Vida>();
        vida.valor = valuesBetweenLevels.vidaJugador;
        GameObject.FindGameObjectWithTag("Gun").GetComponent<HandgunScriptLPFP>().ammo = valuesBetweenLevels.ammo;
        GameObject.FindGameObjectWithTag("Gun").GetComponent<HandgunScriptLPFP>().currentAmmo = valuesBetweenLevels.currentAmmo;
        totalAmmoText.text = GameObject.FindGameObjectWithTag("Gun").GetComponent<HandgunScriptLPFP>().ammo.ToString();
        totalLifeText.text = "100";
        haveKey = false;
        haveMummy = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (skullController.imDestroyed)
        {
            StartCoroutine(finishedGame());
        }
        RevisarVida();
        currentLifeText.text = vida.valor.ToString();

    }

    void RevisarVida()
    {


        if (vida.valor <= 0)
        {
            StartCoroutine(ChangeScene("GameOver3"));
        }
    }


    IEnumerator ChangeScene(string sceneName)
    {

        yield return new WaitForSeconds(1f);
        FindObjectOfType<ProgressSceneLoader>().LoadScene(sceneName);


    }

    IEnumerator finishedGame()
    {
        yield return new WaitForSeconds(2f);
        StartCoroutine(ChangeScene("Win"));
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Blue" || collision.transform.tag == "Orange" || collision.transform.tag == "Skeleton" || collision.transform.tag == "ZombiesNew")
        {
            if (Input.GetButton("KnifeAttack"))
            {
                collision.gameObject.GetComponent<Vida>().recibirDaño(20f);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Blue" || collision.transform.tag == "Orange" || collision.transform.tag == "Skeleton" || collision.transform.tag == "ZombiesNew")
        {
            if (Input.GetButton("KnifeAttack"))
            {
                collision.gameObject.GetComponent<Vida>().recibirDaño(20f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Key")
        {
            haveKey = true;
            Destroy(other.gameObject);
        }
    }

}



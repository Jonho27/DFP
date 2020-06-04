using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerLevel2 : MonoBehaviour
{
    public Vida vida;
    public GameObject hermano;
    public Text currentLifeText;
    public Text totalLifeText;
    public Text totalAmmoText;
    public Text totalGrenadesText;
    public static bool haveKey;

    // Start is called before the first frame update
    void Start()
    {
        vida = GetComponent<Vida>();
        vida.valor = valuesBetweenLevels.vidaJugador;
        GameObject.FindGameObjectWithTag("Gun").GetComponent<HandgunScriptLPFP>().ammo = valuesBetweenLevels.ammo;
        GameObject.FindGameObjectWithTag("Gun").GetComponent<HandgunScriptLPFP>().currentAmmo = valuesBetweenLevels.currentAmmo;
        GameObject.FindGameObjectWithTag("Gun").GetComponent<HandgunScriptLPFP>().numberOfGrenades = valuesBetweenLevels.grenades;
        totalAmmoText.text = GameObject.FindGameObjectWithTag("Gun").GetComponent<HandgunScriptLPFP>().ammo.ToString();
        totalGrenadesText.text = GameObject.FindGameObjectWithTag("Gun").GetComponent<HandgunScriptLPFP>().numberOfGrenades.ToString();
        totalLifeText.text = "100";
        haveKey = false;

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
            StartCoroutine(ChangeScene("GameOver1"));


        }
    }


    IEnumerator ChangeScene(string sceneName)
    {

        yield return new WaitForSeconds(1f);
        FindObjectOfType<ProgressSceneLoader>().LoadScene(sceneName);


    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Blue" || collision.transform.tag == "Orange" || collision.transform.tag == "ProtectiveZombie" || collision.transform.tag == "ZombieFlies")
        {
            if (Input.GetButton("KnifeAttack"))
            {
                collision.gameObject.GetComponent<Vida>().recibirDaño(20f);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Blue" || collision.transform.tag == "Orange" || collision.transform.tag == "ProtectiveZombie" || collision.transform.tag == "ZombieFlies")
        {
            if (Input.GetButton("KnifeAttack"))
            {
                collision.gameObject.GetComponent<Vida>().recibirDaño(20f);
            }
        }
    }

}



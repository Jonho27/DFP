using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class damageScreen : MonoBehaviour
{
    Image image;
    public int numVeces = 0;
    void Start()
    {
        image = GetComponent<Image>();
        //StartBlinking();
    }


    IEnumerator Blink()
    {
        while (numVeces < 40)
        {
            switch (image.color.a.ToString())
            {
                case "0":
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
                    //Play sound
                    numVeces += 1;
                    yield return new WaitForSeconds(0.5f);
                    break;
                case "1":
                    image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
                    //Play sound
                    numVeces += 1;
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
        }


    }

    public void StartBlinking()
    {

        StopAllCoroutines();
        StartCoroutine("Blink");
    }

    public void StopBlinking()
    {
        StopAllCoroutines();
    }

}


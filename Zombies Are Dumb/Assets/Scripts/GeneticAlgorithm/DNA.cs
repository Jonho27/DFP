using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DNA
{

    public List<Vector3> genes = new List<Vector3>();
    public DNA(int genomeLenght = 5)
    {
        //Debug.Log("Lo hago random");
        for(int i = 0; i < genomeLenght; i++)
        {
            genes.Add(new Vector3(Random.Range(-1.0f, 1.0f), 0f, Random.Range(-1.0f, 1.0f)));
        }
    }



    public DNA(string path, int id, int populationSize, int genomeLenght = 5)
    {
        TextReader tr = new StreamReader(path);
        int NumberOfLines = (int)new FileInfo(path).Length;
        string[] ListLines = new string[NumberOfLines];
        int index = id * 2 * genomeLenght;
        
        if (NumberOfLines > 0)
        {   
            for (int l = 0; l <= genomeLenght * 2 * populationSize; l++)
            {
                ListLines[l] = tr.ReadLine();
            }
            tr.Close();

            for (int j = 0; j < genomeLenght; j++)
            {
                genes.Add(new Vector3(float.Parse(ListLines[index + j]), 0f, float.Parse(ListLines[index + 1 + j])));
                index = index + 1;
            }
        }

        else
        {
            for (int j = 0; j < genomeLenght; j++)
            {
                genes.Add(new Vector3(Random.Range(-1.0f, 1.0f), 0f, Random.Range(-1.0f, 1.0f)));


            }
        }
    }

    public DNA(DNA parent, DNA partner, float mutationRate=0.01f)
    {
        for (int i = 0; i < parent.genes.Count; i++)
        {
            float mutationChance = Random.Range(0.0f, 1.0f);
            if(mutationChance <= mutationRate)
            {
                genes.Add(new Vector3(Random.Range(-1.0f, 1.0f), 0f, Random.Range(-1.0f, 1.0f)));
            }
            else
            {
                int chance = Random.Range(0, 2);
                if(chance == 0)
                {
                    genes.Add(parent.genes[i]);
                }
                else
                {
                    genes.Add(partner.genes[i]);
                }
                
            }
        }
    }


}

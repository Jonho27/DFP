using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PopulationController : MonoBehaviour
{
    public static List<GeneticPathfinder> population = new List<GeneticPathfinder>();
    public GameObject creaturePrefab;
    public int populationSize = 10;
    public int genomeLenght;
    public float cutoff = 0.3f;
	public int survivorKeep = 5;
	[Range(0f, 1f)]
	public float mutationRate = 0.01f;
    public Transform spawnPoint;
    public Transform end;
    public float tiempo = 0.0f;
    public string subname;
    public bool saveData;

    void InitPopulation()
    {
       
        
        for (int i = 0; i < populationSize; i++)
        {
            GameObject go = Instantiate(creaturePrefab, spawnPoint.position, Quaternion.identity);
            go.GetComponent<GeneticPathfinder>().InitCreature(new DNA(Application.streamingAssetsPath + subname, i, populationSize, genomeLenght), end.position, i);
            go.GetComponent<Animator>().SetBool("isDead", false);
            go.name = "Zombie." + i;
            population.Add(go.GetComponent<GeneticPathfinder>());
        }

        
    }
    void NextGeneration()
    {
        int survivorCut = Mathf.RoundToInt(populationSize * cutoff);
        List<GeneticPathfinder> survivors = new List<GeneticPathfinder>();
        
        for (int i = 0; i < survivorCut; i++)
        {
            survivors.Add(GetFittest());
            
        }

        for (int i = 0; i < population.Count; i++)
        {
            Destroy(population[i].gameObject);

            //Debug.Log("Bucle iteracion: " + i);
            //Debug.Log("Bucle zombie: " + population[i].gameObject.name);
            /*
            if (population[i].gameObject.GetComponent<LogicaEnemigo>().isDead)
            {
                //Debug.Log("Soy: " + population[i].gameObject.name + " y muero.");
                Destroy(population[i].gameObject);
            }

            else
            {
                //Debug.Log("Soy: " + population[i].gameObject.name + " y deberia desactivar genetica y activar chase.");
                population[i].gameObject.GetComponent<GeneticPathfinder>().enabled = false;
                population[i].gameObject.GetComponent<Chase>().enabled = true;
            }*/




        }
        population.Clear();

		for(int i = 0; i < survivorKeep; i++)
		{
            
			GameObject go = Instantiate(creaturePrefab, spawnPoint.position, Quaternion.identity);
			go.GetComponent<GeneticPathfinder>().InitCreature(survivors[i].dna, end.position, i);
            go.GetComponent<Animator>().SetBool("isDead", false);
            go.name = "ZombieSurvivor." + i;
            population.Add(go.GetComponent<GeneticPathfinder>());

		}
        while(population.Count < populationSize)
        {
            for(int i = 0; i < survivors.Count; i++)
            {
               
                GameObject go = Instantiate(creaturePrefab, spawnPoint.position, Quaternion.identity);
                go.GetComponent<GeneticPathfinder>().InitCreature(new DNA(survivors[i].dna, survivors[Random.Range(0, survivorCut)].dna, mutationRate), end.position, i);
                go.GetComponent<Animator>().SetBool("isDead", false);
                go.name = "ZombieMutado." + i;
                population.Add(go.GetComponent<GeneticPathfinder>());
                if(population.Count >= populationSize)
                {
                    break;
                }
            }
        }

        
        createText(survivors);
        

        for (int i = 0; i < survivors.Count; i++)
        {
            Destroy(survivors[i].gameObject);
            
        }
    }
    private void Start()
    {
        if (ChooseDifficulty.difficulty == 0)
        {
            populationSize = 200;
            subname = "/GenomaFacil.txt";
        }

        else if(ChooseDifficulty.difficulty == 1)
        {
            populationSize = 15;
            subname = "/GenomaNormal.txt";
        }

        else if(ChooseDifficulty.difficulty == 2)
        {
            populationSize = 25;
            subname = "/GenomaDificil.txt";
        }

        /*else if (ChooseDifficulty.difficulty == 3)
        {
            populationSize = 200;
            subname = "/GenomaPrueba.txt";
        }*/

        InitPopulation();
    }
    private void Update()
    {
        tiempo += Time.deltaTime;

        if (!HasActive() /*&&  (tiempo.ToString("0") == "180" || tiempo.ToString("0") == "280" || tiempo.ToString("0") == "350" || tiempo.ToString("0") == "500")*/)
        {
            
            NextGeneration();

        }
    }


    GeneticPathfinder GetFittest()
    {
        float maxFitness = float.MinValue;
        int index = 0;
        for(int i = 0; i < population.Count; i++)
        {
            if(population[i].fitness > maxFitness)
            {
                maxFitness = population[i].fitness;
                index = i;
            }
        }

        GeneticPathfinder fittest = population[index];
        //Debug.Log("Fittest: " + index);
        population.Remove(fittest);
        return fittest;
    }
    bool HasActive()
    {
        for(int i = 0; i < population.Count; i++)
        {
            if (!population[i].hasFinished)
            {
                return true;
            }
        }
        return false;
    }

    public void saveInfo()
    {
       createText(population);

    }

    public void createText(List<GeneticPathfinder> population) {

        string path = Application.streamingAssetsPath + subname;
        File.WriteAllText(path, "");
        for(int i = 0; i < population.Count; i++)
        {
            for (int j = 0; j < genomeLenght; j++)
            {
                File.AppendAllText(path, population[i].dna.genes[j].x.ToString());
                File.AppendAllText(path, "\n");
                File.AppendAllText(path, population[i].dna.genes[j].z.ToString());
                File.AppendAllText(path, "\n");
            }
        }
    }

    



    

}

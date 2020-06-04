using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class pruebaPopulationController : MonoBehaviour
{

    public List<pruebaGeneticPathfinder> population = new List<pruebaGeneticPathfinder>();
    public GameObject creaturePrefab;
    public int populationSize = 10;
    public int genomeLenght;
    public float cutoff = 0.3f;
    public int survivorKeep = 5;
    [Range(0f, 1f)]
    public float mutationRate = 0.01f;
    public Transform spawnPoint;
    public Vector3 spawnVector;
    public Transform end;
    public float tiempo = 0.0f;
    public string subname;
    public bool saveData;
    bool haveWin;
    public Transform brother;
    public Animator anim;
    public bool playerSeen = false;
    public float cooldown;
    public int numGeneracion = 1;
    NavMeshAgent navMeshAgent;



    void InitPopulation()
    {


        for (int i = 0; i < populationSize; i++)
        {
            GameObject go = Instantiate(creaturePrefab, new Vector3(spawnVector.x, spawnVector.y + Random.Range(0f, 1.5f), spawnVector.z), Quaternion.identity);
            go.GetComponent<pruebaGeneticPathfinder>().InitCreature(new pruebaDNA(genomeLenght), end.position, i);
            go.name = "Fly." + i;
            population.Add(go.GetComponent<pruebaGeneticPathfinder>());
        }


    }

    void NextGeneration()
    {
        numGeneracion++;
        int survivorCut = Mathf.RoundToInt(populationSize * cutoff);
        List<pruebaGeneticPathfinder> survivors = new List<pruebaGeneticPathfinder>();

        for (int i = 0; i < survivorCut; i++)
        {
            survivors.Add(GetFittest());
        }

        for (int i = 0; i < population.Count; i++)
        {
            Destroy(population[i].gameObject);
        }
        population.Clear();

        for (int i = 0; i < survivorKeep; i++)
        {

            GameObject go = Instantiate(creaturePrefab, new Vector3(spawnVector.x, spawnVector.y + Random.Range(0f, 1.5f), spawnVector.z), Quaternion.identity);
            go.GetComponent<pruebaGeneticPathfinder>().InitCreature(survivors[i].dna, end.position, i);
            go.name = "FlySurvivor." + i;
            population.Add(go.GetComponent<pruebaGeneticPathfinder>());

        }
        while (population.Count < populationSize)
        {
            for (int i = 0; i < survivors.Count; i++)
            {

                GameObject go = Instantiate(creaturePrefab, new Vector3(spawnVector.x, spawnVector.y + Random.Range(0f, 1.5f), spawnVector.z), Quaternion.identity);
                go.GetComponent<pruebaGeneticPathfinder>().InitCreature(new pruebaDNA(survivors[i].dna, survivors[Random.Range(0, survivorCut)].dna, mutationRate), end.position, i);
                go.name = "FlyMutate." + i;
                population.Add(go.GetComponent<pruebaGeneticPathfinder>());
                if (population.Count >= populationSize)
                {
                    break;
                }
            }
        }


        for (int i = 0; i < survivors.Count; i++)
        {
            Destroy(survivors[i].gameObject);

        }
    }

    private void Start()
    {
        cooldown = 0f;
        anim = this.gameObject.GetComponent<Animator>();
        brother = GameObject.FindGameObjectWithTag("Brother").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        spawnVector = spawnPoint.position;

        if (ChooseDifficulty.difficulty == 0)
        {
            populationSize = 100;
            
        }

        else if (ChooseDifficulty.difficulty == 1)
        {
            populationSize = 150;
        }

        else if (ChooseDifficulty.difficulty == 2)
        {
            populationSize = 200;
        }
        InitPopulation();
    }

    private void Update()
    {
        tiempo += Time.deltaTime;
        if(haveWin)
        {
            cooldown -= Time.deltaTime;
            Vector3 direction = brother.position - this.transform.position;
            float angle = Vector3.Angle(direction, this.transform.forward);

            if (this.GetComponent<Vida>().valor > 0)
            {
                if (Vector3.Distance(brother.position, this.transform.position) < 10 && angle < 45)
                {
                    navMeshAgent.isStopped = true;
                    playerSeen = true;
                    direction.y = 0;
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);



                    if (direction.magnitude > 3)
                    {
                        anim.SetBool("isAttacking", false);
                        anim.SetBool("isWalking", true);
                        this.transform.Translate(0f, 0f, 0.1f);
                    }

                    else
                    {

                        anim.SetBool("isWalking", false);
                        anim.SetBool("isAttacking", true);
                        if (cooldown <= 0f)
                        {
                            StartCoroutine(kickAttack());
                            cooldown = 3.8f;
                        }



                    }

                }

                else
                {
                    navMeshAgent.isStopped = false;
                    anim.SetBool("isAttacking", false);
                    anim.SetBool("isIdle", false);
                    anim.SetBool("isWalking", true);
                    navMeshAgent.SetDestination(brother.position);
                }
            } 
        }

        else if (!HasActive() && !haveWin)
        {

            NextGeneration();

        }
    }

    IEnumerator kickAttack()
    {
        yield return new WaitForSeconds(0.6f);
        brother.GetComponent<Vida>().recibirDaño(40f);

    }

    pruebaGeneticPathfinder GetFittest()
    {
        float maxFitness = float.MinValue;
        int index = 0;
        for (int i = 0; i < population.Count; i++)
        {
            if (population[i].fitness > maxFitness)
            {
                maxFitness = population[i].fitness;
                index = i;
            }
        }

        pruebaGeneticPathfinder fittest = population[index];
        population.Remove(fittest);
        return fittest;
    }

    bool HasActive()
    {
        for (int i = 0; i < population.Count; i++)
        {
            if (population[i].targetFound)
            {
                haveWin = true;
                destroyPopulation(i);
                return true;
            }

            if (!population[i].hasFinished || population[i].targetFound)
            {
                return true;
            }
        }
        return false;
    }

    void destroyPopulation(int winner)
    {
        for (int i = 0; i < population.Count; i++)
        {
            if(i != winner)
            {
                Destroy(population[i].gameObject);
            }
            
        }
    }
}

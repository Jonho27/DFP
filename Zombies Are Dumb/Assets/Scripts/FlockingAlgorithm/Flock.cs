using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

	public FlockAgent agentPrefab;
	List<FlockAgent> agents = new List<FlockAgent>();
	public FlockBehavior behavior;
    public Vector3 spawnPosition = Vector3.zero;

	[Range(1, 500)]
	public int startingCount = 250;
	const float AgentDensity = 0.08f;

	[Range(0.75f, 1f)]
	public float driveFactor = 1f;
	[Range(0.75f, 100f)]
	public float maxSpeed = 0.75f;
	[Range(1f, 20f)]
	public float neighbourRadius = 3f;
	[Range(0f, 2f)]
	public float avoidanceRadiusMultiplier = 1f;

	float squareMaxSpeed;
	float squareNeighbourRadius;
	float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }


	// Start is called before the first frame update
	void Start()
    {
        if (ChooseDifficulty.difficulty == 0)
        {
            startingCount = 5;
        }

        else if (ChooseDifficulty.difficulty == 1)
        {
            startingCount = 10;
        }

        else if (ChooseDifficulty.difficulty == 2)
        {
            startingCount = 15;
        }
        spawnPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.05f, gameObject.transform.position.z);
		squareMaxSpeed = maxSpeed * maxSpeed;
		squareNeighbourRadius = neighbourRadius * neighbourRadius;
		squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for(int i = 0; i < startingCount; i++)
		{
            FlockAgent newAgent = Instantiate(agentPrefab, spawnPosition, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f), transform);
            newAgent.name = gameObject.name + ".Agent." + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }

	}

    // Update is called once per frame
    void Update()
    {
        if(agents.Count == 0)
        {
            Destroy(gameObject);
        }

        foreach(FlockAgent agent in agents)
        {
            if (agent.imDead)
            {
                agents.Remove(agent);

                Destroy(agent.gameObject);
            }

            else
            {
                List<Transform> context = GetNearbyObjects(agent);

                Vector3 move = behavior.CalculateMove(agent, context, this);
                move *= driveFactor;
                if (move.sqrMagnitude > squareMaxSpeed)
                {
                    move = move.normalized * maxSpeed;
                }

                agent.Move(move);

            }


        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighbourRadius);
        foreach(Collider c in contextColliders)
        {
            if(c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }

        return context;
    }


}

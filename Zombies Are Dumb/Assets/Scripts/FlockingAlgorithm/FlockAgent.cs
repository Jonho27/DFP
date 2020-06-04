using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]


public class FlockAgent : MonoBehaviour
{
    
    
    Flock agentFlock;
    public bool imDead;
    public Flock AgentFlock { get { return agentFlock; } }
    Collider agentCollider;
    public Collider AgentCollider { get { return agentCollider; }}
    public string myName;
    public bool playerSeen;
    public ChaseBrother chaseBrother;
    public ChasePlayer chasePlayer;

    // Start is called before the first frame update
    void Start()
    {
        chaseBrother = gameObject.GetComponent<ChaseBrother>();
        chasePlayer = gameObject.GetComponent<ChasePlayer>();
        imDead = false;
        agentCollider = GetComponent<Collider>();
    }

    void Update()
    {

    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }

    public void Move(Vector3 velocity)
    {
        if(chaseBrother != null)
        {
            if (!chaseBrother.playerSeen)
            {
                transform.forward = velocity;
                transform.position += velocity * Time.deltaTime;
            }
        }

        else if (chasePlayer != null)
        {
            if (!chasePlayer.playerSeen)
            {
                transform.forward = velocity;
                transform.position += velocity * Time.deltaTime;
            }
        }

        
        
    }


}

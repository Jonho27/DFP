using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleZombie : MonoBehaviour
{
	public Transform player;
	public Transform brother;
	public Animator anim;
	public bool playerSeen = false;
	public float cooldown;


	public float moveSpeed = 0.8f;
	public float rotSpeed = 100f;

	private bool isWandering = false;
	private bool isRotatingLeft = false;
	private bool isRotatingRight = false;
	private bool isWalking = false;


	// Start is called before the first frame update
	void Start()
	{
		cooldown = 0f;
		anim = this.gameObject.GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player").transform;
		brother = GameObject.FindGameObjectWithTag("Brother").transform;
	}

	// Update is called once per frame
	void Update()
	{


		cooldown -= Time.deltaTime;
		Vector3 directionPlayer = player.position - this.transform.position;
		Vector3 directionBrother = brother.position - this.transform.position;
		float angle = Vector3.Angle(directionPlayer, this.transform.forward);

		if (this.GetComponent<Vida>().valor > 0)
		{
			if (Vector3.Distance(player.position, this.transform.position) < 10 && angle < 45)
			{
				playerSeen = true;
				directionPlayer.y = 0;
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(directionPlayer), 0.1f);

				if (directionPlayer.magnitude > 2.5)
				{
					anim.SetBool("isAttacking", false);
					anim.SetBool("isWalking", true);
					this.transform.Translate(0f, 0f, 0.05f);
				}

				else
				{

					anim.SetBool("isWalking", false);
					anim.SetBool("isAttacking", true);
					if (cooldown <= 0f)
					{
						player.GetComponent<Vida>().recibirDaño(5f);
						cooldown = 1.5f;
					}
				}

			}

			else if (Vector3.Distance(brother.position, this.transform.position) < 10 && angle < 45)
			{
				playerSeen = true;
				directionBrother.y = 0;
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(directionBrother), 0.1f);

				if (directionBrother.magnitude > 2.5)
				{
					anim.SetBool("isAttacking", false);
					anim.SetBool("isWalking", true);
					this.transform.Translate(0f, 0f, 0.05f);
				}

				else
				{
					anim.SetBool("isWalking", false);
					anim.SetBool("isAttacking", true);
					if (cooldown <= 0f)
					{
						brother.GetComponent<Vida>().recibirDaño(5f);
						cooldown = 1.5f;
					}
				}

			}

			else
			{
                
				//Debug.Log("Playerseen ya no");
				playerSeen = false;

				anim.SetBool("isWalking", true);
				anim.SetBool("isAttacking", false);

				randomMovement();


			}
		}


	}

    public void randomMovement()
	{
        RaycastHit hit;
        float[] angulos = { 45, -45 };

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 8f) && Physics.Raycast(transform.position, (transform.forward + transform.right).normalized, out hit, 5f) && Physics.Raycast(transform.position, (transform.forward - transform.right).normalized, out hit, 5f))
        {
            transform.Rotate(new Vector3(0, 180f, 0));
        }

        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 8f) && Physics.Raycast(transform.position, (transform.forward + transform.right).normalized, out hit, 5f) && !Physics.Raycast(transform.position, (transform.forward - transform.right).normalized, out hit, 5f))
        {
            transform.Rotate(new Vector3(0, -45f, 0));
        }

        else if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 8f) && Physics.Raycast(transform.position, (transform.forward + transform.right).normalized, out hit, 5f) && Physics.Raycast(transform.position, (transform.forward - transform.right).normalized, out hit, 5f))
        {
            transform.Rotate(new Vector3(0, 180f, 0));
        }

        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 8f) && !Physics.Raycast(transform.position, (transform.forward + transform.right).normalized, out hit, 5f) && !Physics.Raycast(transform.position, (transform.forward - transform.right).normalized, out hit, 5f))
        {
            transform.Rotate(new Vector3(0, angulos[(int)Random.value], 0));
        }

        transform.position += transform.forward * 0.04f;

    }




}

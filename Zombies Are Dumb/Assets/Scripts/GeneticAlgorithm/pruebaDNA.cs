using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class pruebaDNA
{

	public List<Vector3> genes = new List<Vector3>();
	public pruebaDNA(int genomeLenght = 5)
	{
		//Debug.Log("Lo hago random");
		for (int i = 0; i < genomeLenght; i++)
		{
			genes.Add(new Vector3(Random.Range(-1.0f, 1.0f), 0f, Random.Range(-1.0f, 1.0f)));
		}
	}

	public pruebaDNA(pruebaDNA parent, pruebaDNA partner, float mutationRate = 0.01f)
	{
		for (int i = 0; i < parent.genes.Count; i++)
		{
			float mutationChance = Random.Range(0.0f, 1.0f);
			if (mutationChance <= mutationRate)
			{
				genes.Add(new Vector3(Random.Range(-1.0f, 1.0f), 0f, Random.Range(-1.0f, 1.0f)));
			}
			else
			{
				int chance = Random.Range(0, 2);
				if (chance == 0)
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

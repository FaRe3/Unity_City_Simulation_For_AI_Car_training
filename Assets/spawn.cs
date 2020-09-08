using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour {

	public GameObject Human;
	public float spawnTime = 3f;
	int i;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("SpawnBall", spawnTime, spawnTime);
	}

	// Update is called once per frame
	void Update()
	{

		if (Time.time>  i)
		{
			i += 5;
			Instantiate(Human,gameObject.transform);
		}
	}
}

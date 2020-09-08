using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class cityArea : Area
{
	public GameObject target;
	public int numtarget;
	public bool respawnTargets;
	public float rangeX;
	public float rangeZ;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	void CreateTargets(int numTar, GameObject target)
	{
		for (int i = 0; i < numTar; i++)
		{
			GameObject Tar = Instantiate(target, new Vector3(Random.Range(rangeX,-rangeX), 1f,Random.Range(rangeZ,-rangeZ)) + transform.position, Quaternion.Euler(new Vector3(0f, Random.Range(0f, 360f), 90f)));
			
			Tar.GetComponent<logic>().respawn = respawnTargets;
			Tar.GetComponent<logic>().myArea = this;
		}
	}

	public void ResetCityArea(GameObject[] agents)
	{
		foreach (GameObject agent in agents)
		{
			if (agent.transform.parent == gameObject.transform)
			{
				agent.transform.position = new Vector3(Random.Range(-rangeX, rangeX), 2f,
					Random.Range(-rangeZ, rangeZ))
					+ transform.position;
				agent.transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));
			}
		}

		CreateTargets(numtarget , target);
	}

	public override void ResetArea()
	{
	}
}

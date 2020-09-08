using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using UnityEngine.UI;

public class cityAcademy : Academy
{
	[HideInInspector]
	public GameObject[] agents;
	[HideInInspector]
	public cityArea[] listArea;

	public int totalScore;
	public Text scoreText;
	public override void AcademyReset()
	{
		ClearObjects(GameObject.FindGameObjectsWithTag("target"));
		agents = GameObject.FindGameObjectsWithTag("agent");
		listArea = FindObjectsOfType<cityArea>();
		foreach (cityArea ba in listArea)
		{
			ba.ResetCityArea(agents);
		}

		totalScore = 0;
	}

	void ClearObjects(GameObject[] objects)
	{
		foreach (GameObject bana in objects)
		{
			Destroy(bana);
		}
	}

	public override void AcademyStep()
	{
		scoreText.text = string.Format(@"Score: {0}", totalScore);
	}
}

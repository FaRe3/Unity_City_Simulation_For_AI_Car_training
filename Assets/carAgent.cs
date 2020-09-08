using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class carAgent : Agent
{
	private cityAcademy myAcademy;
	public GameObject area;
	cityArea myArea;
	bool satiated;
	Rigidbody agentRb;
	private int things;
	private float laser_length;
	// Speed of agent rotation.
	public float turnSpeed = 300;
	public float effectTime;
    public int re=5;
    public int pu = -1;

	// Speed of agent movement.
	public float moveSpeed = 2;
	public Material normalMaterial;
	public Material badMaterial;
	public Material goodMaterial;
	public bool contribute;
	private RayPercept rayPer;
	public bool useVectorObs;
	public float posX;
	public float posZ;
    private float sTime;


	public override void InitializeAgent()
	{
		base.InitializeAgent();
		agentRb = GetComponent<Rigidbody>();
		Monitor.verticalOffset = 1f;
		myArea = area.GetComponent<cityArea>();
		rayPer = GetComponent<RayPercept>();
		myAcademy = FindObjectOfType<cityAcademy>();
		SetResetParameters();
	}

	public override void CollectObservations()
	{
		if (useVectorObs)
		{
			float rayDistance = 20f;
			float[] rayAngles = { 20f, 90f, 160f, 45f, 135f, 70f, 110f };
			string[] detectableObjects = { "obstacle", "target","red","agent","human" };
			AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, -.75f, .75f));
			Vector3 localVelocity = transform.InverseTransformDirection(agentRb.velocity);
			AddVectorObs(localVelocity.x);
			AddVectorObs(localVelocity.z);
		}
	}

	public Color32 ToColor(int hexVal)
	{
		byte r = (byte)((hexVal >> 16) & 0xFF);
		byte g = (byte)((hexVal >> 8) & 0xFF);
		byte b = (byte)(hexVal & 0xFF);
		return new Color32(r, g, b, 255);
	}

	public void MoveAgent(float[] act)
	{
		if (Time.time > effectTime) 
		{
			if (satiated) 
			{
				Unsatiate ();
			}
		}
		Vector3 dirToGo = Vector3.zero;
		Vector3 rotateDir = Vector3.zero;

		if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous) {
			dirToGo = transform.forward * Mathf.Clamp (act [0], -1f, 1f);
			rotateDir = transform.up * Mathf.Clamp (act [1], -1f, 1f);
		} else {
			//var forwardAxis = (int)act [0];
			//var rightAxis = (int)act [1];
			var rotateAxis = (int)act [0];

			/*switch (forwardAxis) {
			case 1:
				dirToGo = transform.forward;
				break;
			case 2:
				dirToGo = -transform.forward;
				break;
			}*/

		/*	switch (rightAxis) {
			case 1:
				dirToGo = transform.right;
				break;
			case 2:
				dirToGo = -transform.right;
				break;
			}*/

			switch (rotateAxis) {
			case 1:
				rotateDir = -transform.up;
				break;
			case 2:
				rotateDir = transform.up;
				break; 
			}
				
			agentRb.AddForce (transform.forward * moveSpeed, ForceMode.VelocityChange);
			transform.Rotate (rotateDir, Time.fixedDeltaTime * turnSpeed);
			//AddReward(-1f / agentParameters.maxStep);
		}

		if (agentRb.velocity.sqrMagnitude > 25f) { // slow it down
			agentRb.velocity *= 0.95f;
		}
	}


	void Satiate()
	{
		satiated = true;
        agentRb.velocity *= 0.0f;
        effectTime = Time.time+5f;
	}

	void Unsatiate()
	{
		satiated = false;
	}



	public override void AgentAction(float[] vectorAction, string textAction)
	{
		MoveAgent(vectorAction);
	}

	public override void AgentReset()
	{
		Unsatiate();
		agentRb.velocity = Vector3.zero;
		things = 0;
		transform.position = new Vector3(posX,1.75f,posZ)
			+ area.transform.position;
		transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0, 360)));

		SetResetParameters();
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("target"))
		{
			Satiate ();
			collision.gameObject.GetComponent<logic>().OnEaten();
			AddReward(re);
			things += 1;
			if (contribute)
			{
				myAcademy.totalScore += 5;
			}
		}
		if (collision.gameObject.CompareTag("obstacle"))
		{
			//collision.gameObject.GetComponent<logic>().OnEaten();

			AddReward(pu);
			if (contribute)
			{
				myAcademy.totalScore -= 1;
			}
		}
		if (collision.gameObject.CompareTag("human"))
		{
			//collision.gameObject.GetComponent<logic>().OnEaten();

			AddReward(pu);
			if (contribute)
			{
				myAcademy.totalScore -= 1;
			}
		}
		if (collision.gameObject.CompareTag("animal"))
		{
			//collision.gameObject.GetComponent<logic>().OnEaten();

			AddReward(pu);
			if (contribute)
			{
				myAcademy.totalScore -= 1;
			}
		}
		if (collision.gameObject.CompareTag("agent"))
		{
			//collision.gameObject.GetComponent<logic>().OnEaten();

			AddReward(pu);
			if (contribute)
			{
				myAcademy.totalScore -= 1;
			}
		}
        if (collision.gameObject.CompareTag("red"))
        {
            if (contribute)
            {
                Satiate();
            }
        }
    }

	public override void AgentOnDone()
	{

	}

	public void SetResetParameters()
	{
		var agent_scale = myAcademy.resetParameters["agent_scale"];
		gameObject.transform.localScale = new Vector3(2*agent_scale, agent_scale, 3*agent_scale);
	}
		
}

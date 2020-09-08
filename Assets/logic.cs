using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logic : MonoBehaviour {

	public bool respawn;
	public cityArea myArea;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void OnEaten() {
		if (respawn) 
		{
			transform.position = new Vector3(Random.Range(-myArea.rangeX, myArea.rangeX), 
				3f, 
				Random.Range(-myArea.rangeZ, myArea.rangeZ)) + myArea.transform.position;
		}
		else 
		{
			Destroy(gameObject);
		}
	}
}

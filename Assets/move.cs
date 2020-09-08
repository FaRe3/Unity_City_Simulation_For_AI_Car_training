using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour 
{
	public float fSpeed;
    public float dTime=10.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.position + fSpeed * transform.forward;
		Object.Destroy(gameObject, dTime);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRed : MonoBehaviour {

    public GameObject Red;
    public float spawnTime = 3f;

    // Use this for initialization
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > spawnTime)
        {
            spawnTime += 5;
            Instantiate(Red, gameObject.transform);
        }
    }
}

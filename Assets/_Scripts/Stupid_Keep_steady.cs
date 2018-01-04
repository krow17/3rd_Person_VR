using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stupid_Keep_steady : MonoBehaviour {

    float y = 2.0f;

	// Use this for initialization
	void Start ()
    {
        y = transform.position.y;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndpointManager : MonoBehaviour {

    public bool collided = false;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            collided = true;
            //print("Player tripped endpoint");
        }
    }
}

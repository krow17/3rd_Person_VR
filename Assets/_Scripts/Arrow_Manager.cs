using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_Manager : MonoBehaviour {

    public GameObject camera;
    public GameObject arrow;
    public float distance;

	// Use this for initialization
	void Start () {
        arrow = this.gameObject;
        camera = GameObject.Find("Maze").GetComponent<Map_Manager>().camera;
        arrow.gameObject.GetComponent<Renderer>().enabled = false;
        print("arrow manager is working properly");
	}
	
	// Update is called once per frame
	void Update () {
        distance = Mathf.Abs(Vector3.Distance(this.transform.position, camera.transform.position));

        //if(distance > 11)
        //{
        //    arrow.gameObject.GetComponent<Renderer>().enabled = false;
        //}

        //if (distance < 11)
        //{
        //    arrow.gameObject.GetComponent<Renderer>().enabled = true;
        //}

        //if(distance < 3)
        //{
        //    Destroy(arrow.gameObject);
        //}
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cam_Dependent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onMenuClick()
    {
        if (GameObject.FindGameObjectWithTag("ID") != null)
        {
            return;
        }
        else
            SceneManager.LoadScene("_Scenes/Camera_Dependent");
    }
}

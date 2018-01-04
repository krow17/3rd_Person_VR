using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class practice : MonoBehaviour {

    string mode = "practice";
    string map = "Map0";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onModeClick()
    {
        PlayerPrefs.SetString("Map", map);
        PlayerPrefs.SetString("Mode", mode);
    }

  
}

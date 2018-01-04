using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map5 : MonoBehaviour {

    string map = "Map5";

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onMapClick()
    {
        PlayerPrefs.SetString("Map", map);
    }
}

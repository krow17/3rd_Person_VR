using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map9 : MonoBehaviour {
    string map = "Map9";

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

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map6 : MonoBehaviour {

    string map = "Map6";

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

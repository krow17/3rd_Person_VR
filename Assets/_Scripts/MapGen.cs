using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapGen : MonoBehaviour {

    StreamWriter sw;

    int obj;
    int rotation;
    List<float> x;
    List<float> z;
    Mesh mesh;
    string filename = "Map";

    System.Random rnd = new System.Random();

    // Use this for initialization
    void Start () {
        x = new List<float>();
        z = new List<float>();
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("cube");
        print("length of tiles: " + tiles.Length);
        for(int i = 0; i < tiles.Length; i++)
        {
                //print("found cube");
                x.Add(tiles[i].transform.position.x);
                z.Add(tiles[i].transform.position.z);
        }
        for (int i = 0; i < 10; i++)
        {
            filename = "Map" + i;
            sw = new StreamWriter(filename + ".txt");
            for (int j = 0; j < 49; j++)
            {

                Console.WriteLine(filename);
                obj = rnd.Next(0, 4);
                rotation = rnd.Next(0, 2);
                sw.WriteLine(obj + ","+ x[j] + "," + z[j] + "," + rotation);
                sw.Flush();
            }
        }
        print("MapGen was successful");
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_obs : MonoBehaviour {

    public Vector3[] locations;
    public Vector3[] rotations;
    public TextAsset ta;
    public string[] rows;
    public string[] subs;
    public GameObject[] obstacles;
    public int[] objectIndex;
    public GameObject obs;
    public GameObject map;

    public List<GameObject> arrows;

    public string toLoad;
    string setMode;

    int arrowCounter = 0;
    int straight = 0;
    int vert = 1;
    float temp = 0.0f;

    int rock = 0;
    int tree = 1;
    int corner = 2;
    int mush = 3;
    int finish = 4;
    int arrow = 5;
   
    
    string obj;
    private string FileName = "Data.txt";
    StreamWriter sw;

	// Use this for initialization
	void Start ()
    {

        setMode = PlayerPrefs.GetString("Mode");
        toLoad = PlayerPrefs.GetString("Map");
        if (toLoad == "")
        {
            Debug.LogWarning("loading default map");
            ta = Resources.Load("Map1") as TextAsset;
        }
        else
        {
            ta = Resources.Load(toLoad) as TextAsset;
            Debug.Log("Loaded map: " + toLoad);
        }
        obstacles = Resources.LoadAll<GameObject>("Obstacles");
        map = GameObject.Find("Maze");
        //print("Number of obstacles to load: " + obstacles.Length);

        if (ta != null)
        {
            rows = (ta.text.Split('\n'));
            //print("number of rows: " + rows.Length);
        }
        locations = new Vector3[rows.Length];
        rotations = new Vector3[rows.Length];
        objectIndex = new int[rows.Length];
        //read in list of coordinates by row first, then split each row into three
        //values. These values will be saved as (x,y,z) coordinates in the locations array
        for(int i = 0; i < rows.Length; i++)
        {
            subs = rows[i].Split(',');
            objectIndex[i] = int.Parse(subs[0]);
            if (objectIndex[i] == 5)
            {
                arrowCounter++;
            }
            float x = float.Parse(subs[1]);
            float z = float.Parse(subs[2]);
            int rot = int.Parse(subs[3]);
            //print("(" + x + ", " + z + ")");
            //print("rotation: " + rot);
            if (rot == 0)
                temp = 0.0f;
            else if (rot == 1)
                temp = 90.0f;
            locations[i] = new Vector3(x, 0.0f, z);
            rotations[i] = new Vector3(0.0f, temp, 0.0f);

        }
        loadObstacles();
	}

    public void loadObstacles()
    {
        for(int i = 0; i < objectIndex.Length; i++)
        {

            if(objectIndex[i] == 5 && setMode == "")
            {
                obs = obstacles[arrow];
                locations[i].y += 1.65f;
                obs = (GameObject)Instantiate(obs, locations[i], Quaternion.identity);
                obs.transform.Rotate(0.0f, rotations[i].y, -90.0f);
                arrows.Add(obs);
                //SetParent(obs, map);
            }
            if(objectIndex[i] == 0)
            {
                obs = obstacles[rock];
                obs = (GameObject)Instantiate(obs, locations[i], Quaternion.identity);
                obs.transform.Rotate(rotations[i]);
                SetParent(obs, map);
            }

            if(objectIndex[i] == 1)
            {
                obs = obstacles[tree];
                obs = (GameObject)Instantiate(obs, locations[i], Quaternion.identity);
                obs.transform.Rotate(rotations[i]);
                SetParent(obs, map);
            }

            if(objectIndex[i] == 2)
            {
                obs = obstacles[corner];
                obs = (GameObject)Instantiate(obs, locations[i], Quaternion.identity);
                obs.transform.Rotate(rotations[i]);
                SetParent(obs, map);
            }

            if(objectIndex[i] == 4)
            {
                obs = obstacles[finish];
                obs = (GameObject)Instantiate(obs, locations[i], Quaternion.identity);
                obs.transform.Rotate(rotations[i]);
                SetParent(obs, map);
            }

            if(objectIndex[i] == 3)
            {
                obs = obstacles[mush];
                obs = (GameObject)Instantiate(obs, locations[i], Quaternion.identity);
                obs.transform.Rotate(rotations[i]);
                SetParent(obs, map);
            }
            
        }
        ExtendMap();
        print("size of arrows = " + arrows.Count);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            //float finishTime = Time.timeSinceLevelLoad;
            //sw.WriteLine("Time spent in Practice:P " + finishTime);
            //sw.WriteLine("");
            //sw.Flush();
            SceneManager.LoadScene("_Scenes/Main_Menu");
        }
    }

    public void SetParent(GameObject child, GameObject newParent)
    {
        child.transform.parent = newParent.transform;
    }

    public void ExtendMap()
    {
        GameObject temp = map;
        temp = (GameObject)Instantiate(temp, temp.transform.position, Quaternion.identity);
        temp.transform.position = new Vector3(map.transform.position.x, map.transform.position.y, map.transform.position.z - 35);
        temp.transform.rotation = Quaternion.Euler(0.0f, -180.0f, 0.0f);
        temp.gameObject.GetComponent<Load_obs>().enabled = false;
        temp.gameObject.GetComponent<Map_Manager>().enabled = false;

    }


}

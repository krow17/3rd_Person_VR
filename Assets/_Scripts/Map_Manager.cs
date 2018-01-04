using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class Map_Manager : MonoBehaviour {

    Vector3 startPos; //where did the player start this map
    Vector3 finishPos; //where did the player cross the finish line
    Vector3 startDirection; //which way the start actually is
    Vector3 chosenDirection; //which way the player thinks the start is
    Vector3 camDirection; //which way is the player facing
    Vector3 planeNormal = new Vector3(0.0f, 1.0f, 0.0f);
    Vector3 finalPos;
    

    public GameObject player;
    public GameObject camera; //center eye anchor
    public GameObject finish;

    bool finished = false; //did the player trip the finish line?
    bool backToStart = false;
    public bool buttonTwo = false; //did the player press the B button?


    private string FileName = "Data.txt";
    private string TextToWrite;
    string toLoad;
    string setMode;
    float time;
    float finishTime;
    float returnTime;

    public TextAsset ta;
    StreamWriter sw;

    private int ID; //unique ID number for each user

    List<GameObject> new_arrows;


    // Use this for initialization
    void Start ()
    {

        setMode = PlayerPrefs.GetString("Mode");
        toLoad = PlayerPrefs.GetString("Map");
        sw = new StreamWriter(Application.dataPath + "/" + FileName, true);
        sw.WriteLine("Phase: " + Application.loadedLevelName + ", " + toLoad);
        sw.Flush();
        if (setMode == "")
        {
            GameObject.Find("Maze").GetComponent<Map_Manager>().enabled = true;
        }
        else if (setMode == "practice")
        {
            GameObject.Find("Maze").GetComponent<Map_Manager>().enabled = false;
            sw.WriteLine("PRACTICE");
            sw.Flush();
        }
        player = GameObject.FindGameObjectWithTag("Player");
        //camera = GameObject.FindGameObjectWithTag("MainCamera");
        if(GameObject.Find("CenterEyeAnchor") != null)
        {
            camera = GameObject.Find("CenterEyeAnchor");
        }
        startPos = camera.transform.position;
        
        Debug.Log("Start Position: " + startPos);
        StartCoroutine(startFollow());
    }
	
	// Update is called once per frame
	void Update ()
    {

        finish = GameObject.FindGameObjectWithTag("finish");
        //finalPos = camera.transform.position;
        if (GameObject.FindGameObjectWithTag("finish") == null)
        {
            return;
        }
        finished = finish.GetComponent<EndpointManager>().collided;

        if(!finished)
        {
            return;
        }
        if (finished)
        {
            StartCoroutine(recordFollowData());
        }
    }

    void FixedUpdate()
    {
        RaycastHit seen;
        Ray raydirection = new Ray(camera.transform.position, camera.transform.forward); 
        camDirection = raydirection.direction;
        if(Physics.Raycast(camera.transform.position, raydirection.direction, out seen, 100))
        {
            
        }
        Debug.DrawRay(camera.transform.position, camera.transform.forward * 10, Color.black, 1);
       
    }

    public float getAngle(Vector3 A, Vector3 B)
    {
        float angle = Vector3.Angle(A, B);
        return angle;
    }

    

    IEnumerator startFollow()
    {
        //print("starting follow");
        yield return new WaitForSeconds(1);
        if (GameObject.Find("Maze").GetComponent<Load_obs>() == null)
        {
            print("ERROR: could not find Load_Obs");
        }
        else
        {
            //print("cloning arrows");
            new_arrows = new List<GameObject>(GameObject.Find("Maze").GetComponent<Load_obs>().arrows);
        }
        foreach(GameObject elem in new_arrows)
        {
           
            elem.GetComponent<Renderer>().enabled = false;
        }
        //print("arrows turned off");
        for (int i = 0; i < new_arrows.Count; i++)
        {
            new_arrows[i].GetComponent<Renderer>().enabled = true;
            while(new_arrows[i].GetComponent<Arrow_Manager>().distance > 1.5)
            {
                yield return null;
            }
            Destroy(new_arrows[i]);
        }
    }

    //This CoRoutine will take care of recording the data needed 
    //for the first half of the test. That is, seeing how closely 
    //the user can identify their original starting position
    IEnumerator recordFollowData()
    {
        GameObject.Find("Maze").GetComponent<AudioSource>().Play();
        time = Time.timeSinceLevelLoad;
        sw.WriteLine("Time to reach finish: " + time);
        sw.Flush();
        print("player at finish point");
        Destroy(finish.gameObject);
        finish.GetComponent<EndpointManager>().collided = false;
        finished = false;
        buttonTwo = false;

        //wait until 'B' button is pressed on Touch Controller
        yield return new WaitUntil(() => OVRInput.Get(OVRInput.Button.Two) == true);
        buttonTwo = false;
        finishPos = camera.transform.position;
        print("Finish Position: " + finishPos);
        startDirection = startPos - finishPos;
        startDirection = Vector3.ProjectOnPlane(startDirection, planeNormal);
        print("Start direction: " + startDirection);

        //get direction that user is looking when trigger is pulled
        chosenDirection = (camera.transform.forward);
        print("ray direction before projection ==" + chosenDirection);
        chosenDirection = Vector3.ProjectOnPlane(chosenDirection, planeNormal);
        

        
        print("Chosen direction: " + chosenDirection);
        
        //get angle between user direction and actual direction
        float angle = Vector3.Angle(startDirection, chosenDirection);
        print("Angle == " + angle);
        //write information to file "data.txt"
        sw.WriteLine("Angle == " + angle);
        sw.Flush();

        StartCoroutine(recordReturnData());
    }

    IEnumerator recordReturnData()
    {
        print("recording return data");
        backToStart = true;
        yield return new WaitForSeconds(1);
        while(true)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                break;
            yield return new WaitForSeconds(1);
            Vector3 temp = camera.transform.position;
            sw.Write(temp + ", ");
        }

        sw.WriteLine("");
        sw.Flush();
       // yield return new WaitUntil(() => OVRInput.Get(OVRInput.Button.Two) == true);
        backToStart = false;
        finalPos = camera.transform.position;
        float dist = Mathf.Abs(Vector3.Distance(finalPos, startPos));
        finishTime = Time.timeSinceLevelLoad;
        returnTime = finishTime - time;
        sw.WriteLine("Distance from starting position: " + dist);
        sw.WriteLine("Time to return to start: " + returnTime);
        sw.WriteLine("Time spent in trial: " + finishTime);
        sw.WriteLine("");
        sw.Flush();
        print("TRIAL COMPLETE");
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("_Scenes/Main_Menu");
    }


}

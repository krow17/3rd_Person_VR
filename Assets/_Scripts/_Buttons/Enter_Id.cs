using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Enter_Id : MonoBehaviour {

    string userID;
    public InputField inputfield;
    public Button enter;
    StreamWriter sw;
    private string FileName = "Data.txt";

    // Use this for initialization
    void Start () {
        sw = new StreamWriter(Application.dataPath + "/" + FileName, true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onEnterClick()
    {
        if (inputfield.text == "")
        {
            return;
        }
        else
        {
            userID = inputfield.text;
            sw.WriteLine("User: " + userID);
            sw.Flush();
            Destroy(inputfield.gameObject);
            Destroy(enter.gameObject);
        }
        
        
    }
}

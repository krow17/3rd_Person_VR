using System.Collections;
using System.Collections.Generic;
using UnityEngine;



using UnityStandardAssets.Cameras;

public class Basic_Camera_Follow : MonoBehaviour
    {

        public GameObject camera;
        public GameObject avatar;
       



        public Vector3 offset;
        Vector3 move;

        public bool pushed = false;
        public float speed;
        public float camSpeed = 3.0f;
        

        // Use this for initialization
        void Start()
        {
           
            avatar = GameObject.FindGameObjectWithTag("Player");
            offset = new Vector3(camera.transform.position.x - avatar.transform.position.x, 2.0f, camera.transform.position.z - avatar.transform.position.z);
            //camera.transform.position = offset;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (OVRInput.Get(OVRInput.Button.One))
            {
                pushed = true;
                //print("button is true");
            }

            else
            {
                pushed = false;
                camera.GetComponent<AutoCam>().enabled = false;
               
            }

            if (pushed)
            {
            camera.GetComponent<AutoCam>().enabled = true;
            }
            
        }
    }


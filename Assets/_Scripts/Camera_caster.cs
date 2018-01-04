using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class Camera_caster : MonoBehaviour
    {

        public GameObject camera;
        public GameObject avatar;
        ThirdPersonCharacter tpc;
        RaycastHit hit;
        int layerMask = (1 << 8);
        


        public Vector3 offset;
        Vector3 move;

        public bool pushed = false;
        public float speed;
        public float camSpeed;
        public float sightlength;

        // Use this for initialization
        void Start()
        {
            layerMask = ~layerMask;
            avatar = GameObject.FindGameObjectWithTag("Player");
            camera.transform.position = new Vector3(transform.position.x, 2.0f, transform.position.z);
            offset = new Vector3(camera.transform.position.x - avatar.transform.position.x, 2.0f, camera.transform.position.z - avatar.transform.position.z);
            camera.GetComponent<Stupid_Keep_steady>().enabled = true;

        }

        // Update is called once per frame
        void Update()
        {
            if (OVRInput.Get(OVRInput.Button.One))
            {
                pushed = true;
                camera.GetComponent<AutoCam>().enabled = false;
                //print("button is true");
            }

            else
            {
                pushed = false;
            }

           
        }

        void FixedUpdate()
        {
            if (!pushed)
            {
                float step = camSpeed * Time.deltaTime;
                //camera.transform.position = Vector3.Lerp(camera.transform.position, avatar.transform.position + offset, step);
                camera.GetComponent<AutoCam>().enabled = true;
            }

            RaycastHit seen;
            Ray raydirection = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(raydirection, out seen, sightlength, layerMask))
            {
                
                    
                    if (pushed)
                    {
                        float step = speed * Time.deltaTime;
                        move = seen.point - avatar.transform.position;
                        avatar.GetComponent<ThirdPersonCharacter>().Move(move, false, false);
                        pushed = false;
                    }
                
            }
            Debug.DrawRay(transform.position, transform.forward, Color.black, 1); 
        }
    }
}

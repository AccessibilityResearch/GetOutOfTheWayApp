using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeze : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject freezePlayer = GameObject.Find("FirstPersonController");
        freezePlayer.GetComponent<FirstPersonController>().playerCanMove = false;
        freezePlayer.GetComponent<FirstPersonController>().cameraCanMove = false;
        freezePlayer.GetComponent<Rigidbody>().useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        

        // This releases the player from being frozen when the space key is pressed. Records the time, in order to normalize timing of run
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("kek");
            GameObject followMe = GameObject.Find("FirstPersonController");
            followMe.GetComponent<FirstPersonController>().playerCanMove = true;
            followMe.GetComponent<FirstPersonController>().cameraCanMove = true;
            followMe.GetComponent<Rigidbody>().useGravity = true;
            followMe.AddComponent<CollisionTracker>();
        }
        
    }
}

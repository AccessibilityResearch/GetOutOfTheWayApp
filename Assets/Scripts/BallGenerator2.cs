using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using AK.Wwise;


public class BallGenerator2 : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Ball;
    
    void Start()
    {

        InvokeRepeating("Generate", 1, 6);
        // Generate();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Generate()
    {

        /* Array containing starting coordinates for spheres */
        Vector3[] StartLocs = { new Vector3(Random.Range(20, 100), 0, Random.Range(-100, -20)), new Vector3(Random.Range(20, 100), 0, Random.Range(20, 100)), new Vector3(Random.Range(-100, -20), 0, Random.Range(20, 100)), new Vector3(Random.Range(-100, -20), 0, Random.Range(-100, -20)) };
        /* Vector3[] StartLocs = { new Vector3(98, 2, 0), new Vector3(98, 2, 44), new Vector3(-51, 2, 98), new Vector3(36, 2, 98), new Vector3(-96, 2, -98) };

    
        /* Array containing velocities applied to sphere after spawning. Corresponds to index of StarLoc coordinates to ensure path does not go through a pillar */
        Vector3[] Paths = { new Vector3(-9, 0, -8), new Vector3(-10, 0, 0), new Vector3(4, 0, -4), new Vector3(0, 0, -9), new Vector3(3, 0, 7) };

        /* Generate a random number to choose which path is taken by the sphere */
        int PathSelection = Random.Range(0, StartLocs.Length);

        /* Creates a blank sphere */
        GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Sphere); 
        /* Add a Rigidbody component to the sphere, and set the Use Gravity attribute to false, so sphere does not fall in the Y coordinate */
        temp.AddComponent<Rigidbody>();
        temp.GetComponent<Rigidbody>().useGravity = false;

        /* Adds a script to detect collision that causes the sphere to delete itself when it collides with the player or a wall */
        temp.AddComponent<CollisionDetect>();

        /* Add necessary components to the sphere to allow for Wwise Event to play sound */
        temp.AddComponent<AkGameObj>();
        temp.AddComponent<AkAmbient>();

        /* Tell the object which sound from the soundbank to play */
        AkSoundEngine.PostEvent("Play_Engine_03", temp);

        /* Set the location of the sphere and add velocity to begin movement */
        temp.transform.localPosition = StartLocs[PathSelection];
        temp.GetComponent<Rigidbody>().velocity = Paths[PathSelection];



    }

}

/*Randomly generate 5 balls spawned from the wall to the inside with random speed, angle, starting point*/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using AK.Wwise;


public class RandomLinePath : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Ball;
    public float wallWidth = 400f;
    //Call the EnvironmentGeneratorPrefab1 script to store all the positions of goals and pillars
    private EnvironmentGeneratorPrefab1 envGen;
    private Vector3 direction;

    void Start()
    {

        //InvokeRepeating("GeneratePath", 1, 6);
        /* Generate 5 balls at one time*/
        GeneratePath();

    }
    //Put goals positions and pillar positions into a list
    public List<Vector3> obstaclePositions()
    {
        envGen = FindObjectOfType<EnvironmentGeneratorPrefab1>();
        List<Vector3> pillarPositions = envGen.generatePillar();
        List<Vector3> goalPositions = envGen.generateGoal();

        // make a copy of the longer list to store all obstacles' positions that ball should avoid
        List<Vector3> obstaclePositions = new List<Vector3>(pillarPositions);
        for (int i = 0; i < goalPositions.Count; i++)
        {
            obstaclePositions.Add(goalPositions[i]);
        }
        return obstaclePositions;
    }

    public void GeneratePath()
    {
        List<Vector3> paths = obstaclePositions();

        for (int i = 0; i< 5; i++)
        {


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
            
            temp.GetComponent<Renderer>().material.color = new Color(0, 0, 0);
           
            float speed = Random.Range(15f, 20f);
            float angle = Random.Range(0f, 360f);

            /* Array containing starting coordinates for spheres */
            float x0 = Random.Range(-wallWidth, wallWidth);
            float z0 = Random.Range(-wallWidth, wallWidth);
            Vector3 randomPosition = new Vector3(x0, 5f, z0);
            /* Set the location of the sphere and add velocity to begin movement */
            temp.transform.localPosition = randomPosition;            
            temp.transform.localScale = Vector3.one * 5f;

            /* Moves the sphere with random directions*/
            Vector3 direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0f, Mathf.Sin(angle * Mathf.Deg2Rad)) * speed;

            float m = Mathf.Sin(angle * Mathf.Deg2Rad) / Mathf.Cos(angle * Mathf.Deg2Rad);
            float b = z0 - m * x0;
            foreach (Vector3 v in paths)
            {
                //If the pillars or goals in the path where balls travel, regenerate a new ball
                while (v[0] * m + b == v[2])
                {
                    angle = Random.Range(0f, 360f);
                    direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0f, Mathf.Sin(angle * Mathf.Deg2Rad)) * speed;
                }
            }
            temp.GetComponent<Rigidbody>().velocity = direction;
        }
 

    }



}

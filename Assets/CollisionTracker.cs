using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System.IO;
using System.Text;



public class CollisionTracker : MonoBehaviour
{


    
    public class collectedData
    {
        public int staticCollisions { get; set; } = 0;
        public int movingCollisions { get; set; } = 0;
        public List<string> position = new List<string>();
        public List<string> rotation = new List<string>();
        public System.DateTime baseTimer;
        public System.DateTime timer1;
        public System.DateTime timer2;
        public System.DateTime timer3;
    }

    collectedData tracker = new collectedData();
    

    private void Start()
    {
        string filePath = Application.dataPath + "/results.csv";
        StreamWriter writer = new StreamWriter(filePath);
        writer.WriteLine("Participant, Static Collisions, Moving Collisions, Location, Rotation");
        writer.Flush(); 
        writer.Close();

        // This will stop the player from moving ******AT ALL******
        GameObject freezePlayer = GameObject.Find("FirstPersonController");
        freezePlayer.GetComponent<FirstPersonController>().playerCanMove = false;
        freezePlayer.GetComponent<FirstPersonController>().cameraCanMove = false;
        freezePlayer.GetComponent<Rigidbody>().useGravity = false;
        // Ignore this for now :) InvokeRepeating("PositionPrinter", 2, 5);
        
    }

    public void PositionPrinter()
    {
        // This is my hack test to see if the info was being recorded 
        string gigaString = "";
        foreach(var position in tracker.position)
        {
            gigaString += " " + position;
        }

        string[] gigaArray = tracker.position.ToArray();
        Debug.Log(gigaArray[gigaArray.Length - 1]);
        
    }

    private void Update()
    {
        GameObject followMe = GameObject.Find("FirstPersonController");
        
        // This releases the player from being frozen when the space key is pressed. Records the time, in order to normalize timing of run
        if (followMe.transform.position == new Vector3(0,2,0) && Input.GetKeyDown("space"))
        {
            followMe.GetComponent<FirstPersonController>().playerCanMove = true;
            followMe.GetComponent<FirstPersonController>().cameraCanMove = true;
            followMe.GetComponent<Rigidbody>().useGravity = true;
            tracker.baseTimer = System.DateTime.Now;
            Debug.Log("Current Time " + tracker.baseTimer);
        }

    }

    private void FixedUpdate()
    {
        // The FPC object does a terrible job of recording the rotation, and I found the reticle tracks all three coordinates for it, so I went with that
        GameObject followMe = GameObject.Find("FirstPersonController");
        GameObject rotationRef = GameObject.Find("CrosshairAndStamina");
        // Convert Vector3 into a string resembling an array element to later convert into an array
        string convertedPosition = ("[" + followMe.transform.position.x + ", " + followMe.transform.position.y + ", " + followMe.transform.position.z + "]");
        string convertedRotation = ("[" + rotationRef.transform.rotation.x + ", " + rotationRef.transform.rotation.y + ", " + rotationRef.transform.rotation.z + "]");
        tracker.position.Add(convertedPosition);
        tracker.rotation.Add(convertedRotation);
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Pillar")
        {
            tracker.staticCollisions += 1;
            Debug.Log("Pillar hit " + tracker.staticCollisions);
        }
        else if (collision.gameObject.tag == "Wall")
        {
            tracker.staticCollisions += 1;
            Debug.Log("Wall hit " + tracker.staticCollisions);
        }
        else if (collision.gameObject.tag == "Ball")
        {
            tracker.movingCollisions += 1;
            Debug.Log("Ball hit " + tracker.movingCollisions);
        }
        else if (collision.gameObject.tag == "Goal")
        {
            string[] tempPosition = tracker.position.ToArray();
            string[] tempRotation = tracker.rotation.ToArray();
            string fullPosition = "";
            string fullRotation = "";

            for (int i = 0; i < tempPosition.Length - 2; i++)
            {
                fullPosition += tempPosition[i] + ", ";
            }
            fullPosition += tempPosition[tempPosition.Length - 1];

            for (int i = 0; i < tempRotation.Length - 2; i++)
            {
                fullRotation += tempRotation[i] + ", ";
            }
            fullRotation += tempRotation[tempRotation.Length - 1];


            tracker.timer1 = System.DateTime.Now;
            Debug.Log("Goal At " + tracker.timer1);
            string filePath = Application.dataPath + "/results.csv";
            StreamWriter writer = File.AppendText(filePath);
            writer.WriteLine("Goober McGee" + ", " + tracker.staticCollisions + ", " + tracker.movingCollisions + ", " + "\"" + fullPosition + "\"" + ", " + "\"" + fullRotation + "\"");
            writer.Flush();
            writer.Close();
        }
    }

}

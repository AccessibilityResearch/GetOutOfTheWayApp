using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using AK.Wwise;
using UnityEditor.Experimental.GraphView;

public class EnvironmentGeneratorPrefab1 : MonoBehaviour
{
    public GameObject Ball;
    public float wallWidth = 400f;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty tagsProp = tagManager.FindProperty("tags");

        // Adding a Tag
        string[] s = { "Pillar", "Ball", "Wall", "Goal" };

        for (int i = 0; i < s.Length; i++)
        {

            tagsProp.InsertArrayElementAtIndex(0);
            SerializedProperty n = tagsProp.GetArrayElementAtIndex(0);
            n.stringValue = s[i];
            tagManager.ApplyModifiedPropertiesWithoutUndo();
        }

        /* Hardcoded wall coordinates */
        Vector3[] WallCords = { new Vector3(0, 31, -400), new Vector3(400, 31, 0), new Vector3(-400, 31, 0), new Vector3(0, 31, 400) };

        /* Hardcoded wall orientation */
        Vector3[] WallOrient = { new Vector3(90, 0, 0), new Vector3(90, 0, 90), new Vector3(90, 0, 90), new Vector3(90, 0, 0) };

        Vector3 WallScale = new Vector3(800, 1, 62);

        GameObject[] Walls = new GameObject[WallCords.Length];

        /* Generates a new wall, using each set of coordinates from the WallCords array, and rotation from the WallOrient array */
        for (int i = 0; i < WallCords.Length; i++)
        {
            GameObject Wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Wall.AddComponent<Rigidbody>();
            Wall.GetComponent<Rigidbody>().useGravity = false;
            Wall.GetComponent<Rigidbody>().isKinematic = true;
            Wall.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            Wall.AddComponent<AkGameObj>();
            Wall.AddComponent<AkAmbient>();
            Wall.tag = "Wall";

            AkSoundEngine.PostEvent("Play_The_Creature_1", Wall);
            Wall.transform.localScale = WallScale;
            Wall.transform.localPosition = WallCords[i];
            Wall.transform.localRotation = Quaternion.Euler(WallOrient[i]);

            Walls[i] = Wall;

        }

        GameObject plane1 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane1.transform.localScale = new Vector3(80, 1, 80);
        GeneratePath();

    }
    // Update is called once per frame
    void Update()
    {
    }
    public List<Vector3> generateGoal()
    {
        List<Vector3> goalPositions = new List<Vector3>();
        //Generate the three goals
        for (int i = 0; i < 3; i++)
        {

            Vector3 goalPostion = new Vector3(Random.Range(-390, 390), 25f, Random.Range(-390, 390));
            GameObject goal = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            Destroy(goal.GetComponent<BoxCollider>());
            goal.AddComponent<Rigidbody>();
            goal.GetComponent<Rigidbody>().useGravity = false;
            goal.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            goal.AddComponent<AkGameObj>();
            goal.AddComponent<AkAmbient>();
            goal.tag = "Goal";
            goal.transform.localScale = new Vector3(16, 25, 16);
            goal.transform.localPosition = goalPostion;
            goal.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
            AkSoundEngine.PostEvent("Play_MUSCChim_InsJ_Hand_Bell_01_16_High_C", goal);
            goalPositions.Add(goalPostion);
        }
        return goalPositions;

    }
    public List<Vector3> generatePillar()
    {
        List<Vector3> pillarPositions = new List<Vector3>();
        Vector3 PillarScale = new Vector3(10, 30, 10);

        /* Generates a new pillar, using each set of coordinates from the PillarCords array. Requires Prefab */
        for (int i = 0; i < 3; i++)
        {
            Vector3 pillarPostion = new Vector3(Random.Range(-390, 390), 30f, Random.Range(-390, 390));
            GameObject pillar = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            pillar.AddComponent<Rigidbody>();
            pillar.GetComponent<Rigidbody>().useGravity = false;
            pillar.GetComponent<Rigidbody>().isKinematic = true;
            pillar.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            pillar.AddComponent<AkGameObj>();
            pillar.AddComponent<AkAmbient>();
            pillar.tag = "Pillar";
            pillar.GetComponent<MeshRenderer>().material.color = Color.gray;
            AkSoundEngine.PostEvent("Play_The_Creature_1", pillar);
            pillar.transform.localScale = PillarScale;
            pillar.transform.localPosition = pillarPostion;
            pillarPositions.Add(pillarPostion);

        }
        return pillarPositions;
    }
    public List<Vector3> obstaclePositions()
    {
        List<Vector3> pillarPositions = generatePillar();
        List<Vector3> goalPositions = generateGoal();

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

        for (int i = 0; i < 5; i++)
        {


            /* Creates a blank sphere */
            GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            /* Add a Rigidbody component to the sphere, and set the Use Gravity attribute to false, so sphere does not fall in the Y coordinate */
            temp.AddComponent<Rigidbody>();
            temp.GetComponent<Rigidbody>().useGravity = false;



            /* Add necessary components to the sphere to allow for Wwise Event to play sound */
            temp.AddComponent<AkGameObj>();
            temp.AddComponent<AkAmbient>();
            temp.tag = "Ball";
            /* Tell the object which sound from the soundbank to play */
            AkSoundEngine.PostEvent("Play_Engine_03", temp);

            temp.GetComponent<Renderer>().material.color = new Color(0, 0, 0);

            float speed = Random.Range(15f, 20f);
            float angle = Random.Range(0f, 180f);

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
                int loopCounter = 0;
                //If the pillars or goals in the path where balls travel, regenerate a new ball
                while (v[2] - 16 <= v[0] * m + b && v[0] * m + b <= v[2] + 16 && loopCounter < 1000)
                {
                    angle = Random.Range(0f, 180f);
                    direction = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0f, Mathf.Sin(angle * Mathf.Deg2Rad)) * speed;
                }
            }

            temp.GetComponent<Rigidbody>().velocity = direction;
            Debug.Log("E"+direction);


            /* Adds a script to detect collision that causes the sphere to delete itself when it collides with the player or a wall */
            temp.AddComponent<CollisionDetect>();
            // Get a reference to the newly added CollisionDetect component
            CollisionDetect collisionDetect = temp.GetComponent<CollisionDetect>();

            // Set the direction variable on the CollisionDetect component
            collisionDetect.direction = direction;

        }



    }

    

}
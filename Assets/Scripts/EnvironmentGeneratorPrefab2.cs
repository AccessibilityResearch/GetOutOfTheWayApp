using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using AK.Wwise;
using UnityEditor.Experimental.GraphView;

public class EnvironmentGeneratorPrefab2 : MonoBehaviour
{
    public float wallWidth = 100f;
    // Start is called before the first frame update
    void Start()
    {

        /* Hardcoded pillar coordinates */
        Vector3[] PillarCords = { new Vector3(76, 7, 77), new Vector3(-40, 7, 61), new Vector3(3, 7, -58), new Vector3(50, 7, 17), new Vector3(-76, 7, -26),
          new Vector3(54, 7, -61), new Vector3(-9, 7, -3), new Vector3(10, 7, 58)};

        /* Hardcoded wall coordinates */
        Vector3[] WallCords = { new Vector3(14, 121, -987), new Vector3(952, 121, -33), new Vector3(-974, 121, -5), new Vector3(-9, 121, 984) };

        /* Hardcoded wall orientation */
        Vector3[] WallOrient = { new Vector3(90, 0, 0), new Vector3(90, 0, 90), new Vector3(270, 0, 270), new Vector3(270, 0, 0) };

        Vector3 WallScale = new Vector3(197, 77, 26);
        Vector3 PillarScale = new Vector3(5, 15, 5);

        GameObject[] Walls = new GameObject[WallCords.Length];
        GameObject[] Pillars = new GameObject[PillarCords.Length];

        /* Generates a new pillar, using each set of coordinates from the PillarCords array. Requires Prefab */
        for (int i = 0; i < PillarCords.Length; i++)
        {
            GameObject pillar = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            pillar.AddComponent<Rigidbody>();
            pillar.GetComponent<Rigidbody>().useGravity = false;
            pillar.GetComponent<Rigidbody>().isKinematic = true;
            pillar.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            pillar.AddComponent<AkGameObj>();
            pillar.AddComponent<AkAmbient>();
            pillar.GetComponent<MeshRenderer>().material.color = Color.gray;
            AkSoundEngine.PostEvent("Play_The_Creature_1", pillar);
            pillar.transform.localScale = PillarScale;
            pillar.transform.localPosition = PillarCords[i];
            Pillars[i] = pillar;
        }

        /* Generates a new wall, using each set of coordinates from the WallCords array, and rotation from the WallOrient array */
        for (int i = 0; i < WallCords.Length; i++)
        {
            GameObject Wall = GameObject.CreatePrimitive(PrimitiveType.Plane);
            Wall.AddComponent<Rigidbody>();
            Wall.GetComponent<Rigidbody>().useGravity = false;
            Wall.GetComponent<Rigidbody>().isKinematic = true;
            Wall.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            Wall.AddComponent<AkGameObj>();
            Wall.AddComponent<AkAmbient>();
            AkSoundEngine.PostEvent("Play_The_Creature_1", Wall);
            Wall.transform.localScale = WallScale;
            Wall.transform.localPosition = WallCords[i];
            Wall.transform.localRotation = Quaternion.Euler(WallOrient[i]);
            Walls[i] = Wall;
        }

        GameObject plane1 = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane1.transform.localScale = new Vector3(192, 1, 198);
        List<Vector3> positions = new List<Vector3>();



        for (int i = 0; i < 3; i++)
        {
            Vector3 goalPosition = new Vector3(Random.Range(-90, 90), 10f, Random.Range(-90, 90));

            GameObject goal = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            Destroy(goal.GetComponent<BoxCollider>());
            goal.AddComponent<Rigidbody>();
            goal.GetComponent<Rigidbody>().useGravity = false;
            goal.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            goal.AddComponent<AkGameObj>();
            goal.AddComponent<AkAmbient>();
            goal.transform.localScale = new Vector3(8, 10, 8);
            goal.transform.localPosition = goalPosition;

            goal.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
            AkSoundEngine.PostEvent("Play_MUSCChim_InsJ_Hand_Bell_01_16_High_C", goal);

            // Add a position to the list
            positions.Add(goalPosition);


        }
        foreach (Vector3 position in positions)
        {
            Debug.Log("p" + position);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
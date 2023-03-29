using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AK.Wwise;

public class CollisionDetect : MonoBehaviour
{
    public Vector3 direction; // the direction the ball is moving in
    public float speed = 10f;


    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        // AkSoundEngine.PostEvent("Stop_Engine_3", gameObject);
        if (collision.gameObject.CompareTag("Wall")) // check if the ball hit a wall
        {
            //the position where the ball hits the wall
            Vector3 normal = collision.contacts[0].point;
            Debug.Log(normal);
            transform.localPosition = normal;
            //After the ball hits the ball, the ball will bounce back in the opposite direction
            Vector3 newDirection = direction *(-1f);
            Debug.Log(newDirection);
            GetComponent<Rigidbody>().velocity = newDirection;
        }
    }
}
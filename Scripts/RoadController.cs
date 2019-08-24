using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour {

    private Rigidbody rb;
    private bool isPlayerGrounded;
    //public Transform roadDespawn;
    public Transform roadSpawn;
    private float speed = 1.0f;
    private bool canSpawnGround = false;

    private bool isPlayerJumping;
    private Vector3 destination;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isPlayerGrounded = PlayerController.isGrounded;
        rb.velocity = new Vector3(0, 0, 0);
        //roadDespawn = GameObject.Find("roadDespawn").transform;
        roadSpawn = GameObject.Find("roadSpawn").transform;

        isPlayerJumping = PlayerController.jump;
        destination = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        /*
        isPlayerGrounded = PlayerController.isGrounded;
        if (isPlayerGrounded == false)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, roadDespawn.position, step);
        }
        */

        isPlayerJumping = PlayerController.jump;
        if (isPlayerJumping == true)
        {
            destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.0f);
        }

        //transform.position = Vector3.Lerp(transform.position, destination, speed);
        //transform.position = destination;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination, step);

        if (transform.position.z <= -9.5f)
        {
            roadSpawn.gameObject.SendMessage("SpawnGround", 35);
            Destroy(gameObject);
        }
        
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour {

    private Rigidbody rb;
    private bool isPlayerGrounded;
    public Transform roadDespawn;
    public Transform roadSpawn;
    private float speed = 1f;
    private bool canSpawnGround = false;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        isPlayerGrounded = PlayerController.isGrounded;
        rb.velocity = new Vector3(0, 0, 0);
        roadDespawn = GameObject.Find("roadDespawn").transform;
        roadSpawn = GameObject.Find("roadSpawn").transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        isPlayerGrounded = PlayerController.isGrounded;
        if (isPlayerGrounded == false)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, roadDespawn.position, step);
        }

        if (transform.position.z <= -10f)
        {
            roadSpawn.gameObject.SendMessage("SpawnGround", 35);
            Destroy(gameObject);
        }
        
    }
    
}

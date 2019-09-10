using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour {

    private Rigidbody rb;
    public Transform roadSpawn;
    private float speed = 1.0f;

    private bool isPlayerMovingForward;
    private Vector3 destination;

    // Use this for initialization
    void Start()
    {
        roadSpawn = GameObject.Find("roadSpawn").transform;

        isPlayerMovingForward = PlayerController.forwardMotion;
        destination = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {

        isPlayerMovingForward = PlayerController.forwardMotion;
        if (isPlayerMovingForward)
        {
            destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.0f);
        }
        
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination, step);

        if (transform.position.z <= -2.5f)
        {
            roadSpawn.gameObject.SendMessage("SpawnGround", 35);
            Destroy(gameObject);
        }
        
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour {

    private Rigidbody rb;
    public float speed = 1.0f;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.right * speed;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}

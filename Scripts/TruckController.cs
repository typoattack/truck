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
        if (gameObject.tag == "Coin") transform.Rotate(new Vector3(90, 0, 0));
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (gameObject.tag == "Coin") transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
        if (transform.position.z <= -2.0f) Destroy(gameObject);
    }
}

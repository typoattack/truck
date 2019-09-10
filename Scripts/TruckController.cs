using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour {

    private Rigidbody rb;
    public float speed = 1.0f;
	private Vector3 direction;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
		direction = transform.right;
        rb.velocity = transform.right * speed;
        if (gameObject.tag == "Coin") transform.Rotate(new Vector3(90, 0, 0));
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gameObject.tag == "Coin")
        {
            transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
            if (rb.transform.position.x > 1)
            {
                rb.velocity = new Vector3(speed * -1, 0f, 0f);
            }
            else if (rb.transform.position.x < -1) rb.velocity = new Vector3(speed, 0f, 0f);
        }
        if (transform.position.z <= -2.0f) Destroy(gameObject);
	}

	void OnTriggerStay(Collider other)
	{
        if (gameObject.tag != "Coin" && other.gameObject.CompareTag("crumpleZone"))
        {
            speed = other.transform.parent.gameObject.GetComponent<Rigidbody> ().velocity.magnitude;
			rb.velocity = direction * speed;
		} 
	}
}

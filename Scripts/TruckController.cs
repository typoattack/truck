using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{

    private Rigidbody rb;
    public float speed = 1.0f;
    private Vector3 direction;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        direction = transform.right;
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z <= -2.0f) Destroy(gameObject);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("crumpleZone"))
        {
            speed = other.transform.parent.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
            rb.velocity = direction * speed;
        }
    }

}
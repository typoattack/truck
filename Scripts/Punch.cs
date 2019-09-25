using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    PlayerController parent;

    // Use this for initialization
    void Start()
    {
        parent = transform.GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Truck"))
        {
            parent.Punch(other);
        }
    }
}



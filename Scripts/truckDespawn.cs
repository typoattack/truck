using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class truckDespawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Truck") || other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
    }
}

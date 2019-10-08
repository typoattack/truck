using UnityEngine;
using System.Collections.Generic;

public class Punch : MonoBehaviour
{
    PlayerController parent;
    Queue<Collider> truckQueue = new Queue<Collider>();

    // Use this for initialization
    void Start()
    {
        parent = transform.GetComponentInParent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Truck"))
        {
            truckQueue.Enqueue(other);
            //Debug.Log("Truck Queued");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (truckQueue.Count > 0)
        {
            truckQueue.Dequeue();
            //Debug.Log("Truck Exited");
        }
    }

    public void ThrowPunch()
    {
        while (truckQueue.Count > 0)
        {
            Collider punchedTruck = truckQueue.Dequeue();
            parent.Punch(punchedTruck);
            //Debug.Log("Truck Punched");
        }
    }
}



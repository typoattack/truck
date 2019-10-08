using UnityEngine;
using System.Collections.Generic;

public class Punch : MonoBehaviour
{
    PlayerController parent;
    //Queue<Collider> truckQueue = new Queue<Collider>();
    List<Collider> truckList = new List<Collider>();

    // Use this for initialization
    void Start()
    {
        parent = transform.GetComponentInParent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Truck"))
        {
            //truckQueue.Enqueue(other);
            truckList.Add(other);
            Debug.Log("Truck Listed");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (truckList.Contains(other))
        {
            /*
            if (truckQueue.Count > 0)
            {
                truckQueue.Dequeue();
                Debug.Log("Truck Exited");
            }
            */
            truckList.Remove(other);
            Debug.Log("Truck Exited");
        }
    }

    public void ThrowPunch()
    {
        /*
        while (truckQueue.Count > 0)
        {
            Collider punchedTruck = truckQueue.Dequeue();
            parent.Punch(punchedTruck);
            Debug.Log("Truck Punched");
        }
        */
        foreach (Collider punchedTruck in truckList)
        {
            parent.Punch(punchedTruck);
            Debug.Log("Truck Punched");
        }
        truckList.Clear();
    }
}



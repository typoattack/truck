using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class truckSpawn : MonoBehaviour {

    public TruckController truck;
    public bool canSpawn = false;
    private float waitTime;
    private float timer = 0.0f;
    private int decider;
    private float truckSpeed;

    // Use this for initialization
    void Start ()
    {
        waitTime = Random.Range(2.0f, 5.0f);
        truckSpeed = Random.Range(0.25f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitTime && canSpawn == true)
        {
            decider = Random.Range(-7, 7);
            if (decider < -3)
            {
                SpawnTruck();
                canSpawn = false;
            }
            timer = timer - waitTime;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Truck"))
        {
            canSpawn = true;
        }
    }

    void SpawnTruck()
    {
        TruckController newTruck = Instantiate(truck, transform.position, transform.rotation) as TruckController;
        newTruck.transform.parent = transform.parent;
        newTruck.speed = truckSpeed;
    }
}

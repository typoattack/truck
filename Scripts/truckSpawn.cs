using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class truckSpawn : MonoBehaviour {

    public TruckController truck;
    public TruckController coin;
    public bool canSpawn = false;
    private float waitTime;
    private float timer = 0.0f;
    private int decider;
    private float truckSpeed;
    bool wasLastObjectCoin = false;
    public bool isParentGround = false;
    bool canTractorSpawn = true;
	private float minTruckSpeed;
    bool coinSpawned = false;

    // Use this for initialization
    void Start ()
    {
		waitTime = Random.Range(2.0f, 5.0f);
		minTruckSpeed = Random.Range(.25f, .5f);
		truckSpeed = Random.Range(minTruckSpeed, minTruckSpeed*2);
        if (transform.parent.CompareTag("Ground")) isParentGround = true;
        else SpawnTruck();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(isParentGround == false)
        {
            if (timer > waitTime && canSpawn)
            {
                decider = Random.Range(-7, 7);
                if (decider <= -3 || wasLastObjectCoin)
				{
					truckSpeed = Random.Range(minTruckSpeed, minTruckSpeed*2);
                    SpawnTruck();
                    canSpawn = false;
                }
                else if (!coinSpawned && decider > 5)
                {
                    SpawnCoin();
                    canSpawn = false;
                    coinSpawned = true;
                }
                timer = 0;
            }
        }
        else
        {
            if (transform.parent.transform.position.z == 0.0f && canTractorSpawn)
            {
                truckSpeed = 0.2f;
                SpawnTruck();
                canTractorSpawn = false;
            }
        }
        if (transform.position.z <= -2.0f) Destroy(gameObject);
    }
    
    void OnTriggerExit(Collider other)
	{
        if (other.gameObject.CompareTag("crumpleZone") || other.gameObject.CompareTag("Coin"))
        {
            canSpawn = true;
        }
    }

    void SpawnTruck()
    {
        TruckController newTruck = Instantiate(truck, transform.position, transform.rotation) as TruckController;
        newTruck.gameObject.SetActive(true);
        newTruck.transform.parent = transform.parent;
        newTruck.speed = truckSpeed;
        wasLastObjectCoin = false;
    }

    void SpawnCoin()
    {
        TruckController newCoin = Instantiate(coin, transform.position, transform.rotation) as TruckController;
        newCoin.gameObject.SetActive(true);
        newCoin.transform.parent = transform.parent;
        newCoin.speed = truckSpeed;
        wasLastObjectCoin = true;
    }
}

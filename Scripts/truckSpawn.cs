﻿using System.Collections;
using UnityEngine;

public class truckSpawn : MonoBehaviour {

    public TruckController truck;
    public CoinController coin;
    public bool canSpawn = false;
    private float waitTime;
    private float timer = 0.0f;
    private int decider;
    private float truckSpeed;
    //bool wasLastObjectCoin = false;
    bool isParentGround = false;
    bool canTractorSpawn = true;
	private float minTruckSpeed;
    private float tractorSpeed;
    PlayerController mc;
    private int coinXPosition = 0;
    public int truckSpawnThreshold;
    public int coinSpawnThreshold;

    // Use this for initialization
    void Start ()
    {
		waitTime = Random.Range(2.0f, 5.0f);
		minTruckSpeed = Random.Range(.25f, .5f);
		truckSpeed = Random.Range(minTruckSpeed, minTruckSpeed*2);
        mc = GameObject.Find("MC").GetComponent<PlayerController>();
        tractorSpeed = mc.tractorSpeed;
        decider = Random.Range(0, 19);
        coinSpawnThreshold = Mathf.Min(1 + PlayerController.score / 20, 7);
        if (transform.parent.CompareTag("Ground")) isParentGround = true;
        else if (decider <= coinSpawnThreshold)
        {
            SpawnCoin();
            canSpawn = true;
        }
        else SpawnTruck();
    }

    // Update is called once per frame
    void Update()
    {
        truckSpawnThreshold = Mathf.Min(-7 + PlayerController.score / 20, -3);
        tractorSpeed = mc.tractorSpeed;
        timer += Time.deltaTime;
        if(isParentGround == false) // road spawns trucks
        {
            if (timer > waitTime && canSpawn)
            {
                decider = Random.Range(-7, 7);
                if (decider <= truckSpawnThreshold) //|| wasLastObjectCoin)
				{
					truckSpeed = Random.Range(minTruckSpeed, minTruckSpeed*2);
                    SpawnTruck();
                    canSpawn = false;
                }
                timer = 0;
            }
        }
        else if (transform.parent.transform.position.z == 0.0f && canTractorSpawn) // safe zone spawns tractor
        {
            truckSpeed = tractorSpeed;
            SpawnTruck();
            canTractorSpawn = false;
        }
        if (transform.position.z <= -2.0f) Destroy(gameObject);
    }
    
    void OnTriggerExit(Collider other)
	{
        if (other.gameObject.CompareTag("crumpleZone")) //|| other.gameObject.CompareTag("Coin"))
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
        //wasLastObjectCoin = false;
    }

    void SpawnCoin()
    {
        Vector3 coinPosition = new Vector3(Random.Range(-3, 3), transform.position.y, transform.position.z);
        CoinController newCoin = Instantiate(coin, coinPosition, transform.rotation) as CoinController;
        newCoin.gameObject.SetActive(true);
        newCoin.transform.parent = transform.parent;
        newCoin.speed = 0f;
        //wasLastObjectCoin = true;
    }

    IEnumerator StopSpawnTemporarily(float duration)
    {
        canSpawn = false;
        yield return new WaitForSeconds(duration);
        canSpawn = true;
    }
}

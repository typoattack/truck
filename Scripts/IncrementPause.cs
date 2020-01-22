using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementPause : MonoBehaviour {

    RoadSpawn roadSpawn;
    
    public bool doubleJump;
    public int startingZPosition;
    private int lastRoadPosition;
    PlayerController mc;

    // Use this for initialization
    void Start()
    {
        roadSpawn = GameObject.Find("roadSpawn").GetComponent<RoadSpawn>();
        
        mc = GameObject.Find("MC").GetComponent<PlayerController>();
        doubleJump = mc.doubleJump;
        startingZPosition = (int)transform.position.z;
        lastRoadPosition = roadSpawn.lastRoad;
    }

    // Update is called once per frame
    void Update()
    {
        doubleJump = mc.doubleJump;
        
        if (transform.position.z <= -2.5f)
        {
            if (doubleJump)
            {
                if (startingZPosition % 2 == 0)
                {
                    roadSpawn.gameObject.SendMessage("SpawnGround", lastRoadPosition - 1);
                    roadSpawn.gameObject.SendMessage("SpawnGround", lastRoadPosition);
                }
            }
            else roadSpawn.gameObject.SendMessage("SpawnGround", lastRoadPosition);
            Destroy(gameObject);
        }

    }
}

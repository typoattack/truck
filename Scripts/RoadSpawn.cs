using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawn : MonoBehaviour {

    public RoadController roadLR;
    public RoadController roadRL;
    public RoadController ground;
    private int groundDecider;
    private int rotationDecider;
    private int roadCount = 10;
    private bool isPlayerGrounded;

    // Use this for initialization
    void Start ()
    {

        SpawnGround(0);
        
		for (int i = -2; i <= 35; i++)
        {
            if (i== 0) continue;
            SpawnGround(i);
        }
        
	}
	
	// Update is called once per frame
	void Update () {
    }
    
    void SpawnGround(int i)
    {
        groundDecider = Random.Range(0, 10);
        rotationDecider = Random.Range(-7, 7);

        Vector3 pos = new Vector3(transform.position.x, transform.position.y, i);

        if (groundDecider <= 2 || roadCount > 2)
        {
            Instantiate(ground, pos, transform.rotation);
            roadCount = 0;
        }
        else
        {
            Quaternion newRotation = transform.rotation;
            //if (rotationDecider <= 0) newRotation *= Quaternion.Euler(0, 180f, 0);
            if (rotationDecider <= 0) Instantiate(roadLR, pos, transform.rotation);
            else
            {
                newRotation *= Quaternion.Euler(0, 180f, 0);
                Instantiate(roadRL, pos, newRotation);
            }
            roadCount++;
        }
    }
}

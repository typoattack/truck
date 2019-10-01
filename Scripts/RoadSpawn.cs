using UnityEngine;

public class RoadSpawn : MonoBehaviour {

    public RoadController roadLR;
    public RoadController roadRL;
    public RoadController ground;
    private int groundDecider;
    private int rotationDecider;
    private int roadCount = 10;
    int spawnMode = 0; // 0 for random, 1 for 2+2 highways, 2 for 3+3 highways
    int highwayCounter = 0;

    // Use this for initialization
    void Start ()
    {

        SpawnGround(0);
        
		for (int i = -2; i <= 20; i++)
        {
            if (i== 0) continue;
            SpawnGround(i);
        }
        
	}
    
    void SpawnGround(int i)
    {
        groundDecider = Random.Range(0, 10);
        rotationDecider = Random.Range(-7, 7);

        Vector3 pos = new Vector3(transform.position.x, transform.position.y, i);

        if (groundDecider >= 9 && spawnMode == 0) spawnMode = 1;
        else if (groundDecider >= 8 && spawnMode == 0) spawnMode = 2;
        if (spawnMode == 1 && roadCount == 0)
        {
            if (highwayCounter < 2) Instantiate(roadLR, pos, transform.rotation);
            else Instantiate(roadRL, pos, transform.rotation * Quaternion.Euler(0, 180f, 0));
            
            highwayCounter++;
            if (highwayCounter == 4)
            {
                spawnMode = 0;
                highwayCounter = 0;
                roadCount = 4;
            }
        }
        else if (spawnMode == 2 && roadCount == 0)
        {
            if (highwayCounter < 3) Instantiate(roadLR, pos, transform.rotation);
            else Instantiate(roadRL, pos, transform.rotation * Quaternion.Euler(0, 180f, 0));

            highwayCounter++;
            if (highwayCounter == 6)
            {
                spawnMode = 0;
                highwayCounter = 0;
                roadCount = 6;
            }
        }
        else if (groundDecider <= 2 || roadCount > 2)
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

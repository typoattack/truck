using UnityEngine;

public class RoadController : MonoBehaviour
{
    RoadSpawn roadSpawn;
    private float speed;

    private bool isPlayerMovingForward;
    private Vector3 destination;
    private float distanceToMove;
    public bool doubleJump;
    public int startingZPosition;
    private int lastRoadPosition;
    PlayerController mc;

    // Use this for initialization
    void Start()
    {
        roadSpawn = GameObject.Find("roadSpawn").GetComponent<RoadSpawn>();

        destination = transform.position;
        mc = GameObject.Find("MC").GetComponent<PlayerController>();
        isPlayerMovingForward = mc.forwardMotion;
        speed = mc.speed;
        distanceToMove = mc.distanceToMove;
        //doubleJump = (PlayerPrefs.GetInt("Ability") == 1);
        doubleJump = mc.doubleJump;
        startingZPosition = (int) transform.position.z;
        lastRoadPosition = roadSpawn.lastRoad;
    }

    // Update is called once per frame
    void Update()
    {

        isPlayerMovingForward = mc.forwardMotion;
        speed = mc.speed;
        distanceToMove = mc.distanceToMove;
        //doubleJump = (PlayerPrefs.GetInt("Ability") == 1);
        doubleJump = mc.doubleJump;


        if (isPlayerMovingForward)
        {
            destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - distanceToMove);
        }

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination, step);

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

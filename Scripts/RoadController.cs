using UnityEngine;

public class RoadController : MonoBehaviour
{
    Transform roadSpawn;
    private float speed;

    private bool isPlayerMovingForward;
    private Vector3 destination;
    private float distanceToMove;
    public bool doubleJump;
    public int startingZPosition;
    PlayerController mc;

    // Use this for initialization
    void Start()
    {
        roadSpawn = GameObject.Find("roadSpawn").transform;

        destination = transform.position;
        mc = GameObject.Find("MC").GetComponent<PlayerController>();
        isPlayerMovingForward = mc.forwardMotion;
        speed = mc.speed;
        distanceToMove = mc.distanceToMove;
        doubleJump = mc.jumpTwoSpaces;
        startingZPosition = (int) transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {

        isPlayerMovingForward = mc.forwardMotion;
        speed = mc.speed;
        distanceToMove = mc.distanceToMove;
        doubleJump = mc.jumpTwoSpaces;

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
                    roadSpawn.gameObject.SendMessage("SpawnGround", 19);
                    roadSpawn.gameObject.SendMessage("SpawnGround", 20);
                }
            }
            else roadSpawn.gameObject.SendMessage("SpawnGround", 20);
            Destroy(gameObject);
        }

    }

}

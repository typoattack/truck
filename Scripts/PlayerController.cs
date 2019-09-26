using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    //RigidBody
    private Rigidbody rb;

    //Jump flags and variables
    private bool jump = false;
    public LayerMask Ground;
    [HideInInspector] public static bool isGrounded = true;
    private Vector3 dir = Vector3.down;
    private float groundedDistance;

    //Score
    [HideInInspector] public static int score = 0;
    [HideInInspector] public static int coins = 0;

    //Movement flags and variables
    [HideInInspector] public static bool forwardMotion = false;
    private bool canMoveLeft = false;
    private bool canMoveRight = false;
    private Vector3 destination;
    [HideInInspector] public static float speed;
    private float jumpForce;
    [HideInInspector] public static float distanceToMove;

    //Abilities and powerups
    public bool jumpTwoSpaces = false; // jump forward two spaces--cheerleader or Mario outfit

    public bool fastMovement = false; // faster movement--track and field outfit

    public bool invisibility = false; // player disappears, cannot be hit by trucks for certain time--ninja outfit
    private bool isInvisible = false;

    public bool canDestroyTruck = false; // player can destroy trucks--delinquent outfit
    [HideInInspector] public static float tractorSpeed;

    public bool endurance = false; // player can take one truck hit--zombie outfit
    private bool canActivateEndurance = true;
    private int HP;

    public bool timeFreeze = false; // player can stop all trucks for certain time--Jojos tribute outfit

    public bool destroyAllTrucks = false; // player can destroy all trucks on screen--magical girl outfit

    public int counter;

    //Audio
    private AudioSource audio;
    public AudioClip jumpSound;
    public AudioClip hitByTruckSound;
    public AudioClip collectCoinSound;
    public AudioClip truckDestroyed;
    public AudioClip abilityReady;
    public AudioClip abilityUsed;
    private bool canPlayAudio;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        score = 0;
		if (!PlayerPrefs.HasKey ("TotalCoins")) {
			PlayerPrefs.SetInt ("TotalCoins", 0);
		} else {
			coins = PlayerPrefs.GetInt ("TotalCoins");
		}
        Time.timeScale = 5.0f;

        jumpTwoSpaces = fastMovement = invisibility = canDestroyTruck = endurance = timeFreeze = destroyAllTrucks = false;
        groundedDistance = 0.5f;
        speed = 2.5f;
        jumpForce = 7.0f;
        distanceToMove = 1.0f;
        tractorSpeed = 0.2f;
        counter = 0;
        HP = 2;

    }
	
	// Update is called once per frame
	void Update ()
    {
        //////////////// For debug purpose /////////////////////
        if (Input.GetKeyDown("r")) PlayerPrefs.SetInt("TotalCoins", 0);

        if (Input.GetKeyDown("1"))
        {
            jumpTwoSpaces = true;
            fastMovement = invisibility = canDestroyTruck = endurance = timeFreeze = destroyAllTrucks = false;
            speed = 2.0f;
            jumpForce = 10.0f;
            distanceToMove = 2.0f;            
        }

        if (Input.GetKeyDown("2"))
        {
            fastMovement = true;
            jumpTwoSpaces = invisibility = canDestroyTruck = endurance = timeFreeze = destroyAllTrucks = false;
            speed = 5.0f;
            jumpForce = 3.5f;
            distanceToMove = 1.0f;            
        }
        
        if (Input.GetKeyDown("3"))
        {
            invisibility = true;
            jumpTwoSpaces = fastMovement = canDestroyTruck = endurance = timeFreeze = destroyAllTrucks = false;
            if (counter <= 0)
            {
                audio.PlayOneShot(abilityUsed, 1.0f);
                StartCoroutine(MakeInvisible(10.0f));
            }
        }

        if (Input.GetKeyDown("4"))
        {
            canDestroyTruck = true;
            jumpTwoSpaces = fastMovement = invisibility = endurance = timeFreeze = destroyAllTrucks = false;
        }

        if (Input.GetKeyDown("5") && canActivateEndurance)
        {
            endurance = true;
            jumpTwoSpaces = fastMovement = invisibility = canDestroyTruck = timeFreeze = destroyAllTrucks = false;
            canActivateEndurance = false;            
        }
        
        if (Input.GetKeyDown("6"))
        {
            timeFreeze = true;
            jumpTwoSpaces = fastMovement = invisibility = canDestroyTruck = endurance = destroyAllTrucks = false;
            if (counter <= 0)
            {
                audio.PlayOneShot(abilityUsed, 1.0f);
                affectTruckTemporarily();
            }
        }
        
        if (Input.GetKeyDown("7"))
        {
            destroyAllTrucks = true;
            jumpTwoSpaces = fastMovement = invisibility = canDestroyTruck = endurance = timeFreeze = false;
            if (counter <= 0)
            {
                Debug.Log("activate");
                audio.PlayOneShot(abilityUsed, 1.0f);
                affectTruckTemporarily();
            } 
        }
        
        ////////////////    Debug end      /////////////////////

        isGrounded = Physics.Raycast(transform.position, dir, groundedDistance, Ground);
        if (transform.position.x <= -0.5f) canMoveLeft = false;
        else canMoveLeft = true;
        if (transform.position.x >= 0.5f) canMoveRight = false;
        else canMoveRight = true;

        if (Input.GetKeyDown("w") && isGrounded)
        {
            jump = true;
            forwardMotion = true;
        }

        if (Input.GetKeyDown("a") && canMoveLeft && isGrounded)
        {
            jump = true;
            destination = new Vector3(transform.position.x - 1.0f, transform.position.y, transform.position.z);
        }

        if (Input.GetKeyDown("d") && canMoveRight && isGrounded)
        {
            jump = true;
            destination = new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z);
        }

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination, step);
        if (counter <= 0  && (invisibility || timeFreeze || destroyAllTrucks) && canPlayAudio)
        {
            audio.PlayOneShot(abilityReady, 3.0f);
            canPlayAudio = false;
        }
    }

    void FixedUpdate()
    {
        if (jump)
        {
			rb.velocity = new Vector3 (0f, 0f, 0f);
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            audio.PlayOneShot(jumpSound, 1.0f);
            jump = false;
        }

        if (forwardMotion) forwardMotion = false;
    }
    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Truck") && !isInvisible)
        {
            if (endurance)
            {
                audio.PlayOneShot(truckDestroyed, 1.0f);
                Destroy(other.gameObject);
                if (HP > 0) HP--;
                else endurance = false;
                return;
            }
            //Time.timeScale = 1.0f;
            audio.PlayOneShot(hitByTruckSound, 1.0f);
            StartCoroutine(DelayTime(0.3f));
            Time.timeScale = 0.2f;
            rb.AddForce(other.gameObject.GetComponent<Rigidbody>().velocity *20.0f, ForceMode.Impulse);
            rb.AddForce(new Vector3(0f, 10f, 0f), ForceMode.Impulse);
            //SceneManager.LoadScene("Score", LoadSceneMode.Single);
        }

        if(other.gameObject.CompareTag("Score"))
        {
            score++;
            if (counter > 0) counter--;
        }

        if(other.gameObject.CompareTag("Coin"))
        {
            audio.PlayOneShot(collectCoinSound, 1.0f);
			coins++;
			PlayerPrefs.SetInt ("TotalCoins", coins);
            Destroy(other.gameObject);
        }
    }
    */

    public void Die(Vector3 truckVelocity, Collider other)
    {
        if (!isInvisible)
        {
            if (endurance)
            {
                audio.PlayOneShot(truckDestroyed, 1.0f);
                Destroy(other.gameObject);
                if (HP > 0) HP--;
                else endurance = false;
                return;
            }
            //Time.timeScale = 1.0f;
            audio.PlayOneShot(hitByTruckSound, 1.0f);
            StartCoroutine(DelayTime(0.3f));
            Time.timeScale = 0.2f;
            rb.AddForce(/*other.gameObject.GetComponent<Rigidbody>().velocity*/ truckVelocity * 20.0f, ForceMode.Impulse);
            rb.AddForce(new Vector3(0f, 10f, 0f), ForceMode.Impulse);
            //SceneManager.LoadScene("Score", LoadSceneMode.Single);
        }
    }

    public void AddScore()
    {
        score++;
        if (counter > 0) counter--;
    }

    public void AddCoin(Collider other)
    {
        audio.PlayOneShot(collectCoinSound, 1.0f);
        coins++;
        PlayerPrefs.SetInt("TotalCoins", coins);
        Destroy(other.gameObject);
    }

    public void Punch(Collider other)
    {
        if (canDestroyTruck && Input.GetKeyDown("p"))
        {
            Destroy(other.gameObject);
            audio.PlayOneShot(truckDestroyed, 1.0f);
            tractorSpeed += 0.2f;
        }
    }

    IEnumerator DelayTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        Time.timeScale = 1f;
        //gameObject.SetActive(false);
        SceneManager.LoadScene("Score", LoadSceneMode.Single);
    }
    
    IEnumerator MakeInvisible(float duration)
    {
        isInvisible = true;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(duration);
        isInvisible = false;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        counter = 10;
        canPlayAudio = true;
    }

    private void affectTruckTemporarily()
    {
        GameObject[] allTrucks;
        allTrucks = GameObject.FindGameObjectsWithTag("Truck");
        GameObject[] allTruckSpawns;
        allTruckSpawns = GameObject.FindGameObjectsWithTag("TruckSpawn");
        if (destroyAllTrucks)
        {
            for (int i = 0; i < allTrucks.Length; i++)
            {
                float positionZ = transform.position.z;
                float diffZ = allTrucks[i].transform.position.z - positionZ;
                if (diffZ <= 10.0f) Destroy(allTrucks[i]);
            }
            audio.PlayOneShot(truckDestroyed, 5.0f);
            counter = 30;
        }
        if (timeFreeze)
        {
            for (int i = 0; i < allTrucks.Length; i++)
            {
                allTrucks[i].SendMessage("StopTruckTemporarily", 10.0f);
            }
            for (int i = 0; i < allTruckSpawns.Length; i++)
            {
                allTruckSpawns[i].SendMessage("StopSpawnTemporarily", 10.0f);
            }
            counter = 20;
        }
        canPlayAudio = true;
    }
}

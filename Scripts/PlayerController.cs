using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    //RigidBody
    private Rigidbody rb;

    //Jump flags and variables
    public LayerMask Ground;
    private bool jump = false;
    bool isGrounded = true;
    private Vector3 dir = Vector3.down;
    private float groundedDistance;

    //Score
    [HideInInspector] public static int score = 0, coins = 0, totalScore = 0;

    //Movement flags and variables
    [HideInInspector] public bool forwardMotion = false;
    [HideInInspector] public float speed, distanceToMove;
    private bool canMoveLeft = false, canMoveRight = false;
    private Vector3 destination;
    private float jumpForce;

    //Abilities and powerups
    public int skin; // themed skin
    public int gender; // male or female skin
    public int ability; // Public for debug purposes
    // 1: jump forward two spaces--cheerleader or Mario outfit
    // 2: faster movement--track and field outfit
    // 3: player disappears, cannot be hit by trucks for certain time--ninja outfit
    // 4: player can destroy trucks--delinquent outfit
    // 5: player can take one truck hit--zombie outfit
    // 6: player can stop all trucks for certain time--Jojos tribute outfit
    // 7: player can destroy all trucks on screen--magical girl outfit

    public int maxNumberOfSkins = 8;

    private bool isInvisible = false;

    [HideInInspector] public float tractorSpeed;

    private int HP;

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

    //Other
    private bool canPause;
    public GameObject powerUpButton;
    public GameObject punchButton;
    public bool isIsekaid;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        score = 0;
        if (!PlayerPrefs.HasKey("TotalCoins"))
        {
            PlayerPrefs.SetInt("TotalCoins", 0);
        }
        else
        {
            coins = PlayerPrefs.GetInt("TotalCoins");
        }
        if (!PlayerPrefs.HasKey("TotalScore"))
        {
            PlayerPrefs.SetInt("TotalScore", 0);
        }
        else
        {
            totalScore = PlayerPrefs.GetInt("TotalScore");
        }
        Time.timeScale = 5.0f;
        canPause = true;
        isIsekaid = false;

        groundedDistance = 0.5f;
        speed = 2.5f;
        jumpForce = 7.0f;
        distanceToMove = 1.0f;
        tractorSpeed = 0.2f;
        counter = 0;
        HP = 2;

        skin = PlayerPrefs.GetInt("Skin");
        ability = PlayerPrefs.GetInt("Ability");
        gender = PlayerPrefs.GetInt("Gender");
        if (ability == 1)
        {
            speed = 2.0f;
            jumpForce = 10.0f;
            distanceToMove = 2.0f;
        }

        else if (ability == 2)
        {
            speed = 5.0f;
            jumpForce = 3.5f;
            distanceToMove = 1.0f;
        }

        else if (counter <= 0 && (ability == 3 || ability == 6 || ability == 7))
        {
            powerUpButton.SetActive(true);
        }

        else if (ability == 4)
        {
            punchButton.SetActive(true);
        }

        for (int i = 0; i < maxNumberOfSkins; i++)
        {
            if (i == skin) gameObject.transform.GetChild(2).GetChild(gender).GetChild(i).gameObject.SetActive(true);
            else gameObject.transform.GetChild(2).GetChild(gender).GetChild(i).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //////////////// For debug purpose /////////////////////
        if (Input.GetKeyDown("r"))
        {
            PlayerPrefs.SetInt("TotalCoins", 0);
            PlayerPrefs.SetInt("TotalScore", 0);
            score = coins = totalScore = 0;
        }

        ////////////////    Debug end      /////////////////////

        if (Input.GetKeyDown("e"))
        {
            if (ability == 3)
            {
                if (counter <= 0)
                {
                    audio.PlayOneShot(abilityUsed, 1.0f);
                    StartCoroutine(MakeInvisible(10.0f));
                }
            }
            else if (ability == 6)
            {
                if (counter <= 0)
                {
                    audio.PlayOneShot(abilityUsed, 1.0f);
                    affectTruckTemporarily();
                }
            }
            else if (ability == 7 && counter <= 0)
            {
                audio.PlayOneShot(abilityUsed, 1.0f);
                affectTruckTemporarily();
            }
        }

        isGrounded = Physics.Raycast(transform.position, dir, groundedDistance, Ground);
        if (transform.position.x <= -2.5f) canMoveLeft = false;
        else canMoveLeft = true;
        if (transform.position.x >= 2.5f) canMoveRight = false;
        else canMoveRight = true;

        if (Input.GetKeyDown("w"))
        {
            //jump = true;
            //forwardMotion = true;
            jumpForward();
        }

        if (Input.GetKeyDown("a"))
        {
            //jump = true;
            //destination = new Vector3(transform.position.x - 1.0f, transform.position.y, transform.position.z);
            jumpLeft();
        }

        if (Input.GetKeyDown("d"))
        {
            //jump = true;
            //destination = new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z);
            jumpRight();
        }

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination, step);
        if (counter <= 0 && (ability == 3 || ability == 6 || ability == 7) && canPlayAudio)
        {
            audio.PlayOneShot(abilityReady, 3.0f);
            canPlayAudio = false;
            powerUpButton.SetActive(true);
        }

    }

    void FixedUpdate()
    {
        if (jump)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
            audio.PlayOneShot(jumpSound, 1.0f);
            jump = false;
        }

        if (forwardMotion) forwardMotion = false;
    }
   
    public void jumpForward()
    {
        if (isGrounded)
        {
            jump = true;
            forwardMotion = true;
        }
    }

    public void jumpRight()
    {
        if (canMoveRight && isGrounded)
        {
            jump = true;
            destination = new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z);
        }
    }

    public void jumpLeft()
    {
        if (canMoveLeft && isGrounded)
        {
            jump = true;
            destination = new Vector3(transform.position.x - 1.0f, transform.position.y, transform.position.z);
        }
    }

    public void ActivatePowerup()
    {
        if (ability == 3)
        {
            if (counter <= 0)
            {
                audio.PlayOneShot(abilityUsed, 1.0f);
                StartCoroutine(MakeInvisible(10.0f));
            }
        }
        else if (ability == 6)
        {
            if (counter <= 0)
            {
                audio.PlayOneShot(abilityUsed, 1.0f);
                affectTruckTemporarily();
            }
        }
        else if (ability == 7 && counter <= 0)
        {
            audio.PlayOneShot(abilityUsed, 1.0f);
            affectTruckTemporarily();
        }
        powerUpButton.SetActive(false);
    }

    public void Die(Vector3 truckVelocity, Collider other)
    {
        if (!isInvisible)
        {
            if (ability == 5)
            {
                audio.PlayOneShot(truckDestroyed, 1.0f);
                Destroy(other.gameObject);
                if (HP > 0) HP--;
                else ability = 0;
                return;
            }
            //Time.timeScale = 1.0f;
            canPause = false;
            isIsekaid = true;
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
        totalScore++;
        PlayerPrefs.SetInt("TotalScore", totalScore);
        if (counter > 0) counter--;
    }

    public void AddCoin(Collider other)
    {
        audio.PlayOneShot(collectCoinSound, 1.0f);
        coins++;
        PlayerPrefs.SetInt("TotalCoins", coins);
        Destroy(other.gameObject);
    }

    public void Pause()
    {
        if (canPause) Time.timeScale = 0f;
    }

    public void Unpause()
    {
        Time.timeScale = 5.0f;
    }

    public void Punch(Collider other)
    {
        if (ability == 4)
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

        GameObject[] allTractors;
        allTractors = GameObject.FindGameObjectsWithTag("Tractor");

        GameObject[] allTruckSpawns;
        allTruckSpawns = GameObject.FindGameObjectsWithTag("TruckSpawn");

        if (ability == 7)
        {
            for (int i = 0; i < allTrucks.Length; i++)
            {
                float positionZ = transform.position.z;
                float diffZ = allTrucks[i].transform.position.z - positionZ;
                if (diffZ <= 10.0f) Destroy(allTrucks[i]);
            }

            for (int i = 0; i < allTractors.Length; i++)
            {
                float positionZ = transform.position.z;
                float diffZ = allTractors[i].transform.position.z - positionZ;
                if (diffZ <= 10.0f) Destroy(allTractors[i]);
            }

            for (int i = 0; i < allTruckSpawns.Length; i++)
            {
                allTruckSpawns[i].SendMessage("StopSpawnTemporarily", 10.0f);
            }
            audio.PlayOneShot(truckDestroyed, 5.0f);
            counter = 30;
        }
        else if (ability == 6)
        {
            for (int i = 0; i < allTrucks.Length; i++)
            {
                allTrucks[i].SendMessage("StopTruckTemporarily", 10.0f);
            }

            for (int i = 0; i < allTractors.Length; i++)
            {
                allTractors[i].SendMessage("StopTruckTemporarily", 10.0f);
            }

            for (int i = 0; i < allTruckSpawns.Length; i++)
            {
                allTruckSpawns[i].SendMessage("StopSpawnTemporarily", 20.0f);
            }
            counter = 20;
        }
        canPlayAudio = true;
    }
}

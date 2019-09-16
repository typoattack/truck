using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;

    private bool jump = false;
    [HideInInspector] public static bool forwardMotion = false;
    private bool canMoveLeft = false;
    private bool canMoveRight = false;
    public LayerMask Ground;
    [HideInInspector] public static bool isGrounded = true;
    private Vector3 dir = Vector3.down;
    private float distance = 0.5f;

    [HideInInspector] public static int score = 0;
    [HideInInspector] public static int coins = 0;

    private Vector3 destination;
    private float speed = 2.5f;

    private AudioSource audio;
    public AudioClip jumpSound;
    public AudioClip hitByTruckSound;
    public AudioClip collectCoinSound;

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
    }
	
	// Update is called once per frame
	void Update ()
    {
        isGrounded = Physics.Raycast(transform.position, dir, distance, Ground);
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
    }

    void FixedUpdate()
    {
        if (jump)
        {
			rb.velocity = new Vector3 (0f, 0f, 0f);
            rb.AddForce(new Vector3(0f, 7f, 0f), ForceMode.Impulse);
            audio.PlayOneShot(jumpSound, 1.0f);
            jump = false;
        }

        if (forwardMotion) forwardMotion = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Truck"))
        {
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
        }

        if(other.gameObject.CompareTag("Coin"))
        {
            audio.PlayOneShot(collectCoinSound, 1.0f);
			coins++;
			PlayerPrefs.SetInt ("TotalCoins", coins);
            Destroy(other.gameObject);
        }
    }

    IEnumerator DelayTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        Time.timeScale = 1f;
        //gameObject.SetActive(false);
        SceneManager.LoadScene("Score", LoadSceneMode.Single);
    }

}

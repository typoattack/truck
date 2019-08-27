using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    [HideInInspector] public static bool jump = false;
    public LayerMask Ground;
    [HideInInspector] public static bool isGrounded = true;
    private Vector3 dir = Vector3.down;
    private float distance = 0.5f;
    //private Scene currentscene;
    [HideInInspector] public static int score = 0;
    [HideInInspector] public static int coins = 0;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        //currentscene = SceneManager.GetActiveScene();
        score = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        isGrounded = Physics.Raycast(transform.position, dir, distance, Ground);

        if (Input.GetKeyDown("w") && isGrounded)
        {
            jump = true;
        }
    }

    void FixedUpdate()
    {
        if (jump)
        {
			rb.velocity = new Vector3 (0f, 0f, 0f);
            rb.AddForce(new Vector3(0f, 5.555f, 0f), ForceMode.Impulse);
            jump = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Truck"))
        {
            //Destroy(gameObject);
            SceneManager.LoadScene("Score", LoadSceneMode.Single);
        }

        if(other.gameObject.CompareTag("Score"))
        {
            score++;
        }

        if(other.gameObject.CompareTag("Coin"))
        {
            coins++;
            Destroy(other.gameObject);
        }
    }

}

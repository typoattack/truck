using UnityEngine;

public class CoinController : MonoBehaviour
{

    public float speed = 1.0f;
    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.velocity = transform.right * speed;
        transform.Rotate(new Vector3(90, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
        /*
        if (rb.transform.position.x > 2)
        {
            rb.velocity = new Vector3(speed * -1, 0f, 0f);
        }
        else if (rb.transform.position.x < -2) rb.velocity = new Vector3(speed, 0f, 0f);
        */

        if (transform.position.z <= -2.0f) Destroy(gameObject);
    }
}

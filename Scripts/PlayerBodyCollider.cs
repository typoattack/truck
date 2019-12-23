using UnityEngine;

public class PlayerBodyCollider : MonoBehaviour {

    PlayerController parent;
    private bool isIsekaid;

	// Use this for initialization
	void Start () {
        parent = transform.GetComponentInParent<PlayerController>();
        isIsekaid = parent.isIsekaid;
	}

    void Update()
    {
        isIsekaid = parent.isIsekaid;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Truck") || other.gameObject.CompareTag("Tractor"))
        {
            parent.Die(other.gameObject.GetComponent<Rigidbody>().velocity, other);
        }
        /*
        if (other.gameObject.CompareTag("Score"))
        {
            parent.AddScore();
        }
        */
        if (other.gameObject.CompareTag("Coin") && !isIsekaid)
        {
            parent.AddCoin(other);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Score") && !isIsekaid)
        {
            parent.AddScore();
        }
    }
}

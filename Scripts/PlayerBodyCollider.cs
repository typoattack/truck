using UnityEngine;

public class PlayerBodyCollider : MonoBehaviour {

    PlayerController parent;

	// Use this for initialization
	void Start () {
        parent = transform.GetComponentInParent<PlayerController>();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Truck"))
        {
            parent.Die(other.gameObject.GetComponent<Rigidbody>().velocity, other);
        }
        if (other.gameObject.CompareTag("Score"))
        {
            parent.AddScore();
        }
        if (other.gameObject.CompareTag("Coin"))
        {
            parent.AddCoin(other);
        }
    }
}

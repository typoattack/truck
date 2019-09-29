using UnityEngine;

public class truckDespawn : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Truck") || other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
    }
}

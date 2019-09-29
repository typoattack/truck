using UnityEngine;

public class Punch : MonoBehaviour
{
    PlayerController parent;

    // Use this for initialization
    void Start()
    {
        parent = transform.GetComponentInParent<PlayerController>();
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Truck"))
        {
            parent.Punch(other);
        }
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckBodySelector : MonoBehaviour {

    private int MaxNumberOfBodies = 3;
    public int TruckBodyDecider;

	// Use this for initialization
	void Start () {

        TruckBodyDecider = Random.Range(1, MaxNumberOfBodies + 1);

        for (int i = 1; i <= MaxNumberOfBodies; i++)
        {
            if (i == TruckBodyDecider) gameObject.transform.GetChild(i).gameObject.SetActive(true);
            else gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SackPickup : MonoBehaviour {
    public int value;

    public GameObject pickupEffect;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        //does the other object have the player tag attached to it?
        if (other.tag == "Player")
        {
            FindObjectOfType<GameManager>().AddWeapon(value);

            Instantiate(pickupEffect, transform.position, transform.rotation);

            Destroy(gameObject);




        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GoldPickup : MonoBehaviour {

    public int value;
   
    public GameObject pickupEffect;


   
    

    //public bool alreadyPlayed = false;

	// Use this for initialization
	void Start () {

      
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    //on trigger enter whenver another collider enters teh trigger attached to this script this section of code gets called
    private void OnTriggerEnter(Collider other)
    {
        //does the other object have the player tag attached to it?
        if(other.tag =="Player")
        {
            FindObjectOfType<GameManager>().AddGold(value);

            Instantiate(pickupEffect, transform.position, transform.rotation);
           
            Destroy(gameObject);

             
            
            
        }
                
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class EnemyHealthManager : MonoBehaviour {


    public int health;
    private int currentHealth;

    public Text Wintext;

    

    public Image blackScreen;

    public float winSpeed;

  
    // Use this for initialization
    void Start () {
        currentHealth = health;

        Wintext.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
        if(currentHealth <= 0)
        {
            FindObjectOfType<HealthManager>().isFadeToWin = true;  
            
            Destroy(gameObject);
            
             
                
        }
       


     





    }

    public void HurtEnemy(int damage)
    {
        currentHealth -= damage; 
    }
}


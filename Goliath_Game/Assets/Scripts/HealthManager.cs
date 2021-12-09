using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthManager : MonoBehaviour {

    public int currentHealth; 

    public int maxHealth;

    public David thePlayer;

    public float invincibilityLength;
    private float invincibilityCounter;

    public Renderer davidRenderer;

    private float flashCounter;
    public float flashLength = 0.1f;

    private bool isRespawning;
    private Vector3 respawnPoint;

    public float respawnLength;

    public GameObject deathEffect;

    public Image blackScreen;
    private bool isFadeToBlack;
    private bool isFadeFromBlack;
    public float fadeSpeed;
    public float waitForFade;

    public Text Wintext;
    public Text deathText;

    public bool isFadeToWin;

    public float winSpeed;
    // Use this for initialization
    void Start () {
       
        deathText.enabled = false;
        Wintext.enabled = false;

        currentHealth = maxHealth;

        //thePlayer = FindObjectOfType<David>();

        respawnPoint = thePlayer.transform.position; 
	}

    // Update is called once per frame
    void Update()
    {

        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                davidRenderer.enabled = !davidRenderer.enabled;
                flashCounter = flashLength;
            }

            if (invincibilityCounter <= 0)
            {
                davidRenderer.enabled = true;
            }

        }

        if (isFadeToBlack)
        {
            deathText.enabled = true;
            deathText.color = new Color(deathText.color.r, deathText.color.g, deathText.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 1f)
            {

                isFadeToBlack = false;
            }
        }

        if (isFadeFromBlack)
        {
            deathText.color = new Color(deathText.color.r, deathText.color.g, deathText.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (blackScreen.color.a == 0f)
            {

                isFadeFromBlack = false;

            }
        }


        if (isFadeToWin)
        {
            Wintext.enabled = true;
            Wintext.color = new Color(Wintext.color.r, Wintext.color.g, Wintext.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, winSpeed * Time.deltaTime));

            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, winSpeed * Time.deltaTime));
        }
    }
    //if player is hurt
    public void HurtPlayer(int damage, Vector3 direction)
    {
        if (invincibilityCounter <= 0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Respawn();
            }
            else
            {


                thePlayer.Knockback(direction);

                invincibilityCounter = invincibilityLength;

                davidRenderer.enabled = false;

                flashCounter = flashLength;
            }
        }
    }


    public void Respawn()
    {
        //thePlayer.transform.position = respawnPoint;
        //currentHealth = maxHealth; 
        if (!isRespawning)
        {
            StartCoroutine("RespawnCo");
        }
    }

    public IEnumerator RespawnCo()
    {
        isRespawning = true;
//SetActive false can be used to take out Goliath maybe once he's killed
        thePlayer.gameObject.SetActive(false);

        Instantiate(deathEffect, thePlayer.transform.position, thePlayer.transform.rotation);

        yield return new WaitForSeconds(respawnLength);

            isFadeToBlack = true; 

        yield return new WaitForSeconds(waitForFade);

        isFadeToBlack = false;


        isFadeFromBlack = true; 


        isRespawning = false;

        thePlayer.gameObject.SetActive(true);

        thePlayer.transform.position = respawnPoint;
        currentHealth = maxHealth;

        invincibilityCounter = invincibilityLength;

        davidRenderer.enabled = false;

        flashCounter = flashLength;

    }

    public void HealPlayer(int healAmount)
    {
        currentHealth = +healAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }




}

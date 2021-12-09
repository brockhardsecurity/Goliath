using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

    public AudioClip SoundToPlay;
    public int currentGold;
    public Text keyText;
    public float volume;
    public bool alreadyPlayed = false;
    AudioSource audio;
    public int currentWeapon; 
    // Use this for initialization
    void Start () {
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        GameObject Gold = GameObject.Find("Gold");

       
        if (Gold == null)
        {
            audio.PlayOneShot(SoundToPlay, volume);
            alreadyPlayed = true;
        }



    }

    public void AddGold(int goldToAdd)
    {
        currentGold += goldToAdd;

        keyText.text = "Key Acquired";

    }

    public void AddWeapon(int weaponToAdd)
    {
        currentWeapon += weaponToAdd;
        keyText.text = "Sling Acquired";
    }
}


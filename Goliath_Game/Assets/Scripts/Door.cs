using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public AudioClip SoundToPlay;

    public float volume;
    public Animator anim;
    public bool alreadyPlayed = false;

    AudioSource audio;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

        audio = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        GameObject Sack = GameObject.Find("Sack");
        if (Sack == null)
        {
            anim.Play("DoorOpenCave");

        }
        
        GameObject Gold = GameObject.Find("Gold");
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject Gold = GameObject.Find("Gold");
        if (Gold == null)
        {
            anim.Play("DoorOpenRight");
            anim.Play("DoorOpenLeft");

            audio.PlayOneShot(SoundToPlay, volume);
            alreadyPlayed = true;

        }
         else if (Gold != null)
        {
            anim.Play("NewState");
        }  
            

         

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class David : MonoBehaviour {


    public float moveSpeed;
    public float jumpForce;
    public CharacterController controller;

    
    public float gravityScale; 

    private Vector3 moveDirection;

    public Animator anim;

    public Transform pivot;
    public float rotateSpeed;

    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;

    public GunController theGun;
    public GameObject playerModel;


    
	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (knockBackCounter <= 0)
        {
 // moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
            float yStore = moveDirection.y;
            moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
            moveDirection = moveDirection.normalized * moveSpeed;
            moveDirection.y = yStore;

            if (controller.isGrounded)
            {

                moveDirection.y = 0f;
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                }

            }
        } else
        {
            knockBackCounter -= Time.deltaTime;
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime); 
        controller.Move(moveDirection * Time.deltaTime);



        //Move the player in different directions based on camera look direction
        
        
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);

                //gradually rotate our player instead of snapping
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                //apply rotation to player
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }

            anim.SetBool("isGrounded", controller.isGrounded);

            anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
            if (Input.GetMouseButtonDown(0))
                theGun.isFiring = true;
            if (Input.GetMouseButtonUp(0))
                theGun.isFiring = false;
        
        
	}


    public void Knockback(Vector3 direction)
    {
        knockBackCounter = knockBackTime;
        //you could use this for the door for the Vector3
        //direction = new Vector3(1f, 1f, 1f);

        moveDirection = direction * knockBackForce;

        moveDirection.y = knockBackForce; 

    }




}

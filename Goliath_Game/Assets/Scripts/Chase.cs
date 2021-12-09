using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public int damageToGive = 1;
    
    public Transform player;
    public float speed;
    static Animator anim;
    //to make multiple goliaths get rid of "static" for the animator
    //bool pursuing = false;

    //public Transform head; 

    string state = "patrol";
    public GameObject[] waypoints;
    int currentWP = 0;
    public float rotSpeed = 0.2f;
    //
    
    float accuracyWP = 5.0f;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
      

    }
   
    // Update is called once per frame
    void Update()
    {
        GameObject David = GameObject.Find("David");
        Vector3 direction = player.position - this.transform.position;

        //direction.y = 0;
        float angle = Vector3.Angle(direction, this.transform.forward);

        // if (Vector3.Distance(player.position, this.transform.position) < 25 && angle < 35 || state == "pursuing")
        // if (Vector3.Distance(player.position, this.transform.position) < 25 && angle < 35)




        /* if(state == "patrol" && waypoints.Length > 0)
          {
              anim.SetBool("isIdle", false);
              anim.SetBool("isWalking", true);
              if(Vector3.Distance(waypoints[currentWP].transform.position, transform.position) < accuracyWP)
                  {
              //currentWP = Random.Range(0,waypoints.Length);
                  currentWP++;
                  if(currentWP >= waypoints.Length)
                      {
                        currentWP = 0;
                        anim.SetBool("isIdle", true);
              }
                  }

              //rotate towards waypoint
              direction = waypoints[currentWP].transform.position - transform.position;
              this.transform.rotation = Quaternion.Slerp(transform.rotation,
                  Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
              this.transform.Translate(0, 0, Time.deltaTime * speed);


          } */

        if (Vector3.Distance(player.position, this.transform.position) < 35 && (angle < 45))
        //anim.SetBool("isIdle", false);

        {

            if (direction.magnitude > 5)
            {
                 direction.y = 0;

                this.transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
                this.transform.Translate(0, 0, Time.deltaTime * speed);

                this.transform.rotation = Quaternion.Slerp(transform.rotation,
                 Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
                this.transform.Translate(0, 0, Time.deltaTime * speed);
                anim.SetBool("isWalking", true);
                anim.SetBool("isSlamming", false);
                anim.SetBool("isIdle", false);
            }




            else if (direction.magnitude < 5)
            {
                direction.y = 0;
                this.transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
                


                

                anim.SetBool("isSlamming", true);
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", false);

                this.transform.Translate(0, 0, Time.deltaTime * speed);

                Vector3 hitDirection = player.transform.position - transform.position;

                hitDirection = hitDirection.normalized;
                FindObjectOfType<HealthManager>().HurtPlayer(damageToGive, hitDirection);

            }


        }


        else if (direction.magnitude > 35 && (angle > 45))
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isSlamming", false);
            anim.SetBool("isWalking", false);
            //state = "patrol";
            //pursuing = false;
        }



    }
}

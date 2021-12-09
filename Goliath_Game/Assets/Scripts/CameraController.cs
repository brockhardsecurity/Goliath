using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;

    public Vector3 offset;

    public bool useOffsetValues;

    public float rotateSpeed;

    public Transform pivot;

    public float maxViewAngle;
    public float minViewAngle;

    public bool invertY;

    public bool useController;
    public GunController theGun;
    // Use this for initialization
    void Start () {
        if (!useOffsetValues)
        {
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;
        //pivot.transform.parent = target.transform;

        pivot.transform.parent = null; 

        Cursor.lockState = CursorLockMode.Locked; 

    }


	
	// Update is called once per frame
	void LateUpdate () {

        pivot.transform.position = target.transform.position;
        if (!useController)
        {
            //get the x position of the mouse & rotate the target
            float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            pivot.Rotate(0, horizontal, 0);

            //get the y position of the mouse & rotate the pivot
            float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
            // pivot.Rotate(-vertical, 0, 0);

            if (invertY)

            {
                pivot.Rotate(vertical, 0, 0);
            }
            else
            {
                pivot.Rotate(-vertical, 0, 0);
            }
        }
        if (useController)
        {

            float horizontal = Input.GetAxis("Look") * rotateSpeed;
            pivot.Rotate(0, horizontal, 0);

            //get the y position of the mouse & rotate the pivot
            float vertical = Input.GetAxis("LookUD") * rotateSpeed;





            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("RHorizontal") + Vector3.forward * -Input.GetAxisRaw("RVertical");
            if( playerDirection.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }
            if(Input.GetKeyDown(KeyCode.Joystick1Button3))
            
                theGun.isFiring = true;

            if (Input.GetKeyUp(KeyCode.Joystick1Button3))

                theGun.isFiring = false;
        }
        //limit up/down camera rotation
        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }

        if(pivot.rotation.eulerAngles.x > 180 && pivot.rotation.eulerAngles.x < 360f + minViewAngle)

        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }
            

        //move the camera based on current rotation of the target & the original offset
        float desiredYAngle = pivot.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

     
            //transform.position = target.position - offset; 


            if (transform.position.y < target.position.y)
        {

        transform.position = new Vector3(transform.position.x, target.position.y - .5f, transform.position.z);
        }

        transform.LookAt(target);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followcamera : MonoBehaviour
{
    public GameObject target;

    KeyCode trigger = KeyCode.Return;
    KeyCode upjoystick = KeyCode.UpArrow;
    public Rigidbody player;
    bool moveForward;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    //float speed = 0.003f;
    void cardboardcontroller()
    {

        if (Input.GetButton("Fire1") || Input.GetKey(upjoystick) || Input.GetKey(KeyCode.W)) // gets forward
        {
            moveForward = true;

        }
        else  // Stop
        {
            moveForward = false;
        }
        if (moveForward)
        {
            player.velocity = Camera.main.transform.forward * 1;
           // Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
           // player.SimpleMove(forward * 0.003f);
            // transform.Translate(new Vector3(moveHor, 0, moveVer));
        }

        float Y_Rot = Camera.main.transform.localEulerAngles.y;
        transform.localEulerAngles = new Vector3(0f, Y_Rot, 0);

        // transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, Camera.main.transform.rotation.eulerAngles.y, 0.0f), Time.deltaTime * 5f);
    }
    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            transform.position = target.transform.position;
            cardboardcontroller();
        }
    }
}

using UnityEngine;

public class fpsController : MonoBehaviour
{
    public virtuaFpslController vfpc;
    public VirtualJoystick virtualjoy;

    [Range(0,1)]
    public float Sensitivity;
    [Range(0,1)]
    public float moveSpeed; 
    //private Rigidbody rbPlayer;
    private float lookVer, lookHor;
    private float moveVer, moveHor;
    public bool cardboard = false;
    KeyCode trigger = KeyCode.Return;
    KeyCode upjoystick = KeyCode.UpArrow;
    public CharacterController player;
    public bool cam;
    void Start()
    {
        
        //rbPlayer = GetComponent<Rigidbody>();
        //Cursor.lockState = CursorLockMode.Locked; 

    }
 
    void Update()
    {
        if (!cardboard)
        {
            if (cam)
            {
                LookUp();
            }
            else
            {

                moveArround();
                lookHorF();

            }
        }
        else
        {
            if (gameObject.tag == "Player")
            {
               // cardboardcontroller();
            }
        }
    }
   
    void LookUp()
    {
        lookVer = vfpc.lookVer; 
        transform.localEulerAngles -= new Vector3(lookVer * Sensitivity, 0, 0); 
    }

    void lookHorF()
    {
        lookHor = vfpc.lookHor;
        transform.localEulerAngles += new Vector3(0,lookHor * Sensitivity, 0);
    }

    void moveArround()
    {
        moveHor = virtualjoy.handleJoystick.localPosition.x* moveSpeed * Time.deltaTime;
        moveVer = virtualjoy.handleJoystick.localPosition.y * moveSpeed * Time.deltaTime;

        transform.Translate(new Vector3(moveHor, 0, moveVer)); 
    }
}

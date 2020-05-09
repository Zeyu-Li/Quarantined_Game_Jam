
using UnityEngine;

public class move : MonoBehaviour
{




    public Camera camera;
    public CharacterController controller;

    altCamera cameraScript;

    [Header("Character")]
    [Range(0, 1)]
    public float drunkFactor;
    public float lagTime;

    public float desiredX;
    public float desiredY;
    public float currentX;
    public float currentY;

    [Header("ground movement")]
    public float maxGroundVel = 14f; //units/s
    public float timeToMaxVel = 1f; //time taken to reach maxVel from 0;

    public float groundAccel = 1; //amount to update velocity each time unit
    public float groundFric = 1;
    //veloctiy += acceleration - friction * velocity

    [Header("Jumping")]

    public Transform groundCheckLoc;
    public float groundCheckRad;
    public LayerMask ground;
    public bool isGrounded = true;
    public float gravity = -20f;
    public float jumpVel = 9;
    public float jumpHeight = 10;
    public float timeToApex = 1; //time taken to reach apex of jump

    private float tempX;
    private float tempY;
    public Vector3 velocity = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        updateNumbers();
        cameraScript = transform.GetChild(0).GetComponent<altCamera>();
    }

    // Update is called once per frame
    void Update()
    {

        groundCheck();
        updateMove();
        updateNumbers();
        updateDrunkness();
    }


    void updateDrunkness()
    {
        lagTime = Mathf.Lerp(0, 0.1f, drunkFactor);
        maxGroundVel = Mathf.Lerp(8, 4, drunkFactor);
        timeToMaxVel = Mathf.Lerp(0.3f, 3, drunkFactor);
        cameraScript.bobAmount = Mathf.Lerp(0.2f, 0.5f, drunkFactor);
        cameraScript.tumbleFactor = Mathf.Lerp(0, 30, drunkFactor);
        cameraScript.smoothTime = Mathf.Lerp(0.05f, 1.5f, drunkFactor);

    }
    void updateMove()
    {
        transform.rotation = Quaternion.Euler(0, camera.GetComponent<altCamera>().xCurrentRotation, 0);
        //move before updating velocity!
        controller.Move(velocity * Time.deltaTime);

        //current input
        desiredX = Input.GetAxisRaw("Horizontal");
        desiredY = Input.GetAxisRaw("Vertical");

        currentX = Mathf.SmoothDamp(currentX, desiredX, ref tempX, lagTime);
        currentY = Mathf.SmoothDamp(currentY, desiredY, ref tempY, lagTime);


        Vector3 cInput = (new Vector3(currentX, 0, currentY)).normalized;
        //rotating the current input to align wiht camera
        cInput = transform.rotation * cInput;

        Vector3 acceleration = (cInput * groundAccel);

        //the scale is to get stop friction from affecting vertivle movement
        velocity += (acceleration - groundFric * (Vector3.Scale(velocity, new Vector3(1, 0, 1)))) * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = jumpVel;
        }


        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
    }
    void updateNumbers()
    {
        gravity = -2 * jumpHeight / (timeToApex * timeToApex);
        jumpVel = 2 * jumpHeight / timeToApex;
        groundFric = 5 / (timeToMaxVel); //approximately
        groundAccel = maxGroundVel * groundFric;
    }
    void groundCheck()
    {
        bool temp = isGrounded;
        isGrounded = Physics.CheckSphere(groundCheckLoc.position, groundCheckRad, ground);
        if (!temp && isGrounded)
            velocity.y = 0;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckLoc.position, groundCheckRad);
    }
}
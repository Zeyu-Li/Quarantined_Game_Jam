
using UnityEngine;

public class playerCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public float xSensitivity;
    public float ySensitivity;
    private float xRotation;

    private float yRotation;
    //[HideInInspector]
    public float yCurrentRotation;
    private float yRotationV;
    public float smoothTime;
    //[HideInInspector]
    public float xCurrentRotation;
    private float xRotationV;
    public float yMin;
    public float yMax;
    [Header("====================")]
    public float tumbleAngle = 0;
    public float tumbleFactor = 0;

    public float targetTumble = 0;

    public float tumbleTime;
    float tumbleRef;

    float tempTime = 1;
    [Header("====================")]
    public Vector3 restPos;
    public float transitionSpeed = 20f;
    public float bobSpeed = 5f;
    public float bobAmount = 0.1f;

    public float bobTimer = 0;

    Vector3 camPos;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        camPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {




        xRotation += Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
        yRotation += -Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
        yRotation = Mathf.Clamp(yRotation, yMin, yMax);
        yCurrentRotation = Mathf.SmoothDamp(yCurrentRotation, yRotation, ref yRotationV, smoothTime);
        xCurrentRotation = Mathf.SmoothDamp(xCurrentRotation, xRotation, ref xRotationV, smoothTime);
        if (Time.time > tempTime)
        {
            targetTumble = Random.Range(-tumbleFactor, tumbleFactor);
            tempTime += 2;
        }
        tumbleAngle = Mathf.SmoothDamp(tumbleAngle, targetTumble, ref tumbleRef, tumbleTime);

        transform.rotation = Quaternion.Euler(yCurrentRotation, xCurrentRotation, tumbleAngle);

        headBob();


    }

    void headBob()
    {
        if (transform.parent.GetComponent<move>().velocity.magnitude > 0.1f
        && transform.parent.GetComponent<move>().isGrounded)
        {
            bobTimer += bobSpeed * Time.deltaTime;
            Vector3 newPos = new Vector3(
                Mathf.Cos(bobTimer) * bobAmount,
                restPos.y + Mathf.Abs(Mathf.Sin(bobTimer) * bobAmount),
                restPos.z
            );
            camPos = newPos;
        }
        else
        {
            bobTimer = 0;
            Vector3 newPos = Vector3.Lerp(camPos, restPos, transitionSpeed * Time.deltaTime);
            camPos = newPos;
        }
        if (bobTimer > Mathf.PI * 2)
        {
            bobTimer -= Mathf.PI * 2;
        }
        transform.localPosition = camPos;
    }
}

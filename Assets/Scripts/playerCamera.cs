using System.Collections;
using System.Collections.Generic;
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


    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        Cursor.visible = false;



        xRotation += Input.GetAxis("Mouse X") * xSensitivity;
        yRotation += -Input.GetAxis("Mouse Y") * ySensitivity;
        yRotation = Mathf.Clamp(yRotation, yMin, yMax);
        yCurrentRotation = Mathf.SmoothDamp(yCurrentRotation, yRotation, ref yRotationV, smoothTime);
        xCurrentRotation = Mathf.SmoothDamp(xCurrentRotation, xRotation, ref xRotationV, smoothTime);
        transform.rotation = Quaternion.Euler(yCurrentRotation, xCurrentRotation, 0f);

    }
}

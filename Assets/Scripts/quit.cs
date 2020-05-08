using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quit : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape")) // change escape to any other character if you like 
        {
            Application.Quit();
        }
    }
}

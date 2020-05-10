using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class quitToMenu : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape")) // change escape to any other character if you like 
        {

            SceneManager.LoadScene("title");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
    }
}

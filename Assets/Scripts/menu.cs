using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void play() {
        Cursor.visible = false;
        SceneManager.LoadScene("main");
    }

    public void quit() {
        Application.Quit();
    }

}

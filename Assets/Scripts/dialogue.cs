using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class dialogue : MonoBehaviour
{
    public bool done = false;
    public bool doneScene = false;

    // text
    public TextMeshProUGUI textDisplay;
    public GameObject cont;

    public string[] sentences;
    private int index;

    public float typingSpeed = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Type());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && done) {
            NextSentence();
            cont.SetActive(false);
            done = false;
        }

        if (doneScene) {
            SceneManager.LoadScene("main");
        }
    }

    IEnumerator Type() {
        foreach (char letter in sentences[index].ToCharArray()) {
            if (letter.Equals('/')) {
                textDisplay.text += "\n";
            } else {
                textDisplay.text += letter;
            }
            yield return new WaitForSeconds(typingSpeed);
        }
        cont.SetActive(true);
        done = true;
    }

    public void NextSentence() {
        if (index < sentences.Length - 1) {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        } else {
            doneScene = true;
        }
    }

}

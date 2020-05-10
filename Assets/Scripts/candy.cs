using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class candy : MonoBehaviour {
    
    public GameObject child;
    public bool ate = false;
    public float timeLimit = 40f;

    // player movement
    public movement move;
    public float speedChange = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ate) {
            timeLimit -= Time.deltaTime;
            if (timeLimit < 0) {
                ate = false;
                timeLimit = 40f;
                move.movementSpeed -= speedChange;
            }
        }
    }
    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.CompareTag("Player")) {
            child.SetActive(false);
            ate = true;
            move.movementSpeed += speedChange;
        }
    }
}

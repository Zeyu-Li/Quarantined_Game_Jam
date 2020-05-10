using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete : MonoBehaviour {
    GameObject objs;
    // Start is called before the first frame update
    void Start()
    {
        objs = GameObject.FindGameObjectWithTag("Music");

        try {
            Debug.Log(objs);
            Destroy(objs);
        } catch {
            ;
        }
    }
}

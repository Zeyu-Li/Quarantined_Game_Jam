﻿
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public Animator animator;
    public Camera cam;
    public gameMaster master;
    Slider slider;

    public float confortLevel;
    public float maxConfort = 5;


    public float pickUpDistance = 2;
    public bool canBeDamaged = true;

    // Start is called before the first frame update
    void Start()
    {
        master = GameObject.Find("GameMaster").GetComponent<gameMaster>();


        slider = GameObject.Find("Hp bar").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (master.gameOn)
        {

            RaycastHit hit;
            Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.red, 0.05f);
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, pickUpDistance))
            {

                if (hit.collider.gameObject.tag == "Item")
                {
                    Debug.Log(hit.collider.gameObject.tag);

                    if (Input.GetKey(KeyCode.F))
                    {
                        if (confortLevel < maxConfort)
                        {
                            confortLevel++;
                        }
                        GameObject.Destroy(hit.collider.gameObject);
                        master.itemPickedUp();
                    }
                }
            }
            slider.value = confortLevel / maxConfort;
        }
    }

    public void takeDamage()
    {
        if (canBeDamaged)
        {
            confortLevel--;
            if (confortLevel < 0)
            {
                master.gameOver();
            }
            canBeDamaged = false;
            animator.SetTrigger("doFade");
            StartCoroutine(waitThree());
        }

    }



    private IEnumerator waitThree()
    {
        yield return new WaitForSeconds(3f);
        canBeDamaged = true;
    }




}

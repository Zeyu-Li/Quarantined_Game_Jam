
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




            slider.value = confortLevel / maxConfort;
        }
    }

    public void takeDamage()
    {
        if (canBeDamaged)
        {
            confortLevel--;
            if (confortLevel <= 0)
            {
                master.gameOver();
            }
            canBeDamaged = false;
            animator.SetTrigger("doFade");
            StartCoroutine(waitThree());
        }

    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Item")
        {
            if (confortLevel < maxConfort)
            {
                confortLevel++;
            }
            GameObject.Destroy(other.gameObject);
            master.itemPickedUp();
        }

    }


    private IEnumerator waitThree()
    {
        yield return new WaitForSeconds(3f);
        canBeDamaged = true;
    }




}

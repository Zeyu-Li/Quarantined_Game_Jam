using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().SetDestination(target.position);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, target.position - transform.position, out hit))
        {
            if (hit.distance < 0.5f)
                if (hit.collider.gameObject.tag == "Player")
                {
                    hit.collider.gameObject.GetComponent<player>().takeDamage();
                    StartCoroutine(pauseChasing());
                }
        }
    }

    public IEnumerator pauseChasing()
    {
        GetComponent<NavMeshAgent>().isStopped = true;
        yield return new WaitForSeconds(2);
        GetComponent<NavMeshAgent>().isStopped = false;
    }


}

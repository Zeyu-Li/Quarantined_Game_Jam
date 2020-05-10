using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    public Transform target;
    public float hitRadius = 1;

    Transform player;
    NavMeshAgent agent;
    gameMaster master;
    bool fleeing;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        GameObject temp = GameObject.Find("GameMaster");
        if (temp != null)
        {
            master = temp.GetComponent<gameMaster>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (master != null || target != null)
        {
            agent.SetDestination(target.position);

            RaycastHit hit;

            if (Physics.Raycast(transform.position, target.position - transform.position, out hit))
            {
                if (hit.distance < hitRadius)
                    if (hit.collider.gameObject.tag == "Player" && !fleeing)
                    {
                        GetComponent<AudioSource>().Play();
                        hit.collider.gameObject.GetComponent<player>().takeDamage();
                        StartCoroutine(pauseChasing());
                    }
            }
        }
    }

    public IEnumerator pauseChasing()
    {
        fleeing = true;
        agent.isStopped = true;
        yield return new WaitForSeconds(1);
        agent.isStopped = false;
        target = master.pickUpLocations[Random.Range(0, master.pickUpLocations.Count)];
        yield return new WaitForSeconds(4);
        target = player;
        fleeing = false;
    }


}

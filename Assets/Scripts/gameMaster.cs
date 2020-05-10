using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class gameMaster : MonoBehaviour
{


    public List<Transform> pickUpLocations;
    public List<GameObject> itemsToSpawn;
    public GameObject enemyPrefab;
    public bool gameOn = true;

    player p;
    GameObject enemy;
    GameObject hpBar;
    GameObject gameOverLabel;
    // Start is called before the first frame update
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;

        hpBar = GameObject.Find("Hp bar");
        gameOverLabel = GameObject.Find("Game Over Label");
        initialize();




    }

    // Update is called once per frame
    void Update()
    {

    }
    public void initialize()
    {
        //clean up
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (var item in items)
        {
            Destroy(item);
        }
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
        p.confortLevel = p.maxConfort;
        p.canBeDamaged = true;

        gameOverLabel.SetActive(false);
        hpBar.SetActive(true);

        spawnEnemy();
        spawnNewItem();

    }

    public void spawnNewItem()
    {
        Transform rngTransform = pickUpLocations[Random.Range(0, pickUpLocations.Count)];
        Instantiate(itemsToSpawn[Random.Range(0, itemsToSpawn.Count)], rngTransform.position, rngTransform.rotation);
    }

    public void spawnEnemy()
    {
        Transform rngTransform = pickUpLocations[Random.Range(0, pickUpLocations.Count)];
        enemy = Instantiate(enemyPrefab, rngTransform.position, rngTransform.rotation);
        enemy.GetComponent<enemy>().target = p.transform;

    }
    public void itemPickedUp()
    {
        spawnNewItem();
        Transform rngTransform = pickUpLocations[Random.Range(0, pickUpLocations.Count)];
        StartCoroutine(enemy.GetComponent<enemy>().pauseChasing());
    }
    public void gameOver()
    {
        gameOverLabel.SetActive(true);
        hpBar.SetActive(false);
    }


}

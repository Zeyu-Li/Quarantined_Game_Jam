using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class gameMaster : MonoBehaviour
{
    public Transform spawnLocation;

    public List<Transform> pickUpLocations;
    public List<GameObject> itemsToSpawn;
    public GameObject enemyPrefab;
    public GameObject candyPrefab;
    public bool gameOn = true;

    player p;

    GameObject enemy;
    GameObject hpBar;
    GameObject gameOverLabel;

    GameObject halo;
    int numberOfItems;


    // Start is called before the first frame update
    void Start()
    {

        initialize();
    }

    // Update is called once per frame
    void Update()
    {
        checkWinGame();
        if (gameOn == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                initialize();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneManager.LoadScene("title");
            }
        }
    }
    public void initialize()
    {
        Cursor.lockState = CursorLockMode.Locked;
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
        hpBar = GameObject.Find("Hp bar");
        gameOverLabel = GameObject.Find("Game Over Label");
        halo = GameObject.Find("Halo");

        //clean up
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach (var item in items)
        {
            Destroy(item);
        }
<<<<<<< HEAD
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<player>();
        p.confortLevel = 3;
=======

        p.confortLevel = (int)(p.maxConfort / 3);
>>>>>>> 5bafd2baad75e6275829934a7b31118e2701ec7a
        p.canBeDamaged = true;
        halo.SetActive(false);
        gameOverLabel.SetActive(false);
        hpBar.SetActive(true);
        numberOfItems = 0;
        spawnEnemy();
        spawnNewItem();
    }

    public void spawnCandy()
    {
        Instantiate(candyPrefab, spawnLocation.position, spawnLocation.rotation);
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
        numberOfItems++;
        spawnNewItem();
        Transform rngTransform = pickUpLocations[Random.Range(0, pickUpLocations.Count)];
        StartCoroutine(enemy.GetComponent<enemy>().pauseChasing());


        if (numberOfItems % 4 == 0)
        {
            spawnCandy();
        }



    }
    public void gameOver()
    {
        gameOverLabel.SetActive(true);
        hpBar.SetActive(false);
        gameOn = false;
    }

    public void checkWinGame()
    {
        if (p.confortLevel == p.maxConfort)
        {
            halo.SetActive(true);
        }


        if (halo.activeInHierarchy)
        {
            if (Vector3.Distance(p.transform.position, halo.transform.position) < 0.75f)
            {
                p.canBeDamaged = false;
                enemy.SetActive(false);
                hpBar.SetActive(false);
                Debug.Log("win");
                SceneManager.LoadScene("end");
            }
        }
    }
}

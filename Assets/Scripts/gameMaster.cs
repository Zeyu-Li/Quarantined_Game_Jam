﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        Cursor.lockState = CursorLockMode.Locked;
        hpBar = GameObject.Find("Hp bar");
        gameOverLabel = GameObject.Find("Game Over Label");
        halo = GameObject.Find("Halo");
        initialize();

    }

    // Update is called once per frame
    void Update()
    {
        checkWinGame();
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
        p.confortLevel = (int)(p.maxConfort / 3);
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
    }

    public void checkWinGame()
    {
        if (p.confortLevel == p.maxConfort)
        {
            halo.SetActive(true);
        }


        if (halo.activeInHierarchy)
        {
            if (Vector3.Distance(p.transform.position, halo.transform.position) < 1)
            {
                p.canBeDamaged = false;
                //win game here
            }
        }
    }
}

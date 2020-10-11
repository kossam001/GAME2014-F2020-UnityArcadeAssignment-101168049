/* EnemyManager.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-11
 * 
 * Manages enemy spawning.
 * 
 * 2020-10-11: Added script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Requirements
    public List<GameObject> enemyPrefabs;
    public FireballManager fireballManager; // Enemies need access
    public List<GameObject> spawnPoints; 

    // Spawn data
    private int numEnemiesActive;
    public int maxEnemiesActive = 5;
    private float spawnTimer = 0;

    // Data structures
    public List<GameObject> enemies;
    public List<GameObject> activeEnemies;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject CPU in enemyPrefabs)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject newEnemy = Instantiate(CPU, transform);
                newEnemy.GetComponent<CPUFire>().fireballManager = fireballManager;
                newEnemy.SetActive(false);
                //newEnemy.GetComponent<Item>().gameManager = this;

                enemies.Add(newEnemy);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Spawn timer stops when max enemies reached
        if (numEnemiesActive >= maxEnemiesActive)
        {
            return;
        }

        spawnTimer -= Time.deltaTime;
        if (spawnTimer > 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, enemies.Count - 1);
        GameObject spawnedEnemy = enemies[randomIndex];
        activeEnemies.Add(spawnedEnemy);

        // Set transform
        GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        spawnedEnemy.transform.position = spawnPoint.transform.position;
        spawnedEnemy.transform.rotation = spawnPoint.transform.rotation;

        spawnedEnemy.SetActive(true);
        numEnemiesActive++;

        enemies.RemoveAt(randomIndex);

        // Reset timer
        spawnTimer = Random.Range(3f, 10f);
    }
}

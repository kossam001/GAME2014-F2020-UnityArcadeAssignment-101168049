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
    public List<GameObject> enemyPrefabs;
    public List<GameObject> enemies;

    public FireballManager fireballManager; // Enemies need access

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
        
    }
}

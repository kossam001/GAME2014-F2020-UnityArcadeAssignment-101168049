/* GameManager.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-10
 * 
 * Manages different aspects of the game such as score, spawning, stat changes, etc.
 * 
 * 2020-10-10: Added script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int itemsSpawned = 0;
    public List<GameObject> inactiveItems;
    public List<GameObject> activeItems;

    public List<GameObject> itemPrefabs;
    public GameObject fireballPrefab;
    public List<GameObject> enemyPrefabs;

    public List<GameObject> fireballs;
    public List<GameObject> enemies;

    private void Start()
    {
        foreach (GameObject item in itemPrefabs)
        {
            for (int i=0; i < 5; i++)
            {
                GameObject newItem = Instantiate(item, transform);
                newItem.SetActive(false);
                newItem.GetComponent<Item>().gameManager = this;

                inactiveItems.Add(newItem);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (itemsSpawned <= 0)
        {
            float xPos = Random.Range(-16f, 12f);
            float yPos = Random.Range(-8f, 20f);

            if (inactiveItems.Count <= 0)
            {
                return;
            }

            int randomIndex = Random.Range(0, inactiveItems.Count - 1);
            GameObject activeItem = inactiveItems[randomIndex];

            activeItem.transform.position = new Vector2(xPos, yPos); // Random position
            activeItem.SetActive(true); // Activate
            activeItems.Add(activeItem); // Move to active list 
            inactiveItems.RemoveAt(randomIndex); // Remove from inactive list

            itemsSpawned++;
        }
    }
}

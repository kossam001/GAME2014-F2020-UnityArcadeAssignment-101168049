/* ItemManager.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-11
 * 
 * Manages item spawning.
 * 
 * 2020-10-11: Added script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public int itemsSpawned = 0;
    public List<GameObject> inactiveItems;
    public List<GameObject> activeItems;

    public List<GameObject> itemPrefabs;

    // Spawn Bounds
    public Transform upperLeftCorner;
    public Transform upperRightCorner;
    public Transform lowerLeftCorner;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject item in itemPrefabs)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject newItem = Instantiate(item, transform);
                newItem.SetActive(false);
                newItem.GetComponent<Item>().manager = this;

                inactiveItems.Add(newItem);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (itemsSpawned <= 0)
        {
            float xPos = Random.Range(upperLeftCorner.position.x, upperRightCorner.position.x);
            float yPos = Random.Range(upperLeftCorner.position.y, lowerLeftCorner.position.y);

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

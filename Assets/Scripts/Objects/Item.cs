/* Item.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-10
 * 
 * Picking up an item should give an effect.
 * 
 * 2020-10-10: Added script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        meat,
        shell,
        egg,
        pepper
    }

    ItemType itemID;
    private GameObject owner;

    public void Pickup()
    {
        switch (itemID)
        {
            case ItemType.meat:
                Destroy(gameObject);
                break;
            case ItemType.shell:
                Destroy(gameObject);
                break;
            case ItemType.egg:
                Destroy(gameObject);
                break;
            case ItemType.pepper:
                Destroy(gameObject);
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" ||
            collision.gameObject.tag == "CPU")
        {
            owner = collision.gameObject;
            Pickup();
        }
    }
}

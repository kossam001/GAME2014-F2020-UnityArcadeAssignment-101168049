/* Item.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-10
 * 
 * Picking up an item should give an effect.
 * 
 * 2020-10-10: Added script.
 * 2020-10-10: Items become disabled instead of destroyed when used.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Item : MonoBehaviour
{
    public Sprite sprite;
    public SpriteRenderer renderer;
    protected GameObject owner;
    public ItemManager manager;

    private void Start()
    {
        renderer.sprite = sprite;
    }

    public virtual void Pickup()
    {
        manager.inactiveItems.Add(gameObject);
        manager.activeItems.Remove(gameObject);
        gameObject.SetActive(false);

        manager.itemsSpawned--;

        if (owner.tag == "Player")
            GameManager.Instance.AddScore(1500);
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
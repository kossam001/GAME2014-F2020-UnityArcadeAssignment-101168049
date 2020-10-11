﻿/* Item.cs
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
    public GameManager gameManager;

    private void Start()
    {
        renderer.sprite = sprite;
    }

    public virtual void Pickup()
    {
        gameManager.inactiveItems.Add(gameObject);
        gameManager.activeItems.Remove(gameObject);
        gameObject.SetActive(false);

        gameManager.itemsSpawned--;
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
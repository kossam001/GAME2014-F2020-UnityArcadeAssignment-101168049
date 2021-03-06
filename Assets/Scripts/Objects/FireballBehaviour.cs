﻿/* FireballBehaviour.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-12
 * 
 * To be attached to a fireball GameObject.
 * What a fireball is supposed to move forward until it collides with something
 * and disappears.
 * 
 * 2020-10-06: Added this script.
 * 2020-10-07: Added collision handling.
 * 2020-10-10: Fireball speed modification.  Scales with player speed.
 * 2020-10-11: Fireball collisions with other objects.
 * 2020-10-11: Fireballs do not get destroyed.
 * 2020-10-12: Added Nest collision.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{
    public float speed;
    public GameObject owner;
    public FireballManager manager;
    public bool despawn = false;

    private int points = 250;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        // Fireball needs to be despawned by something, have it raise a flag
        if (despawn)
        {
            owner = null;
            gameObject.SetActive(false);
            manager.EnqueueFireball(gameObject);
            despawn = false;
        }
    }

    private void Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!owner)
        {
            return;
        }

        if (collision.gameObject.tag == "StaticObstacle")
        {
            manager.EnqueueFireball(gameObject);
            gameObject.SetActive(false);
        }
        // Projectiles destroy other projectiles
        else if (collision.gameObject.tag == "Projectile")
        {
            if (owner.tag == "Player")
            {
                GameManager.Instance.AddScore(points);
            }

            manager.EnqueueFireball(gameObject);
            gameObject.SetActive(false);
        }
        // It is possible to destroy own nest
        else if (collision.gameObject.name == "Nest")
        {
            collision.gameObject.GetComponent<Nest>().DecreaseHealth();

            manager.EnqueueFireball(gameObject);
            gameObject.SetActive(false);
        }
        else if (owner != null)
        {
            if (collision.gameObject.tag == "CPU" && 
                !ReferenceEquals(collision.gameObject, owner) &&
                owner.tag != "CPU") // CPUs should not be able to kill other CPUs
            {
                owner = null;
                collision.gameObject.GetComponent<CharacterStats>().DecreaseHealth();
                manager.EnqueueFireball(gameObject);
                gameObject.SetActive(false);
                
            }
            else if (collision.gameObject.tag == "Player" && !ReferenceEquals(collision.gameObject, owner))
            {
                owner = null;
                collision.gameObject.GetComponent<CharacterStats>().DecreaseHealth();
                manager.EnqueueFireball(gameObject);
                gameObject.SetActive(false);
            }
        }
    }
}

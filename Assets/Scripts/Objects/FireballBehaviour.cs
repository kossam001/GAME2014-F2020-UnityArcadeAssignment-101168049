﻿/* FireballBehaviour.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-10
 * 
 * To be attached to a fireball GameObject.
 * What a fireball is supposed to move forward until it collides with something
 * and disappears.
 * 
 * 2020-10-06: Added this script.
 * 2020-10-07: Added collision handling.
 * 2020-10-10: Fireball speed modification.  Scales with player speed.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{
    public float speed;
    public FireballManager manager;
    public bool despawn = false;

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
            gameObject.SetActive(false);
            manager.fireballs.Enqueue(gameObject);
            despawn = false;
        }
    }

    private void Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "StaticObstacle")
        {
            manager.fireballs.Enqueue(gameObject);
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
}

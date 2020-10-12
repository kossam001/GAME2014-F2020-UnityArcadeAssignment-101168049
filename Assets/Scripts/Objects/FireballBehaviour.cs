/* FireballBehaviour.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-11
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
        // Projectiles destroy other projectiles
        else if (collision.gameObject.tag == "Projectile")
        {
            manager.fireballs.Enqueue(gameObject);
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
                manager.fireballs.Enqueue(gameObject);
                gameObject.SetActive(false);

                // Enemy dead if health = 0
                if (collision.gameObject.GetComponent<CharacterStats>().GetHealth() <= 0)
                {
                    collision.gameObject.GetComponent<CharacterStats>().isDead = true;
                }
            }
            else if (collision.gameObject.tag == "Player" && !ReferenceEquals(collision.gameObject, owner))
            {
                owner = null;
                collision.gameObject.GetComponent<CharacterStats>().DecreaseHealth();
                manager.fireballs.Enqueue(gameObject);
                gameObject.SetActive(false);
            }
        }
    }
}

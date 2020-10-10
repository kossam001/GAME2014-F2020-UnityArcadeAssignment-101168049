/* FireballBehaviour.cs
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
 * 2020-10-10: Fireball speed modification
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{
    public float speed;
    public GameObject owner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        // Fireballs should travel faster than owner
        if (owner)
        {
            speed = owner.GetComponent<CharacterStats>().speed + 1.5f;
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
            Destroy(gameObject);
        }
    }
}

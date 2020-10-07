/* FireballBehaviour.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-06
 * 
 * To be attached to a fireball GameObject.
 * What a fireball is supposed to move forward until it collides with something
 * and disappears.
 * 
 * 2020-10-06: Added this script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "StaticObstacle")
            Destroy(gameObject);
    }
}

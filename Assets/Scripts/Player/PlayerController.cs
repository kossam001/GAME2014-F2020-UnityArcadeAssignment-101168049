/* PlayerController.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-09
 * 
 * Controls the player character.
 * 
 * 2020-10-06: Added this script.
 * 2020-10-09: Added testing code.  Need removal.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterStats stats;
    public GameObject fireball;
    public float size = 0.9f;
    public FireballManager fireballManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Turn();
        Fire();

        cooldown -= Time.deltaTime;
        //Debug.Log("Turn Right? " + !CanTurnRight());
        //Debug.Log("Turn Left? " + !CanTurnLeft());
        //Debug.Log("Turn Back? " + CanTurnBack());
        //Debug.Log("See? " + Search());
    }

    private void Move()
    {
        // moves character by getting axis value - keyboard input 
        transform.position += new Vector3(Input.GetAxis("Horizontal") * stats.speed * Time.deltaTime,
                                          Input.GetAxis("Vertical") * stats.speed * Time.deltaTime,
                                          0.0f);
    }

    private void Turn()
    {
        // movement vector - calculated using current position minus future position
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal") + transform.position.x, Input.GetAxis("Vertical") + transform.position.y)
            - transform.position;

        // don't update rotation if player is not moving
        if (moveDirection.sqrMagnitude != 0)
        {
            // rotate player gradually in the direction of movement.
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 10);
        }

        // Simple rotation
        //float rotationSpeed = 20;

        //if (Input.GetAxis("Horizontal") > 0.1f)
        //{
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), rotationSpeed);
        //}
        //if (Input.GetAxis("Horizontal") < -0.1f)
        //{
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, 180)), rotationSpeed);
        //}
        //if (Input.GetAxis("Vertical") > 0.1f)
        //{
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, 90)), rotationSpeed);
        //}
        //if (Input.GetAxis("Vertical") < -0.1f)
        //{
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, 270)), rotationSpeed);
        //}
    }

    float cooldown;
    private void Fire()
    {
        if (cooldown > 0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject fireball = fireballManager.fireballs.Dequeue();
            fireball.transform.position = transform.position;
            fireball.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -90);
            fireball.GetComponent<FireballBehaviour>().speed = GetComponent<CharacterStats>().speed + 1.5f;
            fireball.SetActive(true);
            //GameObject newFireball = Instantiate(fireball, transform.position, transform.rotation * Quaternion.Euler(0, 0, -90));
            //newFireball.GetComponent<FireballBehaviour>().owner = gameObject; // Doing this instead of parenting so the scale is unaffected
            cooldown = stats.firerate;
        }
        
    }

    public float dist = 1.1f;
    public float scale = 0.1f;
    private bool CanTurnBack()
    {
        return Physics2D.BoxCast(transform.position,
    new Vector2(size * scale, size * scale), 0, transform.right, dist, 0b10100000000);
    }

    private bool CanTurnRight()
    {
        return Physics2D.BoxCast(transform.GetComponent<CapsuleCollider2D>().bounds.center,
            new Vector2(size, size), 0, -transform.up, 3, 0b10100000000);
    }

    private bool CanTurnLeft()
    {
        return Physics2D.BoxCast(transform.GetComponent<CapsuleCollider2D>().bounds.center,
            new Vector2(size, size), 0, transform.up, 3, 0b10100000000);
    }

    public float sight = 10;
    private bool Search()
    {
        return Physics2D.Raycast(transform.position, transform.right, 10, 0b1100000000);
    }
}

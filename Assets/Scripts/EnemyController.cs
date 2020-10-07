/* EnemyController.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-07
 * 
 * Controls the enemy character.
 * 
 * 2020-10-07: Added this script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
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
        Turn();
    }

    private void Move()
    {
        // moves character by getting axis value - keyboard input 
        transform.position += new Vector3(speed * Time.deltaTime,
                                          speed * Time.deltaTime,
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
    }
}

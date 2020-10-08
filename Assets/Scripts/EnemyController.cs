/* EnemyController.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-08
 * 
 * Controls the enemy character.
 * 
 * 2020-10-07: Added this script.
 * 2020-10-08: Added turning.
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
        transform.position += new Vector3(transform.right.x * speed * Time.deltaTime,
                                          transform.right.y * speed * Time.deltaTime,
                                          0.0f);

        
    }

    private bool CanTurnRight()
    {
        // Check for obstructions on the right
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, 3, 0x0100);

        //Debug.Log(hit.collider.name);

        // if there was a hit
        //if (hit)
        //{

        //    return false;
        //}

        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(1.4f, 1.4f), 0, -transform.up, 3, 0x0100);
        if (hit)
        {
            return false;
        }

        return true;
    }

    private bool CanTurnLeft()
    {
        // Check for obstructions on the right
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(1.4f, 1.4f), 0, transform.up, 3, 0x0100);

        // if there was a hit
        if (hit)
        {
            return false;
        }

        return true;
    }

    private bool ShouldTurnBack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1, 0x0100);

        if (hit)
        {
            return true;
        }

        return false;
    }

    bool isTurning = false;
    Vector2 targetDirection;
    private void Turn()
    {
        if (CanTurnRight() && !isTurning)
        {
            Debug.Log("Right");
            isTurning = true;
            StartCoroutine(TurningInProgress(-transform.up, -90));
        }
        else if (CanTurnLeft() && !isTurning)
        {
            Debug.Log("Left");
            isTurning = true;
            StartCoroutine(TurningInProgress(transform.up, 90));
        }
        else if (ShouldTurnBack() && !isTurning)
        {
            Debug.Log("Back");
            isTurning = true;
            StartCoroutine(TurningInProgress(-transform.right, 180));
        }
    }

    private IEnumerator TurningInProgress(Vector2 targetDirection, float angle)
    {
        while (Vector2.Angle(transform.right, targetDirection) > 1)
        {
            // rotate player gradually in the direction of movement.
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles,
                transform.rotation.eulerAngles + new Vector3(0, 0, angle),
                0.05f);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f); // Delay turning for 0.5s to prevent U-turns
        isTurning = false;
    }
}

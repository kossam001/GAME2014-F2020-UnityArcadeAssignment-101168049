/* CPUMove.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-09
 * 
 * Script containing functions for CPU movement.
 * 
 * 2020-10-09: Added script.
 * 2020-10-09: Refactored code.
 * 2020-10-09: Stopped generating multiple coroutines that causes CPU to spin in place.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private float boxCastSize = 0.9f;

    public void Wander()
    {
        Move();
        Turn();
    }

    public void Move()
    {
        // moves character by getting axis value - keyboard input 
        transform.position += new Vector3(transform.right.x * speed * Time.deltaTime,
                                          transform.right.y * speed * Time.deltaTime,
                                          0.0f);
    }

    // Turning variables
    public bool isTurning = false;

    Vector2 targetDirection;
    float turnCheckInterval;

    // Randomly turns when possible
    public void Turn()
    {
        if (turnCheckInterval > 0)
        {
            turnCheckInterval -= Time.deltaTime;
            return;
        }

        // randomly turn right
        if (CanTurnRight() && !isTurning)
        {
            if (MakeTurn("Right", -transform.up, -90, 0.25f))
            {
                return;
            }

            turnCheckInterval = 0.5f;
        }
        // randomly turn left
        if (CanTurnLeft() && !isTurning)
        {
            if (MakeTurn("Left", transform.up, 90, 0.25f))
            {
                return;
            }

            turnCheckInterval = 0.5f;
        }
    }

    // Decide whether or not to turn based on probability
    // Start coroutine to turn.
    public bool MakeTurn(string debugDir, Vector3 movementDir, float angle, float probability)
    {
        if (isTurning)
        {
            return false;
        }

        bool success = false;
        if (Random.value < probability)
        {
            isTurning = true;
            StartCoroutine(TurningInProgress(movementDir, angle));
            success = true;
        }

        return success;
    }

    // Gradually turn instead of snapping to the right direction
    // Runs separately from Update()
    public IEnumerator TurningInProgress(Vector2 targetDirection, float angle)
    {
        while (Vector2.Angle(transform.right, targetDirection) > 1)
        {
            // rotate player gradually in the direction of movement.
            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles,
                transform.rotation.eulerAngles + new Vector3(0, 0, angle),
                0.1f);
            
            yield return null;
        }

        yield return new WaitForSeconds(0.5f); // Delay turning for 0.5s to prevent U-turns
        isTurning = false;
    }

    // Check if turning is possible
    public bool CanTurnRight()
    {
        // Cast a box ray that determines whether or not the character can fit into a passage
        RaycastHit2D hit = Physics2D.BoxCast(transform.GetComponent<CapsuleCollider2D>().bounds.center,
            new Vector2(boxCastSize, boxCastSize), 0, -transform.up, 1, 0x0100);

        if (hit)
        {
            return false;
        }

        return true;
    }

    // Check if turning is possible on the left
    public bool CanTurnLeft()
    {
        // Check for obstructions on the right
        RaycastHit2D hit = Physics2D.BoxCast(transform.GetComponent<CapsuleCollider2D>().bounds.center,
            new Vector2(boxCastSize, boxCastSize), 0, transform.up, 1, 0x0100);

        // if there was a hit
        if (hit)
        {
            return false;
        }

        return true;
    }

    // Check for a Dead End
    public bool ShouldTurnBack()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position,
            new Vector2(boxCastSize * 0.1f, boxCastSize * 0.1f), 0, transform.right, 1.1f, 0x0100);

        if (hit)
        {
            bool canTurnLeft = CanTurnLeft();
            bool canTurnRight = CanTurnRight();

            // Dead end
            if (!canTurnLeft && !canTurnRight)
            {
                return true;
            }
        }
        return false;
    }
    
    // Predictive Search
    public void Chase(Vector3 playerLastPos, Vector3 playerLastDir, ref bool isChasing)
    {
        // Close enough to position
        if (Vector2.Distance(playerLastPos, transform.position) < 1f && isChasing)
        {
            float turnAngle = Vector2.Angle(transform.right, playerLastDir);

            // Do some extra work to figure out the sign of the angle
            turnAngle = Vector3.Angle(transform.right, playerLastDir);
            Vector3 cross = Vector3.Cross(transform.right, playerLastDir);
            if (cross.z < 0)
                turnAngle = -turnAngle;

            Debug.Log("TURN " + turnAngle);

            Vector3 dir = transform.right;

            // Make the turns 90 degrees
            if (CanTurnRight() && turnAngle < -10 && !isTurning)
            {
                turnAngle = -90;
                dir = -transform.up;
                MakeTurn("Chase enemy", dir, turnAngle, 1f);
            }
            else if (CanTurnLeft() && turnAngle > 10 && !isTurning)
            {
                turnAngle = 90;
                dir = transform.up;
                MakeTurn("Chase enemy", dir, turnAngle, 1f);
            }
            isChasing = false;
        }

        if (ShouldTurnBack())
        {
            isChasing = false;
        }
    }

    public void EncounterWall()
    {
        bool canTurnLeft = CanTurnLeft();
        bool canTurnRight = CanTurnRight();

        // Dead end
        if (!canTurnLeft && !canTurnRight)
        {
            if (ShouldTurnBack() && !isTurning)
            {
                MakeTurn("Back", -transform.right, 180, 1f);
            }
        }

        // Fork
        else if (!isTurning)
        {
            if (canTurnLeft && canTurnRight)
            {
                if (Random.value > 0.5f)
                {
                    MakeTurn("Left", transform.up, 90, 1f);
                }
                else
                {
                    MakeTurn("Right", -transform.up, -90, 1f);
                }
            }
            // Can only go left
            else if (!canTurnRight && canTurnLeft)
            {
                MakeTurn("Left", transform.up, 90, 1f);
            }
            // Can only go right
            else if (!canTurnLeft && canTurnRight)
            {
                MakeTurn("Right", -transform.up, -90, 1f);
            }

            isTurning = true;
        }
    }
}

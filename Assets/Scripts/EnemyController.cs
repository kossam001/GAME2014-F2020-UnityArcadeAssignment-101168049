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
 * 2020-10-08: Added random wandering.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    [SerializeField]
    private float boxCastSize = 0.9f;

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

        RaycastHit2D hit = Physics2D.BoxCast(transform.GetComponent<CapsuleCollider2D>().bounds.center, 
            new Vector2(boxCastSize, boxCastSize), 0, -transform.up, 1, 0x0100);

        if (hit)
        {
            return false;
        }

        return true;
    }

    private bool CanTurnLeft()
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

    private bool ShouldTurnBack()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position,
            new Vector2(boxCastSize * 0.1f, boxCastSize * 0.1f), 0, transform.right, 1.1f, 0x0100);

        if (hit)
        {
            Debug.Log("Wall");

            bool canTurnLeft = CanTurnLeft();
            bool canTurnRight = CanTurnRight();

            // Dead end
            if (!canTurnLeft && !canTurnRight)
            {
                return true;
            }

            //// Fork
            //else if (canTurnLeft && canTurnRight)
            //{
            //    if (Random.value > 0.5f)
            //    {
            //        Debug.Log("Force Left");
            //        isTurning = true;
            //        StartCoroutine(TurningInProgress(transform.up, 90));
            //    }
            //    else
            //    {
            //        Debug.Log("Force Right");
            //        isTurning = true;
            //        StartCoroutine(TurningInProgress(-transform.up, -90));
            //    }
            //}
            //// Can only go left
            //else if (!canTurnRight && canTurnLeft)
            //{
            //    Debug.Log("Force Left");
            //    isTurning = true;
            //    StartCoroutine(TurningInProgress(transform.up, 90));
            //}
            //// Can only go right
            //else if (!canTurnLeft && canTurnRight)
            //{
            //    Debug.Log("Force Right");
            //    isTurning = true;
            //    StartCoroutine(TurningInProgress(-transform.up, -90));
            //}
        }
        return false;
    }

    bool isTurning = false;
    Vector2 targetDirection;
    float turnCheckInterval;
    private void Turn()
    {
        if (turnCheckInterval > 0)
        {
            turnCheckInterval -= Time.deltaTime;
            return;
        }
        //else
        //{
        //    turnCheckInterval = 1f;
        //}

        // randomly turn right
        if (CanTurnRight() && !isTurning)// && Random.value < 0.25f)
        {
            //if (Random.value < 0.25f)
            //{
            //    Debug.Log("Right");
            //    isTurning = true;
            //    StartCoroutine(TurningInProgress(-transform.up, -90));
            //}
            if (MakeTurn("Right", -transform.up, -90, 0.25f))
            {
                return;
            }

            turnCheckInterval = 0.5f;
        }
        // randomly turn left
        if (CanTurnLeft() && !isTurning)// && Random.value > 0.75f)
        {
            if (MakeTurn("Left", transform.up, 90, 0.25f))
            {
                return;
            }

            turnCheckInterval = 0.5f;
        }
        // check for a dead end
        //if (ShouldTurnBack() && !isTurning)
        //{
        //    Debug.Log("Turn Back");
        //    isTurning = true;
        //    StartCoroutine(TurningInProgress(-transform.right, 180));
        //}
        // hit a wall but not a dead end
        //else
        //{
        //    if (CanTurnLeft())
        //    {
        //        if (CanTurnRight() && Random.value > 0.5f)
        //        {
        //            Debug.Log("Right");
        //            isTurning = true;
        //            StartCoroutine(TurningInProgress(-transform.up, -90));
        //        }

        //        Debug.Log("Left");
        //        isTurning = true;
        //        StartCoroutine(TurningInProgress(transform.up, 90));
        //    }
        //    else
        //    {
        //        Debug.Log("Right");
        //        isTurning = true;
        //        StartCoroutine(TurningInProgress(-transform.up, -90));
        //    }
        //}
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

    private bool MakeTurn(string debugDir, Vector3 dirVec, float angle, float probability)
    {
        bool success = false;

        if (Random.value < probability)
        {
            Debug.Log(debugDir);
            isTurning = true;
            StartCoroutine(TurningInProgress(dirVec, angle));
            success = true;
        }
        
        return success;
    }

    // OnTriggerStay instead of OnTriggerEnter because 
    // OnTriggerEnter sometimes misses and event never happens.
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            EncounterWall();
        }
    }

    private void EncounterWall()
    {
        bool canTurnLeft = CanTurnLeft();
        bool canTurnRight = CanTurnRight();

        // Dead end
        if (!canTurnLeft && !canTurnRight)
        {
            if (ShouldTurnBack() && !isTurning)
            {
                Debug.Log("Turn Back");
                isTurning = true;
                StartCoroutine(TurningInProgress(-transform.right, 180));
            }
        }

        // Fork
        else if (!isTurning)
        {
            if (canTurnLeft && canTurnRight)
            {
                if (Random.value > 0.5f)
                {
                    Debug.Log("Force Left");
                    isTurning = true;
                    StartCoroutine(TurningInProgress(transform.up, 90));
                }
                else
                {
                    Debug.Log("Force Right");
                    isTurning = true;
                    StartCoroutine(TurningInProgress(-transform.up, -90));
                }
            }
            // Can only go left
            else if (!canTurnRight && canTurnLeft)
            {
                Debug.Log("Force Left");
                isTurning = true;
                StartCoroutine(TurningInProgress(transform.up, 90));
            }
            // Can only go right
            else if (!canTurnLeft && canTurnRight)
            {
                Debug.Log("Force Right");
                isTurning = true;
                StartCoroutine(TurningInProgress(-transform.up, -90));
            }
        }
    }
}

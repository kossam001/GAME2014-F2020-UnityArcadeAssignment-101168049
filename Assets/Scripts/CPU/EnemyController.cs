/* EnemyController.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-09
 * 
 * Controls the enemy character.
 * 
 * 2020-10-07: Added this script.
 * 2020-10-08: Added turning.
 * 2020-10-08: Added random wandering.
 * 2020-10-09: Added chasing.
 * 2020-10-09: Refactored code.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private CPUMove movement;
    [SerializeField]
    private CPUSearch detection;
    [SerializeField]
    private CPUFire shooting;
    public CPUStats stats;

    // Update is called once per frame
    void Update()
    {
        if (!stats.isChasing)
        {
            movement.Wander();
        }
        else if (stats.isChasing)
        {
            movement.Move();
            movement.Chase(playerLastPos, playerLastDir, ref stats.isChasing);
        }
        Search();
        shooting.Shoot();
    }
    
    Vector3 playerLastPos;
    Vector3 playerLastDir;

    private void Search()
    {
        CPUSearch.HitResult hitResult = detection.SearchForPlayer();

        if (hitResult.resultDirection == CPUSearch.DetectionDirection.nothing)
        {
            return;
        }

        // Close distance on enemy
        stats.isChasing = true;
        playerLastPos = hitResult.hitPos;
        playerLastDir = hitResult.hitDir;

        // Everytime the enemy is spotted during a chase, refresh timer
        detection.refresh = true;

        // Right
        if (hitResult.resultDirection == CPUSearch.DetectionDirection.right)
        {
            if (movement.CanTurnRight() && !movement.isTurning)
            {
                movement.MakeTurn("Right", -transform.up, -90, 1f);
            }
        }

        // Search left
        if (hitResult.resultDirection == CPUSearch.DetectionDirection.left)
        {
            if (movement.CanTurnLeft() && !movement.isTurning)
            {
                movement.MakeTurn("Left", transform.up, 90, 1f);
            }
        }

        // Back
        if (hitResult.resultDirection == CPUSearch.DetectionDirection.back)
        {
            movement.MakeTurn("Back", -transform.right, 180, 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            StartCoroutine(Knockback(collision.collider.gameObject));
        }
    }

    private IEnumerator Knockback(GameObject character)
    {
        float duration = 0.1f;

        while (duration > 0f)
        {
            duration -= Time.deltaTime;
            character.transform.position =
                character.transform.position +
                20 * Time.deltaTime * -character.transform.right;

            yield return null;
        }
    }

    public bool shouldIgnoreWalls = false; // Stop OnTrigger for walls if it interferes with other behaviours

    // OnTriggerStay instead of OnTriggerEnter because 
    // OnTriggerEnter sometimes misses and event never happens.
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && !shouldIgnoreWalls)
        {
            movement.EncounterWall();
        }
        // Stop moving if player is very close
        if (collision.gameObject.tag == "Player")
        {

        }
    }
}

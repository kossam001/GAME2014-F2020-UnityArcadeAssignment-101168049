/* EnemyController.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-11
 * 
 * Controls the enemy character.
 * 
 * 2020-10-07: Added this script.
 * 2020-10-08: Added turning.
 * 2020-10-08: Added random wandering.
 * 2020-10-09: Added chasing.
 * 2020-10-10: Refactored code.
 * 2020-10-10: Updated Search code.
 * 2020-10-11: Added Despawn.
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
    public CharacterStats stats;
    public EnemyManager manager;

    // Update is called once per frame
    void Update()
    {
        // Cleanup dead enemies inside own script instead of outside
        if (stats.isDead)
        {
            Despawn();
        }

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
        CPUSearch.HitResult hitResult = detection.SearchForObject();

        if (hitResult.resultDirection == CPUSearch.DetectionDirection.nothing)
        {
            return;
        }

        // Found Player
        if (hitResult.hitObj == "Player")
        {
            // Close distance on enemy
            stats.isChasing = true;
            playerLastPos = hitResult.hitPos;
            playerLastDir = hitResult.hitDir;

            // Everytime the enemy is spotted during a chase, refresh timer
            detection.refresh = true;
        }
        // Else found item
        // Ignore if in a chase
        else if (stats.isChasing && hitResult.hitObj == "Item")
        {
            return;
        }

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

    private void Despawn()
    {
        manager.inactiveEnemies.Add(gameObject);
        manager.activeEnemies.Remove(gameObject);
        manager.numEnemiesActive--;
        stats.isDead = false;
        gameObject.SetActive(false);
    }
}

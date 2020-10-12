/* CPUSearch.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-09
 * 
 * Script containing functions for CPU detection.
 * 
 * 2020-10-09: Added script.
 * 2020-10-09: Refactored code.
 * 2020-10-09: Added detecting back (Commented). 
 * 2020-10-10: Added a timeout for searching.
 * 2020-10-10: AI will go towards items it sees.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUSearch : MonoBehaviour
{
    public CharacterStats stats;

    public enum DetectionDirection
    {
        nothing,
        front,
        right,
        left,
        back
    }

    public class HitResult
    {
        public DetectionDirection resultDirection = DetectionDirection.nothing;
        public Vector3 hitPos = Vector3.zero;
        public Vector3 hitDir = Vector3.zero;
        public string hitObj;
    }
    
    public HitResult SearchForObject()
    {
        HitResult result = new HitResult();

        // Search front
        SearchInDirection(DetectionDirection.front, transform.right, stats.GetDetectionRange(), ref result);

        // Search right
        SearchInDirection(DetectionDirection.right, -transform.up, stats.GetDetectionRange() * 0.5f, ref result);

        // Search left
        SearchInDirection(DetectionDirection.left, transform.up, stats.GetDetectionRange() * 0.5f, ref result);

        // Search back
        //SearchInDirection(DetectionDirection.back, -transform.right, stats.detectionRange * 0.3f, "Player", ref result);

        return result;
    }

    private void SearchInDirection(DetectionDirection direction, Vector3 dirVector, float range, ref HitResult result)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + dirVector, dirVector, range, 0b1100000000);

        if (hit.collider)
        {
            if (hit.collider.gameObject.tag == "Player" ||
                hit.collider.gameObject.tag == "Item")
            {
                result.resultDirection = direction;
                result.hitPos = hit.collider.bounds.center;
                result.hitDir = hit.collider.gameObject.transform.right;
                result.hitObj = hit.collider.gameObject.tag;
            }
        }
    }

    // Stop chasing when enemy is confirmed gone
    // External control variables
    public bool refresh = false; // So the chase doesn't end abruptly
    public bool timerStarted = false; // Prevent multiple copies of Timeout()
    public IEnumerator Timeout()
    {
        if (!timerStarted)
        {
            timerStarted = true;
            float timeoutTimer = Random.Range(5, 10);

            while (timeoutTimer > 0)
            {
                if (refresh)
                {
                    timeoutTimer = Random.Range(5, 10);
                    refresh = false;
                }
                timeoutTimer -= Time.deltaTime;
                yield return null;
            }
        }
        timerStarted = false;
        stats.isChasing = false;
        
    }
}

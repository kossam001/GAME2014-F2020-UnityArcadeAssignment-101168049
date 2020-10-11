/* CPUHead.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-10
 * 
 * A script for functionality on the characters head.
 * Mostly for an additional collider since the dino is a little long.
 * 
 * 2020-10-10: Added script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUHead : MonoBehaviour
{
    [SerializeField]
    private CPUMove movement;

    // OnTriggerStay instead of OnTriggerEnter because 
    // OnTriggerEnter sometimes misses and event never happens.
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 10)
        {
            movement.EncounterWall();
        }
        // Stop moving if player is very close
        if (collision.gameObject.tag == "Player")
        {

        }
    }
}

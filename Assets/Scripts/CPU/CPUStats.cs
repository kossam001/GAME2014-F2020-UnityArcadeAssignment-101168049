/* CPUStats.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-10
 * 
 * Contains information on the CPU character.
 * 
 * 2020-10-10: Added script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUStats : MonoBehaviour
{
    // Stats
    public float speed = 3;
    public float firerate = 10;
    public float health = 1;
    public float detectionRange = 15;

    // States
    public bool isChasing = false;
}

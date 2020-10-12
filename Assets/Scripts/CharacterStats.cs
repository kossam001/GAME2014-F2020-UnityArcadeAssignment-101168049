/* CharacterStats.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-11
 * 
 * Contains information on the character.
 * 
 * 2020-10-10: Added script.
 * 2020-10-11: Added death state.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    // Stats
    public float speed = 3;
    public float maxSpeed = 10;

    public float firerate = 1;
    public float maxFirerate = 0.2f;

    public float health = 1;
    public float maxHealth = 5;

    public float detectionRange = 15;

    // States
    public bool isChasing = false;
    public bool isDead = false;
}

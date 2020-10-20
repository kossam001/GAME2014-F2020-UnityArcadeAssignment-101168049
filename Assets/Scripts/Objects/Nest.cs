/* Nest.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-12
 * 
 * Contains information on the character.
 * 
 * 2020-10-12: Added script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour
{
    [SerializeField]
    private int health = 10;

    public void DecreaseHealth()
    {
        health--;

        if (health <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
}

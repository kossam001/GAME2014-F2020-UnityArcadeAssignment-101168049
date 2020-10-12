/* Shield.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-11
 * 
 * Applies shield effect (Temporary invincibility).
 * 
 * 2020-10-11: Added script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Item
{
    public float activationTime = 1f;

    private void Start()
    {
        renderer.sprite = sprite;
    }

    public override void Pickup()
    {
        owner.GetComponent<CharacterStats>().ActivateShield(activationTime);

        base.Pickup();
    }
}

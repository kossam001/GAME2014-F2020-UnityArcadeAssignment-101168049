/* PepperEffect.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-10
 * 
 * Applies pepper effect (projectile firerate up).
 * 
 * 2020-10-10: Added script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepperEffect : Item
{
    private void Start()
    {
        renderer.sprite = sprite;
    }

    public override void Pickup()
    {

        owner.GetComponent<CharacterStats>().IncreaseFirerate();

        base.Pickup();
    }
}

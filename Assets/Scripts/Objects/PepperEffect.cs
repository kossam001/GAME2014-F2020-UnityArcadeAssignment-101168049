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
        if (owner.GetComponent<CharacterStats>().firerate >= owner.GetComponent<CharacterStats>().maxFirerate)
            owner.GetComponent<CharacterStats>().firerate -= 0.1f;

        base.Pickup();
    }
}

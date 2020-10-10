/* MeatEffect.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-10
 * 
 * Applies meat effect (movement + projectile speed up).
 * 
 * 2020-10-10: Added script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MeatEffect : Item
{
    private void Start()
    {
        renderer.sprite = sprite;
    }

    public override void Pickup()
    {
        owner.GetComponent<CharacterStats>().speed++;

        base.Pickup();
    }
}

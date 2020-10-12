/* EggEffect.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-11
 * 
 * Applies egg effect (increase health).
 * 
 * 2020-10-11: Added script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggEffect : Item
{
    private void Start()
    {
        renderer.sprite = sprite;
    }

    public override void Pickup()
    {

        owner.GetComponent<CharacterStats>().IncreaseHealth();

        base.Pickup();
    }
}

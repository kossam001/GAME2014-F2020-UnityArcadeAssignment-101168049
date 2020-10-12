/* RegenShield.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-11
 * 
 * Some Enemies have regenerative shields.
 * 
 * 2020-10-11: Added script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenShield : MonoBehaviour
{
    [SerializeField]
    private float cooldownTime = 15f;
    private float cooldown = 15f;
    [SerializeField]
    private float shieldDuration = 5f;
    
    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<CharacterStats>().hasShield)
        {
            return;
        }

        cooldown -= Time.deltaTime;

        if (cooldown > 0)
        {
            return;
        }

        gameObject.GetComponent<CharacterStats>().ActivateShield(shieldDuration);
        cooldown = cooldownTime;
    }
}

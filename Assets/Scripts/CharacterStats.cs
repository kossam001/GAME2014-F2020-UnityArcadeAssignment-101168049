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
 * 2020-10-11: Health related functions added for easier implementation of shield buff.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    // Stats
    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private float maxSpeed = 10;

    [SerializeField]
    private float firerate = 1;
    [SerializeField]
    private float maxFirerate = 0.2f;

    [SerializeField]
    private int health = 1;
    [SerializeField]
    private float maxHealth = 5;

    [SerializeField]
    private float shieldDuration;

    [SerializeField]
    private float detectionRange = 15;

    // States
    public bool isChasing = false;
    public bool isDead = false;
    public bool hasShield = false;

    // Other
    public SpriteRenderer shieldSprite;
    
    public void ActivateShield(float duration)
    {
        if (hasShield)
        {
            shieldDuration = duration;
        }
        else
        {
            hasShield = true;
            shieldSprite.enabled = true;
            StartCoroutine(ShieldOn(duration));
        }
    }

    // Not always on, call when needed.
    private IEnumerator ShieldOn(float duration)
    {
        shieldDuration = duration;

        while (shieldDuration > 0)
        {
            shieldDuration -= Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(duration);
        shieldSprite.enabled = false;
        hasShield = false;
    }

    // Shield prevents health decrease
    public void DecreaseHealth()
    {
        if (!hasShield)
            health--;
    }

    public void IncreaseHealth()
    {
        if (health < maxHealth)
        {
            health++;
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public void IncreaseSpeed()
    {
        if (speed < maxSpeed)
        {
            speed++;
        }
    }

    public float GetSpeed()
    {
        return speed;
    }

    public void IncreaseFirerate()
    {
        if (firerate < maxFirerate)
        {
            firerate -= 0.1f;
        }
    }

    public float GetFirerate()
    {
        return firerate;
    }

    public float GetDetectionRange()
    {
        return detectionRange;
    }
}

/* CharacterStats.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-18
 * 
 * Contains information on the character.
 * 
 * 2020-10-10: Added script.
 * 2020-10-11: Added death state.
 * 2020-10-11: Health related functions added for easier implementation of shield buff.
 * 2020-10-12: Enemies change colour to show damage.
 * 2020-10-12: Set default values for stats so they can be reverted on respawn.
 * 2020-10-12: Adds score.
 * 2020-10-18: Sound added.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterStats : MonoBehaviour
{
    // Stats
    [SerializeField]
    private float origSpeed, speed = 3;

    [SerializeField]
    private float maxSpeed = 10;

    [SerializeField]
    private float origFirerate, firerate = 1;

    [SerializeField]
    private float maxFirerate = 0.2f;

    [SerializeField]
    private int origHealth, health = 1;

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
    public int points;

    public AudioClip hitSE;

    // Initialize default values
    private void Start()
    {
        origFirerate = firerate;
        origHealth = health;
        origSpeed = speed;
    }

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
        {
            Vector3 audioPos = transform.position;
            audioPos.z = -10;
            AudioSource.PlayClipAtPoint(hitSE, audioPos);

            health--;
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 255);
            StartCoroutine(TakeDamage());

            if (gameObject.tag == "Player")
                GameManager.Instance.UpdateLives(health);

            if (health <= 0)
            {
                isDead = true;
            }
        }
    }

    // Health carries over level
    public void SetHealth(int _health)
    {
        health = _health;
    }

    private IEnumerator TakeDamage()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
    }

    public void IncreaseHealth()
    {
        if (health < maxHealth)
        {
            health++;
            if (gameObject.tag == "Player")
                GameManager.Instance.UpdateLives(health);
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

    public void ResetStats()
    {
        health = origHealth;
        speed = origSpeed;
        firerate = origFirerate;
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
    }

    public void AddPoints()
    {
        GameManager.Instance.AddScore(points);
    }
}

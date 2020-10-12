/* CPUFire.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-10
 * 
 * Script containing functions for CPU Attack.
 * 
 * 2020-10-10: Added script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUFire : MonoBehaviour
{
    [SerializeField]
    private CPUMove movement;
    public GameObject fireball;
    public CharacterStats stats;
    public FireballManager fireballManager;

    // Update is called once per frame
    void Update()
    {
        // Update once per Update();
        cooldown -= Time.deltaTime;
    }

    private void ShootRight()
    {
        if (stats.isChasing)
        {
            return;
        }

        // Turn Right
        movement.MakeRightTurn();
    }

    private void ShootLeft()
    {
        if (stats.isChasing)
        {
            return;
        }

        // Turn Left
        movement.MakeLeftTurn();
    }

    float cooldown;
    public void Shoot()
    {
        if (!CanShoot())
        {
            return;
        }

        if (Random.value < 0.25f)
        {
            ShootLeft();
        }
        else if (Random.value < 0.25f)
        {
            ShootRight();
        }

        GameObject fireball = fireballManager.fireballs.Dequeue();
        fireball.transform.position = transform.position;
        fireball.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -90);
        fireball.GetComponent<FireballBehaviour>().speed = GetComponent<CharacterStats>().GetSpeed() + 1.5f;
        fireball.GetComponent<FireballBehaviour>().owner = gameObject;
        fireball.SetActive(true);

        cooldown = Random.Range(stats.GetFirerate(), 3f);
    }

    private bool CanShoot()
    {
        if (cooldown > 0)
        {
            return false;
        }
 
        return true;
    }
}

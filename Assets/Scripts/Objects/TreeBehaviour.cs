/* TreeBehaviour.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-12
 * 
 * Trees should block other characters.
 * They should disappear when hit with a fireball.
 * A fireball that hits a tree should disappear.
 * 
 * 2020-10-07: Added this script.
 * 2020-10-11: Trees don't destroy fireballs.
 * 2020-10-12: Added points.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeBehaviour : MonoBehaviour
{
    public Tilemap tilemap;
    [SerializeField]
    private int points = 100;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            // Convert the world position of the projectile into cell position
            Vector3Int tilePos = tilemap.WorldToCell(collision.gameObject.transform.position);
            if (tilemap.HasTile(tilePos))
            {
                tilemap.SetTile(tilePos, null); // Remove tree tile

                collision.gameObject.GetComponent<FireballBehaviour>().despawn = true; // Raise the flag, don't do it on its own
                                                                                       // or collider bugs.

                GameManager.Instance.AddScore(points);
            }
        }
    }
}

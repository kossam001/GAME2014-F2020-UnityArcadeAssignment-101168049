/* TreeBehaviour.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-07
 * 
 * Trees should block other characters.
 * They should disappear when hit with a fireball.
 * A fireball that hits a tree should disappear.
 * 
 * 2020-10-07: Added this script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeBehaviour : MonoBehaviour
{
    public Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            // Convert the world position of the projectile into cell position
            Vector3Int tilePos = tilemap.WorldToCell(collision.gameObject.transform.position);
            if (tilemap.HasTile(tilePos))
            {
                tilemap.SetTile(tilePos, null); // Remove tree tile
                Destroy(collision.gameObject); // Despawning of projectiles here since the world WorldToCell is not very precise
            }
        }
    }
}

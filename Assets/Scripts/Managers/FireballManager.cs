/* FireballManager.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-11
 * 
 * Manages fireball spawning.
 * 
 * 2020-10-11: Added script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballManager : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Queue<GameObject> fireballs;

    // Start is called before the first frame update
    void Start()
    {
        fireballs = new Queue<GameObject>();

        for (int i = 0; i < 50; i++)
        {
            GameObject fireball = Instantiate(fireballPrefab, transform);
            fireball.GetComponent<FireballBehaviour>().manager = this;
            fireball.SetActive(false);
            fireballs.Enqueue(fireball);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

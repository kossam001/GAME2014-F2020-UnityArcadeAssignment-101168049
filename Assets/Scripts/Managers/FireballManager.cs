/* FireballManager.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-18
 * 
 * Manages fireball spawning.
 * 
 * 2020-10-11: Added script.
 * 2020-10-18: Privated fireball list.  That way, when enqueuing, audio plays, and less external code will need changing.
 * 2020-10-18: Fireball plays audio on collision.  Fireball despawns on collision, so calling it when enqueued.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballManager : MonoBehaviour
{
    public GameObject fireballPrefab;
    private Queue<GameObject> fireballs;

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

    // Easier
    public void EnqueueFireball(GameObject fireball)
    {
        Vector3 audioPos = fireball.transform.position;
        audioPos.z = -10;
        AudioSource.PlayClipAtPoint(fireball.GetComponent<AudioSource>().clip, audioPos);
        fireballs.Enqueue(fireball);
    }

    public GameObject DequeueFireball()
    {
        return fireballs.Dequeue();
    }
}

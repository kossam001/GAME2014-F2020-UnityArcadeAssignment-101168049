/* SwapScalerReference.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-09-29
 * 
 * Objects with this script will become Singletons.
 * 
 * 2020-09-29: Added this script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    private static Singleton instance;
    public static Singleton Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }
}

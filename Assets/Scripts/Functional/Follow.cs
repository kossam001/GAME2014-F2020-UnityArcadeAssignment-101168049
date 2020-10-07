/* Follow.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-06
 * 
 * GameObject with this script will follow the target.  
 * Used to copy the position of another GameObject without parenting them.
 * Since it is intended to be used with the camera, the z value should not be
 * copied.
 * 
 * 2020-10-06: Added this script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // keep z the same, it shouldn't matter, but for something like a camera, z is important
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }
}

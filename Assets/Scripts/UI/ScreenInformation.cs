/* GameManager.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-15
 * 
 * Keeps track of device orientation across scenes.
 * Device orientation is unknown at the start of a new scene, so
 * this script keeps it consistent.
 * 
 * 2020-10-15: Added script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenInformation : MonoBehaviour
{
    private static ScreenInformation instance;
    public static ScreenInformation Instance { get { return instance; } }

    // Singleton
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
    }

    public enum orientation
    {
        LANDSCAPELEFT,
        LANDSCAPERIGHT,
        PORTRAIT,
        UNKNOWN
    }

    public orientation lastOrientation = orientation.UNKNOWN;

    private void Update()
    {
        switch (Input.deviceOrientation)
        {
            case DeviceOrientation.LandscapeLeft:
                lastOrientation = orientation.LANDSCAPELEFT;
                break;
            case DeviceOrientation.LandscapeRight:
                lastOrientation = orientation.LANDSCAPERIGHT;
                break;
            case DeviceOrientation.Portrait:
                lastOrientation = orientation.PORTRAIT;
                break;
        }
    }

    public orientation GetLastOrientation()
    {
        Debug.Log(lastOrientation);
        return lastOrientation;
    }
}

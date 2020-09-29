/* SwapLayout.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-09-29
 * 
 * To allow portrait mode to work, I created a separate layout for several screens.
 * This script will swap out the layouts and handle potential issues with orientation changes.
 * 
 * 2020-09-29: Added this script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapLayout : MonoBehaviour
{
    [SerializeField]
    GameObject portraitLayout;

    [SerializeField]
    GameObject landscapeLayout;

    [SerializeField]
    GameObject level;

    // Update is called once per frame
    void Update()
    {
        switch (Input.deviceOrientation)
        {
            case DeviceOrientation.LandscapeLeft:
                landscapeLayout.SetActive(true);
                portraitLayout.SetActive(false);

                if (level != null)
                {
                    level.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                }

                landscapeLayout.transform.Rotate(0, 0, 0);
                break;
            case DeviceOrientation.LandscapeRight:
                landscapeLayout.SetActive(true);
                portraitLayout.SetActive(false);

                if (level != null)
                {
                    level.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                }

                landscapeLayout.transform.Rotate(0, 0, 180);
                break;
            case DeviceOrientation.Portrait:
                landscapeLayout.SetActive(false);
                portraitLayout.SetActive(true);

                // When orienting to portrait, the overall screen shrinks
                // so the level becomes enlarged, so this section fixes that.
                if (level != null)
                {
                    level.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                }
   
                break;
            case DeviceOrientation.Unknown:
                break;
        }
    }
}

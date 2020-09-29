/* SwapScalerReference.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-09-29
 * 
 * Allows the program to swap Resolution Reference between width and height on the CanvasScaler
 * if necessary.
 * 
 * 2020-09-29: Added this script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class SwapScalerReference : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CanvasScaler canvasScaler = GetComponent<CanvasScaler>();

        if (Screen.currentResolution.width / Screen.currentResolution.height < 1.5f)
        {
            canvasScaler.matchWidthOrHeight = 0;
        }
        else
        {
            canvasScaler.matchWidthOrHeight = 1;
        }
    }
}

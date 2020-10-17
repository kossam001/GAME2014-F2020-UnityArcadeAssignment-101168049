/* RetryButton.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-16
 * 
 * Additional behaviours to the retry button.
 * 
 * 2020-10-16: Added this script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButton : MonoBehaviour
{
    public void OnClick()
    {
        // Reset score
        GameManager.Instance.score = 0;
    }
}

/* FinalScore.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-16
 * 
 * Display final score.
 * 
 * 2020-10-16: Added this script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour, DynamicUI
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = GameManager.Instance.score.ToString();
        GameManager.Instance.score = 0;
    }

    public void OnLayoutChange()
    {
        GetComponent<Text>().text = GameManager.Instance.score.ToString();
        GameManager.Instance.score = 0;
    }
}

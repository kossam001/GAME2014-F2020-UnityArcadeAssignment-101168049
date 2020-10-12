/* GameManager.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-11
 * 
 * Manages different aspects of the game such as score, spawning, stat changes, etc.
 * 
 * 2020-10-10: Added script.
 * 2929-10-11: Moved item related code to ItemManager
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score = 0;

    // UI
    [SerializeField]
    private Text scoreText;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddScore(int _score)
    {
        score += _score;
    }
}

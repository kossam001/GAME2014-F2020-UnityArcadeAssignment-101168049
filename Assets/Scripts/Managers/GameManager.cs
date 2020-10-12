/* GameManager.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-12
 * 
 * Manages score.
 * 
 * 2020-10-10: Added script.
 * 2020-10-11: Moved item related code to ItemManager
 * 2020-10-12: Added scorekeeping and displaying it on a text label.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

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
        scoreText.text = score.ToString();
    }

    public void AddScore(int _score)
    {
        score += _score;
    }
}

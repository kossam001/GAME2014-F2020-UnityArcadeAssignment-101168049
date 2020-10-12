/* UpdateLayout.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-12
 * 
 * Update the UI labels based on game progression.
 * GameManager is persistent but does not have access
 * to ingame UI.  
 * 
 * Ingame UI should not be persistent or else it would
 * appear in other scenes.
 * 
 * This is an intermediary between the two objects to communicate information.
 * 
 * 2020-10-12: Added script.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLayout : MonoBehaviour
{
    private static UpdateLayout instance;
    public static UpdateLayout Instance { get { return instance; } }

    // Singleton
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // UI Landscape
    [SerializeField]
    private Text scoreTextLandscape;
    [SerializeField]
    private Text livesTextLandscape;
    [SerializeField]
    private Text enemiesLeftTextLandscape;

    public void UpdateScore(int score)
    {
        scoreTextLandscape.text = score.ToString();
    }

    public void UpdateLives(int lives)
    {
        livesTextLandscape.text = lives.ToString();
    }

    public void UpdateEnemyCounter(int enemies)
    {
        enemiesLeftTextLandscape.text = enemies.ToString();
    }

    public bool isReady()
    {
        // Possible for this to be initialized but labels not
        if (enemiesLeftTextLandscape && livesTextLandscape && scoreTextLandscape)
        {
            return true;
        }

        return false;
    }
}

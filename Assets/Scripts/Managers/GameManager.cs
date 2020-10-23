/* GameManager.cs
 * 
 * Samuel Ko
 * 101168049
 * Last Modified: 2020-10-18
 * 
 * Manages gameplay elements.
 * 
 * 2020-10-10: Added script.
 * 2020-10-11: Moved item related code to ItemManager
 * 2020-10-12: Added scorekeeping and displaying it on a text label.
 * 2020-10-15: Reset player lives on gameover
 * 2020-10-18: Added simple level progression.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    UnityEvent updateUI = new UnityEvent();

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
            updateUI.AddListener(LoadUIInfo);
        }
    }

    public int score = 0;

    // UI
    [SerializeField]
    public int lives = 3;
    public int origLives = 3;

    [SerializeField]
    public int enemiesLeft;
    public int maxEnemiesInStage = 5;

    // Check that the layout has been initialized before updating
    private IEnumerator QueueUpdate()
    {
        while (!UpdateLayout.Instance || !UpdateLayout.Instance.isReady())
        {
            yield return null;
        }

        UpdateLayout.Instance.UpdateScore(score);
        UpdateLayout.Instance.UpdateEnemyCounter(enemiesLeft);
        UpdateLayout.Instance.UpdateLives(lives);
    }

    public void AddScore(int _score)
    {
        score += _score;
        updateUI.Invoke();
    }

    public void ResetScore()
    {
        score = 0;
    }

    public void UpdateEnemies(int enemyCount)
    {
        enemiesLeft = enemyCount;
        updateUI.Invoke();
    }

    public void UpdateLives(int _lives)
    {
        lives = _lives;
        updateUI.Invoke();
    }

    public void LoadStage(string stageName)
    {
        maxEnemiesInStage += 5;
        SceneManager.LoadScene(stageName);
    }

    public void GameOver()
    {
        maxEnemiesInStage = 5;
        lives = origLives;
        SceneManager.LoadScene("GameOver");
    }

    public void LoadUIInfo()
    {
        StartCoroutine(QueueUpdate());
    }
}

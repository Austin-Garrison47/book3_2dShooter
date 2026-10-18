
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using System.Globalization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TMP_Text playerNameText;
    public TMP_Text levelText;
    public TMP_Text scoreText;
    public TMP_Text timerText;

    public int score = 0;
    public int level = 1;

    public static int finalScore;
    public static int finalLevel;
    public static float finalTime;

    float timer = 0f;
    bool isPaused = false;
    bool gameEnded = false;
    int enemiesDestroyed = 0;

    void Start()
    {
        instance = this;
        Time.timeScale = 1f;
        playerNameText.text = "Player: " + PreferencesManager.playerName;
    }

    void Update()
    {
        if (gameEnded)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f;
        }

        if (!isPaused)
            timer += Time.deltaTime;

        int minutes = (int)(timer / 60);
        int seconds = (int)(timer % 60);

        timerText.text = "Time: " + minutes + ":" + seconds.ToString("00");
        scoreText.text = "Score: " + score;
        levelText.text = "Level: " + level;
    }

    public void AddScore(int points)
    {
        score += points;
        enemiesDestroyed++;

        if (enemiesDestroyed % 5 == 0)
            level++;
    }

    public void StopGame()
    {
        if (!gameEnded)
            StartCoroutine(SaveScore());
    }

    IEnumerator SaveScore()
    {
        gameEnded = true;

        finalScore = score;
        finalLevel = level;
        finalTime = timer;

        Time.timeScale = 1f;

        string url = "http://104.129.130.168/updateShooters.php" +
            "?name=" + UnityWebRequest.EscapeURL(PreferencesManager.playerName) +
            "&level=" + finalLevel +
            "&score=" + finalScore +
            "&time=" + finalTime.ToString(CultureInfo.InvariantCulture);

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.timeout = 10;
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
                Debug.LogWarning("Score submission failed: " + request.error);
            else
                Debug.Log("Score submission request completed.");
        }

        SceneManager.LoadScene("exit");
    }
}

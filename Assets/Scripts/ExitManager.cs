
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class ExitManager : MonoBehaviour
{
    public TMP_Text finalNameText;
    public TMP_Text finalScoreText;
    public TMP_Text finalLevelText;
    public TMP_Text finalTimeText;
    public TMP_Text leaderboardText;

    void Start()
    {
        finalNameText.text = "Player: " + PreferencesManager.playerName;
        finalScoreText.text = "Score: " + GameManager.finalScore;
        finalLevelText.text = "Level: " + GameManager.finalLevel;

        int minutes = (int)(GameManager.finalTime / 60);
        int seconds = (int)(GameManager.finalTime % 60);

        finalTimeText.text = "Time: " + minutes + ":" + seconds.ToString("00");

        StartCoroutine(GetScores());
    }

    IEnumerator GetScores()
    {
        leaderboardText.text = "Loading top scores...";

        using (UnityWebRequest request =
            UnityWebRequest.Get("http://104.129.130.168/getShooters.php"))
        {
            request.timeout = 10;
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                leaderboardText.text = "Could not load scores.";
                Debug.LogWarning(request.error);
                yield break;
            }

            string[] lines = request.downloadHandler.text.Split('\n');
            List<string[]> scores = new List<string[]>();

            foreach (string line in lines)
            {
                string[] values = line.Split(
                    (char[])null,
                    System.StringSplitOptions.RemoveEmptyEntries);

                if (values.Length >= 4 &&
                    int.TryParse(values[values.Length - 2], out int score))
                {
                    string name = string.Join(" ", values, 0, values.Length - 3);
                    string level = values[values.Length - 3];
                    string time = values[values.Length - 1];

                    scores.Add(new string[] {
                        name, level, score.ToString(), time
                    });
                }
            }

            scores.Sort((a, b) =>
                int.Parse(b[2]).CompareTo(int.Parse(a[2])));

            leaderboardText.text = "TOP 5 SCORES\n";

            for (int i = 0; i < Mathf.Min(5, scores.Count); i++)
            {
                leaderboardText.text +=
                    (i + 1) + ". " + scores[i][0] +
                    "  Level: " + scores[i][1] +
                    "  Score: " + scores[i][2] +
                    "  Time: " + scores[i][3] + "\n";
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("game");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("intro");
    }
}

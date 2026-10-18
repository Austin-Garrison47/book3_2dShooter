
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PreferencesManager : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public TMP_Dropdown enemySpeedDropdown;
    public Slider spawnRateSlider;
    public TMP_Text spawnRateValueText;

    public static string playerName = "Guest";
    public static float enemySpeed = 0.016f;
    public static int spawnRate = 10;

    void Start()
    {
        spawnRateSlider.minValue = 1;
        spawnRateSlider.maxValue = 15;
        spawnRateSlider.wholeNumbers = true;
        spawnRateSlider.value = spawnRate;
        spawnRateValueText.text = spawnRate.ToString();
    }

    public void UpdateSpawnRate(float value)
    {
        spawnRateValueText.text = ((int)value).ToString();
    }

    public void PlayGame()
    {
        playerName = playerNameInput.text;

        if (playerName == "")
        {
            playerName = "Guest";
        }

        if (enemySpeedDropdown.value == 0)
            enemySpeed = 0.008f;  // Very Slow
        else if (enemySpeedDropdown.value == 1)
            enemySpeed = 0.015f;  // Slow
        else if (enemySpeedDropdown.value == 2)
            enemySpeed = 0.021f;  // Normal
        else if (enemySpeedDropdown.value == 3)
            enemySpeed = 0.028f;  // Fast
        else
            enemySpeed = 0.035f;  // Very Fast

        spawnRate = (int)spawnRateSlider.value;

        SceneManager.LoadScene("game");
    }

    public void Back()
    {
        SceneManager.LoadScene("intro");
    }
}

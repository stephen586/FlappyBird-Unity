
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public int highScore = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public GameObject gameOverText;
    public GameObject restartButton;

    // Start screen objects
    public GameObject startTitle;
    public GameObject playButton;

    private bool gameStarted = false;
    private bool gameEnded = false;

    // Remember when the player clicks RESTART
    private static bool restartRequested = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        score = 0;
        gameStarted = false;
        gameEnded = false;

        highScore = PlayerPrefs.GetInt("HighScore", 0);

        UpdateScoreText();
        UpdateHighScoreText();

        // Show start screen
        if (startTitle != null)
            startTitle.SetActive(true);

        if (playButton != null)
            playButton.SetActive(true);

        // Hide gameplay UI
        if (scoreText != null)
            scoreText.gameObject.SetActive(false);

        if (highScoreText != null)
            highScoreText.gameObject.SetActive(false);

        if (gameOverText != null)
            gameOverText.SetActive(false);

        if (restartButton != null)
            restartButton.SetActive(false);

        // Wait for PLAY
        Time.timeScale = 0f;

        // Automatically start after RESTART
        if (restartRequested)
        {
            restartRequested = false;
            StartGame();
        }
    }

    public void StartGame()
    {
        if (gameStarted)
            return;

        gameStarted = true;

        if (startTitle != null)
            startTitle.SetActive(false);

        if (playButton != null)
            playButton.SetActive(false);

        if (scoreText != null)
            scoreText.gameObject.SetActive(true);

        Time.timeScale = 1f;

        Debug.Log("Game Started!");
    }

    public void AddScore()
    {
        if (!gameStarted || gameEnded)
            return;

        score++;
        UpdateScoreText();

        if (score > highScore)
        {
            highScore = score;

            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();

            UpdateHighScoreText();
        }

        Debug.Log("Score: " + score);
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = score.ToString();
    }

    private void UpdateHighScoreText()
    {
        if (highScoreText != null)
            highScoreText.text = "BEST: " + highScore;
    }

    public void GameOver()
    {
        if (!gameStarted || gameEnded)
            return;

        gameEnded = true;

        if (gameOverText != null)
            gameOverText.SetActive(true);

        if (restartButton != null)
            restartButton.SetActive(true);

        if (highScoreText != null)
        {
            UpdateHighScoreText();
            highScoreText.gameObject.SetActive(true);
        }

        Debug.Log("Game Over!");

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        restartRequested = true;

        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
